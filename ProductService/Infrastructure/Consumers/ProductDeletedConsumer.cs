using Application.Command.EventModel;
using Infrastructure.DbConnect;
using MongoDB.Driver;

namespace Infrastructure.Consumers
{
    public class ProductDeletedConsumer
    {
        private readonly MongoDbECommerceContext _mongoContext;

        public ProductDeletedConsumer(MongoDbECommerceContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public void Consume(ProductCreatedEvent message)
        {
            Console.WriteLine($"[Consumer] Deleting product with BusinessId: {message.ProductId}");

            var filter = Builders<Domain.EntitiesM.ProductM>.Filter.Eq(p => p.BusinessId, message.ProductId);
            var result = _mongoContext.Products.DeleteOne(filter);

            if (result.DeletedCount > 0)
                Console.WriteLine("[Consumer] Product deleted from MongoDB.");
            else
                Console.WriteLine("[Consumer] No product found to delete.");
        }
    }
}
