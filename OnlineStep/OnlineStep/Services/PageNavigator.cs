using OnlineStep.Models;
using OnlineStep.Navigation.Interfaces;
using OnlineStep.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OnlineStep.Services
{
    public static class PageNavigator
    {
        public static List<IPage> PageList { get; set; }
        public static int Index { get; set; }
        public static int Xp { get; set; }
        private static List<bool> _pageResults = new List<bool>();
        public static List<bool> PageResults { get => _pageResults; set => _pageResults = value; }

        public static string GetChapterResult()
        {
            string chapterResult = "";
            int correctAnswers = 0;
            foreach (bool pageResult in PageResults)
            {
                if (pageResult)
                {
                    correctAnswers++;
                }
            }
            chapterResult = correctAnswers.ToString() + "/" + PageList.Count.ToString();
            return chapterResult;
        }

        public static IPage GetCurrentPage
        {
            get
            {
                Debug.WriteLine("Getting page where index = " + Index);
                return PageList[Index];
            }
        }

        public static void PushNextPage(INavigator navigator)
        {

            Debug.WriteLine("Index: " + Index + "\nPageList Count: " + PageList.Count);

            if (PageList.Count == 0)
            {
                throw new System.ArgumentException("PageList cannot be null", "PageList");
            };

            // All pages has been displayed
            if (PageList.Count <= Index)
            {
                Debug.WriteLine("All pages has been displayed -> ScoreView");
                navigator.PushAsync<ScoreViewModel>();
            };

            // Displays next pages
            if (PageList.Count > 0 && PageList.Count > Index)
            {
                switch (PageList[Index].type.ToLower())
                {
                    case "mcq":
                        navigator.PushAsync<McqViewModel>();
                        Index++;
                        break;

                    case "cloze":
                        navigator.PushAsync<ClozeViewModel>();
                        Index++;
                        break;

                    default:
                        throw new System.ArgumentException("PageList", "Page type not found: " + PageList[Index].type);
                }
            }
        }
        public static void ChapterCompleted()
        {
            User.Instance.Xp += Xp;
            double correctAnswers = 0;
            foreach (bool pageResult in PageResults)
            {
                if (pageResult)
                {
                    correctAnswers++;
                }
            }
            double chapterProgress = correctAnswers / PageList.Count;
            Debug.WriteLine(chapterProgress);
            Debug.WriteLine(Global.Instance.ChapterId);
            bool _isSet = false;
            foreach (var cp in User.Instance.ChapterProgressList)
            {
                if (cp._id.Equals(Global.Instance.ChapterId))
                {
                    if (chapterProgress > cp.Progress)
                    {
                        cp.Progress = chapterProgress;
                    }
                    _isSet = true;
                    break;
                }
            }
            if (!_isSet)
            {
                User.Instance.ChapterProgressList.Add(new User.ChapterProgress(Global.Instance.ChapterId, chapterProgress, PageResults));
            }


            PageResults = new List<bool>();
            PageList = new List<IPage>();
            Index = 0;
            Xp = 0;
            Debug.WriteLine("Current User XP: " + User.Instance.Xp.ToString());

            foreach (var cp in User.Instance.ChapterProgressList)
            {
                Debug.WriteLine(cp._id + ": " + cp.Progress.ToString());
            }
        }

        public static float GetProgress()
        {
            if (Index == 0)
            {
                return 0.0f;
            }
            else
            {
                float progress = Index * (1 / (float)PageList.Count);
                return progress;
            }
        }
    }
}