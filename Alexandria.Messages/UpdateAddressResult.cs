namespace Alexandria.Messages
{
    public class UpdateAddressResult
    {
        public bool Success { get; set; }
		
		public string ErrorMessage { set; get; }

        public long UserId { get; set; }
    }
}