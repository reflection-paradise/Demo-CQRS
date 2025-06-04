using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntitiesM
{
    public class ProductDetailsM
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("productId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("specifications")]
        public string Specifications { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }
    }
}
