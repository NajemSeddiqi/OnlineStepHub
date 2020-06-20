using Nest;
using Newtonsoft.Json;

namespace OnlineStep.Models
{
    public interface IPage
    {
        [JsonProperty(PropertyName = "_id")]
        string _id { get; set; }
        [JsonProperty(PropertyName = "type")]
        string type { get; set; }
        [JsonProperty(PropertyName = "title")]
        string title { get; set; }
        [JsonProperty(PropertyName = "author")]
        string author { get; set; }
        [JsonProperty(PropertyName = "image")]
        string image { get; set; }
    }

}
