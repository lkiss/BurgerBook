using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BurgerBook_API.Models
{
	public class BurgerMenu
	{
        [BsonElement("burgers")]
        public List<BurgerMenuItem>? Burgers { get; set; }
	}
}

