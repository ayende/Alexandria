namespace Alexandria.Messages
{
    public class UpdateDetailsResponse
    {
        public bool Success { get; set; }

        public string Key
        {
            get { return "UpdateDetails [UserId #" + UserId + "]"; }
        }

        public long UserId { get; set; }
    }
}