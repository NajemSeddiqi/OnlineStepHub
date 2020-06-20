using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using OnlineStep.Models;
using OnlineStep.Navigation.Interfaces;
using OnlineStep.Services;
using Xamarin.Forms;

namespace OnlineStep.ViewModels
{
    public class ScoreViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;

        public ScoreViewModel(INavigator navigator)
        {
            Debug.WriteLine("ScoreViewModel Constructor: ");
            _navigator = navigator;
            ChapterXp = PageNavigator.Xp.ToString();
            ChapterResult = PageNavigator.GetChapterResult();
            ResultMessage = "Gattis!";
        }

        public double Progress => PageNavigator.GetProgress();
        public string ChapterXp { get; set; }
        public string ChapterResult { get; set; }
        public string ResultMessage { get; set; }

        public ICommand GoToChapter => new Command(() =>
        {
            PageNavigator.ChapterCompleted();
            _navigator.PushAsync<ChapterViewModel>();
        });

    }
}
