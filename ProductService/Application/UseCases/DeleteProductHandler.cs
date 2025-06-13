using Application.Command.EventModel;
using Application.Command;
using Application.IMessageBus;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.UseCases
{
    public class DeleteProductHandler
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper _mapper;
        private readonly IRabbitMQPublisher _publisher;

        public DeleteProductHandler(IProductRepository productRepository, IMapper mapper, IRabbitMQPublisher publisher)
        {
            this.productRepository = productRepository;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<bool> DeleteHandler(string id)
        {
            try
            {
                var find = await productRepository.GetByIdSQLAsync(id);
                if (find == null)
                {
                    throw new Exception("No data to update!");
                }
                var delete = await productRepository.DeleteRepository(find);
                // Sau khi tạo, gửi event
                var eventMessage = new ProductCreatedEvent
                {
                    ProductId = id
                };

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(eventMessage));
                _publisher.Publish("product_exchange", "product.deleted", body);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
