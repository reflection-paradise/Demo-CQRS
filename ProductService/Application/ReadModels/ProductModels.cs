using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReadModels
{
    public class ProductModels
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("productName")]
        public string ProductName { get; set; }

        [BsonElement("categoryId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
