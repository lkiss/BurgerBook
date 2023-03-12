using System;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace BurgerBook_API.Models
{
	public class Location
	{
        [BsonElement("longitude")]
        [JsonPropertyName("lng")]
        public double Longitude { get; set; }

        [BsonElement("latitude")]
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

	}
}

