using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BurgerBook_API.Models
{
	public class BurgerReview
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("burgerplaceid")]
        public string BurgerPlaceId { get; set; }

        [BsonElement("overallrating")]
        public double OverallRating { get; set; }

        [BsonElement("tasterating")]
        public double TasteRating { get; set; }

        [BsonElement("texturerating")]
        public double TextureRating { get; set; }

        [BsonElement("visualrating")]
        public double VisualRating { get; set; }

        [BsonElement("comment")]
        public string? Comment { get; set; }

        [BsonIgnore]
        public string? PictureUrl { get; set; }
	}
}

