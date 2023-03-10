using System;
namespace BurgerBook_API.Models
{
	public class BurgerReview
	{
		public double OverallRating { get; set; }
		public double TasteRating { get; set; }
		public double TextureRating { get; set; }
		public double VisualRating { get; set; }
		public string Comment { get; set; }
		public byte[] Picture { get; set; }

		public BurgerReview()
		{
		}
	}
}

