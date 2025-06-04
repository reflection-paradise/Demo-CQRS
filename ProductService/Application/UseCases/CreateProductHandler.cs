using Application.IMessageBus;
using System.Text;
using System.Text.Json;
using Application.Command.EventModel;
using Application.Command;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

public class CreateProductHandler
{
    private readonly IProductRepository productRepository;
    private readonly IMapper _mapper;
    private readonly IRabbitMQPublisher _publisher;
    private static readonly Random _random = new Random();
    private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public CreateProductHandler(IProductRepository productRepository, IMapper mapper, IRabbitMQPublisher publisher)
    {
        this.productRepository = productRepository;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<CreateProductDTO> CreateHandler(CreateProductDTO data)
    {
        try
        {
            var map = _mapper.Map<Product>(data);
            map.ProductId = GenerateProductId();
            var createdata = await productRepository.CreateRepository(map);
            var result = _mapper.Map<CreateProductDTO>(createdata);

            // Sau khi tạo, gửi event
            var eventMessage = new ProductCreatedEvent
            {
                ProductId = map.ProductId,
                ProductName = map.ProductName,
                CategoryId = map.CategoryId,
                Price = map.Price,
                CreatedAt = map.CreatedAt ?? DateTime.UtcNow
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(eventMessage));
            _publisher.Publish("product_exchange", "product.created", body);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static string GenerateProductId()
    {
        int length = 6;
        return new string(Enumerable.Repeat(_chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}
