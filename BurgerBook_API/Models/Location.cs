using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BurgerBook_API.Models
{
	public class Location
	{
        [BsonElement("longitude")]
        public double Longitude { get; set; }

        [BsonElement("latitude")]
        public double Latitude { get; set; }

	}
}

