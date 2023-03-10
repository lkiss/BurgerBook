using System;
namespace BurgerBook_API.Models
{
	public class Address
	{
		public string Zip { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }

        public Address()
		{
		}
	}
}

