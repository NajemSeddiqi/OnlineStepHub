using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace OnlineStep.Models
{
    public class Cloze : IPage
    {
        /// <summary>
        /// This constructor is required for the JSON deserializer to be able
        /// to identify concrete classes to use when deserializing the interface properties.
        /// </summary>
        [JsonConstructor]
        public Cloze(string _id, string type, string title, string author, string image)
        {
            this._id = _id;
            this.type = type;
            this.title = title;
            this.author = author;
            this.image = image;
        }
        [JsonProperty(PropertyName = "content")]
        public Content content { get; set; }
        [JsonProperty(PropertyName = "_id")]
        public string _id { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string type { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }
        [JsonProperty(PropertyName = "author")]
        public string author { get; set; }
        [JsonProperty(PropertyName = "image")]
        public string image { get; set; }

        public class Content
        {
            [JsonProperty(PropertyName = "missingWords")]
            public List<string> missingWords { get; set; }
            [JsonProperty(PropertyName = "sentence")]
            public string sentence { get; set; }
            [JsonProperty(PropertyName = "image")]
            public string image { get; set; }
        }
    }
}
