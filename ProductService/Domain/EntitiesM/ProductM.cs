using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntitiesM
{
    public class ProductM
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("productName")]
        public string ProductName { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("categoryId")]
        public string CategoryId { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    
}
}
