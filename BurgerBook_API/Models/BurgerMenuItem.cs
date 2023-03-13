using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BurgerBook_API.Models
{
	public class BurgerMenuItem
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("price")]
        public double? Price { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("picture")]
        public string? PictureUrl { get; set; }
	}
}

