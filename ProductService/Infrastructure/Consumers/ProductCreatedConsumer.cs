using Application.Command.EventModel;
using Domain.EntitiesM;
using Infrastructure.DbConnect;
using MongoDB.Driver;

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
            Console.WriteLine($"[Consumer] Handling product event: {message.ProductName}");

            var collection = _mongoContext.Products;

            var filter = Builders<ProductM>.Filter.Eq(p => p.BusinessId, message.ProductId);
            var existingProduct = collection.Find(filter).FirstOrDefault();

            if (existingProduct != null)
            {
                UpdateProduct(collection, filter, message);
            }
            else
            {
                InsertProduct(collection, message);
            }
        }

        private void UpdateProduct(IMongoCollection<ProductM> collection, FilterDefinition<ProductM> filter, ProductCreatedEvent message)
        {
            var update = Builders<ProductM>.Update
                .Set(p => p.ProductName, message.ProductName)
                .Set(p => p.CategoryId, "64a1f8b7e1a4b5f3cde12345") // hoặc map đúng Mongo ID theo logic thực tế
                .Set(p => p.Price, (double)message.Price)
                .Set(p => p.CreatedAt, message.CreatedAt);

            collection.UpdateOne(filter, update);
            Console.WriteLine("[Consumer] Product updated in MongoDB.");
        }

        private void InsertProduct(IMongoCollection<ProductM> collection, ProductCreatedEvent message)
        {
            var product = new ProductM
            {
                BusinessId = message.ProductId,
                ProductName = message.ProductName,
                CategoryId = "64a1f8b7e1a4b5f3cde12345", // giống trên
                Price = (double)message.Price,
                CreatedAt = message.CreatedAt
            };

            collection.InsertOne(product);
            Console.WriteLine("[Consumer] Product inserted into MongoDB.");
        }
    }
}
