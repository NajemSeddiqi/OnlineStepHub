using OnlineStep.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStep.Services
{
    public class Global
    {
        private Global() { }
        private static Lazy<Global> _instance = new Lazy<Global>(() => new
        Global());
        public static Global Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        public string ChapterId { get; set; }
        public string CourseId { get; set; }
        public List<Course> Courses { get; set; }
    }
}
