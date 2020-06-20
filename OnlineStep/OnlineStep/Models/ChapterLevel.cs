using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStep.Models
{
    public class ChapterLevel
    {
        public List<Chapter> Chapters { get; set; }
        public string Level { get; set; }
        public bool Locked { get; set; }

        public bool UnLocked {
            get {
                return !Locked;
            } 
        }
    }
}
