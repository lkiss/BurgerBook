using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BurgerBook_API.Models
{
	public class BurgerMenuItem
	{
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

