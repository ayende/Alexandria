namespace Alexandria.Messages
{
    using System;

    public class SearchResponse : ICachableResponse
    {
        public BookDTO[] Books { get; set; }

        public long UserId { get; set; }

        public string Key
        {
            get { return "Search (UserId #" + UserId + ")"; }
        }

        public DateTime Timestamp { get; set; }
    }
}