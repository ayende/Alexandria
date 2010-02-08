namespace Alexandria.Messages
{
    public class UpdateDetails
    {
        public long UserId { get; set; }

        public SubscriptionDetailsDTO Details { get; set; }

		public override string ToString()
		{
			return "UpdateDetails (UserId #" + UserId + ")";
		}
    }
}