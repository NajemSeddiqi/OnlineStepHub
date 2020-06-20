using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace OnlineStep.Models
{
    public class Mcq : IPage
    {
        /// <summary>
        /// This constructor is required for the JSON deserializer to be able
        /// to identify concrete classes to use when deserializing the interface properties.
        /// </summary>
        [JsonConstructor]
        public Mcq(string _id, string type, string title, string author, string image)
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
            [JsonProperty(PropertyName = "question")]
            public string question { get; set; }
            [JsonProperty(PropertyName = "answers")]
            public List<string> answers { get; set; }
            [JsonProperty(PropertyName = "correctAnswer")]
            public string correctAnswer { get; set; }
        }
    }

}
