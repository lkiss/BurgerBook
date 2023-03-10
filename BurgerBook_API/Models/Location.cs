using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BurgerBook_API.Models
{
	public class Location
	{
        [BsonElement("longitude")]
        public double Longitude { get; set; }

        [BsonElement("altitude")]
        public double Altitude { get; set; }

	}
}

