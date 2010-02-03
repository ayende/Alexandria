using System;

namespace Alexandria.Messages
{
	[Serializable]
	public class SubscriptionDetailsDTO
	{
		public string Name { get; set; }
		public string Street { get; set; }
		public string HouseNumber { get; set; }
		public string City { get; set; }
		public string ZipCode { get; set; }
		public string Country { get; set; }
		public int NumberOfPossibleBooksOut { get; set; }
		public decimal MonthlyCost { get; set; }
		public string CreditCard { get; set; }
	}
}