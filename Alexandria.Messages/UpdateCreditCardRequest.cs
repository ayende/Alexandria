namespace Alexandria.Messages
{
    public class UpdateCreditCardRequest
    {
        public long UserId { get; set; }

        public string CreditCard { get; set; }

        public string Key
        {
            get { return "UpdateCreditCard [UserId #" + UserId + "]"; }
        }
    }
}