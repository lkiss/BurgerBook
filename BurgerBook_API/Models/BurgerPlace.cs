using System;
namespace BurgerBook_API.Models
{
	public class BurgerPlace
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public double Rating { get; set; }
		public Address Address { get; set; }
		public Location Location { get; set; }
		public BurgerMenu Menu { get; set; }
		public List<BurgerReview> Reviews { get; set; }
		
	}
}

