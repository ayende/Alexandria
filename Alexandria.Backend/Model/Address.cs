namespace Alexandria.Backend.Model
{
	public class Address
	{
		public virtual string Street { get; set; }
		public virtual string Country { get; set; }
		public virtual string City { get; set; }
		public virtual string ZipCode { get; set; }
		public virtual string HouseNumber { get; set; }
	}
}