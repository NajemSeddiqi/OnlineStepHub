using System.Collections.Generic;

namespace OnlineStep.Models
{
    public class Course
    {
        public List<string> Chapters { get; set; }
        public string _id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Subject { get; set; }
        public string Img { get; set; }
        public int __v { get; set; }
    }
}
