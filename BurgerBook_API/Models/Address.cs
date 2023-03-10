using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BurgerBook_API.Models
{
	public class Address
	{
        [BsonElement("zip")]
        public string? Zip { get; set; }

        [BsonElement("city")]
        public string? City { get; set; }

        [BsonElement("street")]
        public string? Street { get; set; }

        [BsonElement("addressline1")]
        public string? AddressLine1 { get; set; }

        [BsonElement("addressline2")]
        public string? AddressLine2 { get; set; }
	}
}

