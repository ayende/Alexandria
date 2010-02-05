namespace Alexandria.Messages
{
    public class UpdateDetailsRequest
    {
        public long UserId { get; set; }

        public SubscriptionDetailsDTO Details { get; set; }

        public string Key
        {
            get { return "UpdateDetails (UserId #" + UserId + ")"; }
        }

		public override string ToString()
		{
			return Key;
		}
    }
}