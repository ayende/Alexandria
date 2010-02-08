namespace Alexandria.Messages
{
    public class SearchRequest : ICacheableRequest
    {
        public long UserId { get; set; }

        public string Query { get; set; }

        public string Key
        {
            get { return "Search (" + Query + ")"; }
        }

        public override string ToString()
        {
            return Key;
        }
    }
}