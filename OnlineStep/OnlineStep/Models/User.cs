using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStep.Models
{
    public class User
    {
        private User() { }
        private static Lazy<User> _instance = new Lazy<User>(() => new
        User());
        private List<ChapterProgress> _chapterProgressList = new List<ChapterProgress>();
        public static User Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        public int Xp { get; set; }
        public List<ChapterProgress> ChapterProgressList 
        { 
            get => _chapterProgressList;
            set => _chapterProgressList = value;
        }
        public class ChapterProgress
        {
            public ChapterProgress(string chapterId, double progress, List<bool> pageResults)
            {
                _id = chapterId;
                Progress = progress;
                PageResults = pageResults;
            }
            public string _id { get; set; }
            public double Progress { get; set; }
            public List<bool> PageResults { get; set; }
        }
    }
}