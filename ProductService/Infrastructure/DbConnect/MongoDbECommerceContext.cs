using Application.ReadModels;
using Domain.EntitiesM;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbConnect
{
    public class MongoDbECommerceContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbECommerceContext(IConfiguration configuration)
        {
            var mongoSection = configuration.GetSection("MongoSettings");
            var connectionString = mongoSection["ConnectionString"];
            var databaseName = mongoSection["DatabaseName"];

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "MongoDB connection string is null or empty");
            }
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName), "MongoDB database name is null or empty");
            }

            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(settings);
            _database = client.GetDatabase(databaseName);
        }


        public IMongoCollection<CategoryM> Categories => _database.GetCollection<CategoryM>("Category");
        public IMongoCollection<ProductM> Products => _database.GetCollection<ProductM>("Product");
        public IMongoCollection<ProductDetailsM> ProductDetails => _database.GetCollection<ProductDetailsM>("ProductDetails");
        public IMongoCollection<UserM> Users => _database.GetCollection<UserM>("User");
    }
}
