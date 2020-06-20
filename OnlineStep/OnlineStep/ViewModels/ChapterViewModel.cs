using Newtonsoft.Json;
using OnlineStep.Helpers;
using OnlineStep.Models;
using OnlineStep.Navigation.Interfaces;
using OnlineStep.Services;
using Refit;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;


namespace OnlineStep.ViewModels
{
    class ChapterViewModel : BaseViewModel
    {
        private readonly OnlineStepApiService Service = new OnlineStepApiService();
        private readonly INavigator _navigator;
        private Data Data;

        public ChapterViewModel(INavigator navigator)
        {
            _ = InitAsyncApiRequest();
            _navigator = navigator;
        }

        private void SetCurrentUserProgress()
        {
            Debug.WriteLine("SetCurrentUserProgress start: ");
            List<User.ChapterProgress> chapterProgressList = User.Instance.ChapterProgressList;
            double minScoreTreshold = 0.5;
            for (int i = 0; i < ChapterLevels.Count; i++)
            {
                if (ChapterLevels[i].Level.Equals("1"))
                {
                    ChapterLevels[i].Locked = false;
                }
                else
                {
                    ChapterLevels[i].Locked = !ChapterLevels[i - 1].Chapters.All(chapter => chapterProgressList.Any(chapterProgress => chapterProgress._id.Equals(chapter._id) && chapterProgress.Progress >= minScoreTreshold));
                    //Uncomment to unlock all levels for testing
                    //ChapterLevels[i].Locked = false;
                }

                ChapterLevels[i].Chapters.All(chapter => chapter.Locked = ChapterLevels[i].Locked);
                Debug.WriteLine("Level " + ChapterLevels[i].Level + " Locked: " + ChapterLevels[i].Locked);

                for (int j = 0; j < ChapterLevels[i].Chapters.Count; j++)
                {
                    double pagesResult = 0;
                    foreach (var chapterProgress in chapterProgressList)
                    {
                        if (chapterProgress._id.Equals(ChapterLevels[i].Chapters[j]._id))
                        {
                            int correctAnswers = 0;
                            foreach (bool pageResult in chapterProgress.PageResults)
                            {
                                if (pageResult)
                                {
                                    correctAnswers++;
                                }
                            }
                            pagesResult = (double)correctAnswers / ChapterLevels[i].Chapters[j].Pages.Count;
                        }
                    }
                    ChapterLevels[i].Chapters[j].PagesResult = pagesResult;
                }
            }
            Debug.WriteLine("SetCurrentUserProgress end: ");
        }


        //Empty constructor used by Unit Test
        public ChapterViewModel() { }

        public List<ChapterLevel> ChapterLevels { get; set; }
        public List<IPage> Pages { get; set; }

        public bool IsLocked(string _id)
        {
            foreach (var item in User.Instance.ChapterProgressList)
            {
                if (item._id.Equals(_id) && item.Progress >= 0.8)
                {
                    return false;
                }
            }
            return true;
        }

        private async System.Threading.Tasks.Task InitAsyncApiRequest()
        {
            ChapterLevels = await Service.FetchChapterLevels(Global.Instance.CourseId);
            SetCurrentUserProgress();
        }
        private async System.Threading.Tasks.Task<List<IPage>> LoadPages(string id)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new PageConverter());
            var myApi = RestService.For<IOnlineStepApi>("https://online-step.herokuapp.com",
                new RefitSettings
                {
                    ContentSerializer = new JsonContentSerializer(jsonSerializerSettings)
                });
            return await myApi.GetPages(id);
        }

        public ICommand GoToPageView => new Command<string>(async (chapterId) =>
        {

            if (ChapterLevels.Any(chapterLevel => chapterLevel.Chapters.Any(chapter => chapter._id.Equals(chapterId) && !chapterLevel.Locked)))
            {
                Global.Instance.ChapterId = chapterId;
                PageNavigator.PageList = await LoadPages(chapterId.ToString());
                PageNavigator.Index = 0;
                PageNavigator.PushNextPage(_navigator);
            }
        });


    }
}
