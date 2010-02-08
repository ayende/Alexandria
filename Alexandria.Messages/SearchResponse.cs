namespace Alexandria.Messages
{
    using System;

    public class SearchResponse : ICacheableResponse
    {
        public BookDTO[] Books { get; set; }

        public long UserId { get; set; }

        public string Key
        {
            get { return "Search (" + Query+ ")"; }
        }

        public string Query { get; set; }

        public DateTime Timestamp { get; set; }
    }
}