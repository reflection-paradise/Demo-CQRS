using Application.Command.EventModel;
using Domain.EntitiesM;
using Infrastructure.DbConnect;

namespace Infrastructure.Consumers
{
    public class ProductCreatedConsumer
    {
        private readonly MongoDbECommerceContext _mongoContext;

        public ProductCreatedConsumer(MongoDbECommerceContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public void Consume(ProductCreatedEvent message)
        {
            Console.WriteLine($"Saving product to Mongo: {message.ProductName}");

            var product = new ProductM
            {
                ProductName = message.ProductName,
                CategoryId = "64a1f8b7e1a4b5f3cde12345",
                Price = (double)message.Price,
                CreatedAt = message.CreatedAt
            };

            _mongoContext.Products.InsertOne(product);
            Console.WriteLine("Mongo insert done.");

        }
    }
}
