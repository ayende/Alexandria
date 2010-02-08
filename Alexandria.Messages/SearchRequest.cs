namespace Alexandria.Messages
{
    public class SearchRequest : ICachableRequest
    {
        public long UserId { get; set; }

        public string Query { get; set; }

        public string Key
        {
            get { return "Search (UserId #" + UserId + ")"; }
        }

        public override string ToString()
        {
            return Key;
        }
    }
}