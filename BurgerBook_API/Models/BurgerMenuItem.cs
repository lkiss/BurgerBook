using System;
namespace BurgerBook_API.Models
{
	public class BurgerMenuItem
	{
		public string Name { get; set; }
		public double Price { get; set; }
		public string Description { get; set; }
		public byte[] Picture { get; set; }

		public BurgerMenuItem()
		{
		}
	}
}

