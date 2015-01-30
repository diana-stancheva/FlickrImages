namespace FlickrImages.Models
{
    using Newtonsoft.Json;

    public class ItemMediaModel
    {
        [JsonProperty("m")]
        public string M { get; set; }
    }
}
