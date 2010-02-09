namespace Alexandria.Messages
{
    public class UpdateAddress
    {
        public long UserId { get; set; }

        public AddressDTO Details { get; set; }

		public override string ToString()
		{
			return "UpdateAddress (UserId #" + UserId + ")";
		}
    }
}