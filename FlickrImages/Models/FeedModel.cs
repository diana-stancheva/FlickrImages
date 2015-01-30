namespace FlickrImages.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class FeedModel
    {
        private ICollection<FeedItemModel> items = new List<FeedItemModel>();

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("generator")]
        public string Generator { get; set; }

        [JsonProperty("items")]
        public ICollection<FeedItemModel> Items
        {
            get
            {
                return this.items;
            }

            set
            {
                this.items = value;
            }
        }
    }
}