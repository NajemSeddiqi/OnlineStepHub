using System.Collections.Generic;

namespace OnlineStep.Models
{
    public class Chapter
    {
        public List<string> Pages { get; set; }
        public string _id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Subjects { get; set; }
        public string Icon { get; set; }
        public string Level { get; set; }
        public int __v { get; set; }
        public string Subject { get; set; }
        public bool Locked { get; set; }
        public double PagesResult { get; set; }
    }
}