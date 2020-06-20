using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OnlineStep.Helpers;
using OnlineStep.Models;
using OnlineStep.Services;
using OnlineStep.ViewModels;
using OnlineStep.Views;
using Refit;

namespace OnlineStep.Test

{
    [TestClass]
    public class DataCenterTest
    {
       
        [TestMethod]
        public async System.Threading.Tasks.Task FetchChaptersTest()
        {

            /*
             * Fetching chapters
             */

            //Arrenge
            string courseId = "5e3bd92155de5958085644e3";
            OnlineStepApiService apiFetcher = new OnlineStepApiService();
            List<ChapterLevel> chapterLevels = new List<ChapterLevel>();

            //Act
            chapterLevels = await apiFetcher.FetchChapterLevels(courseId);


            //Assert
            Assert.AreEqual(chapterLevels[0].Chapters[0]._id, "5e3976792e6b5d4ee4a2956f");

        }
    }
}
