using System;
using BurgerBook.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BurgerBook_API.Models
{
	public class BurgerPlace
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

		[BsonElement("name")]
		public string? Name { get; set; }

        [BsonElement("address")]
        public Address? Address { get; set; }

        [BsonElement("location")]
        public Location? Location { get; set; }

        [BsonElement("menu")]
        public BurgerMenu? Menu { get; set; }

        [BsonElement("openinghours")]
        public Dictionary<string, OpeningHours>? OpeningHours { get; set; }

    }
}

