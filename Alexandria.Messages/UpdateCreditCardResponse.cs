namespace Alexandria.Messages
{
    public class UpdateCreditCardResponse
    {
        public bool Success { get; set; }

        public string Key
        {
            get { return "UpdateCreditCard [UserId #" + UserId + "]"; }
        }

        public long UserId { get; set; }
    }
}