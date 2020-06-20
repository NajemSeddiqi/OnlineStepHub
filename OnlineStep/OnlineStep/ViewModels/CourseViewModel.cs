using OnlineStep.Models;
using OnlineStep.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using OnlineStep.Helpers;
using OnlineStep.Navigation.Interfaces;
using Xamarin.Forms;

namespace OnlineStep.ViewModels
{
    internal class CourseViewModel : BaseViewModel
    {
        public List<Course> Courses { get; set; }
        private readonly INavigator _navigator;
        private Data Data;


        public CourseViewModel(INavigator navigator)
        {
            InitAsyncApiRequest();
            _navigator = navigator;                  
        }
        public void InitAsyncApiRequest()
        {
            Courses = Global.Instance.Courses;
            Debug.WriteLine("Courses fetched from Global Instance: " + Courses.Count);
            foreach(Course course in Courses){course.Name = course.Name.ToUpper();}
        }

        public ICommand GoToChapters => new Command<string>((id) =>
        {
            Global.Instance.CourseId = id;
            _navigator.PushAsync<ChapterViewModel>();
        });
            
    }
};

