using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.FileTest
{
    public class TestConnection
    {
        static void Main(string[] args)
        {
            const string connectionUri = "mongodb+srv://kajumisu:S%40ophaixoan123@ecommercecqrsclutster.bzsm0sj.mongodb.net/?retryWrites=true&w=majority&appName=ECommerceCQRSClutster";
            const string dbName = "ECommerceDb";

            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);

            try
            {
                var result = database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Success!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
