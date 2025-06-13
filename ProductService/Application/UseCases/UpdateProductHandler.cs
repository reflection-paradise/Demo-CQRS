using Application.Command.EventModel;
using Application.Command;
using Application.IMessageBus;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.EntitiesM;

namespace Application.UseCases
{
    public class UpdateProductHandler
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper _mapper;
        private readonly IRabbitMQPublisher _publisher;
        private static readonly Random _random = new Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public UpdateProductHandler(IProductRepository productRepository, IMapper mapper, IRabbitMQPublisher publisher)
        {
            this.productRepository = productRepository;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<bool> UpdateHandler(string id, UpdateProductDTO data)
        {
            try
            {
                var find = await productRepository.GetByIdSQLAsync(id);
                if(find == null)
                {
                    throw new Exception("No data to update!");
                }
                _mapper.Map(data, find);
                var dataUpdate = await productRepository.UpdateRepository(find);

                // Sau khi tạo, gửi event
                var eventMessage = new ProductCreatedEvent
                {
                    ProductId = dataUpdate.ProductId,
                    ProductName = dataUpdate.ProductName,
                    CategoryId = dataUpdate.CategoryId,
                    Price = dataUpdate.Price,
                    CreatedAt = dataUpdate.CreatedAt ?? DateTime.UtcNow
                };

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(eventMessage));
                _publisher.Publish("product_exchange", "product.updated", body);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
