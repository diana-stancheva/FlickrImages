namespace FlickrImages.Models
{
    using System;

    using Newtonsoft.Json;

    [JsonObject("items")]
    public class FeedItemModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("media")]
        public ItemMediaModel Media { get; set; }

         [JsonProperty("date_taken")]
        public DateTime DateTaken { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("published")]
        public DateTime Published { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("author_id")]
        public string AuthorID { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }
    }
}