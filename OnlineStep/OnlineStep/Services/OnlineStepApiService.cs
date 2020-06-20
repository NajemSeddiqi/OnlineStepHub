using Akavache;
using Fusillade;
using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OnlineStep.Helpers;
using OnlineStep.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;


namespace OnlineStep.Services
{
    public class OnlineStepApiService
    {
        //This is the class where everything comes together
        private readonly IServiceHelper ServiceHelper;
        private readonly Lazy<IOnlineStepApi> onlineStepApi;
        private IOnlineStepApi OnlineStepApi { get { return onlineStepApi.Value; } }
        private IBlobCache Cache;
        private const string url = "https://online-step.herokuapp.com";

        public OnlineStepApiService()
        {
            onlineStepApi = new Lazy<IOnlineStepApi>(() => createClient(new RateLimitedHttpMessageHandler(new NativeMessageHandler(), Priority.UserInitiated)));
        }

        public async Task<List<Course>> FetchCourses()
        {
            //Using the Akavache nuget we can store data from GetCoursesAsync() in our localMachine using a keyword "courses"
            //The offset method tells the cache how long we want the data to remain inside the cache
            //This method is different from FetchChapters and FetchPages because we run it when the app is loading in the start
            //Making the loading of courses much faster
            Cache = BlobCache.LocalMachine;
            IOnlineStepApi __onlineStepApi = RestService.For<IOnlineStepApi>("https://online-step.herokuapp.com");
            List<Course> Courses = await __onlineStepApi.GetCourses();
            return Courses;
        }

        //These methods are the ones we call from courseViewModel and ChapterViewModel
        public async Task<List<ChapterLevel>> FetchChapterLevels(string id)
        {
            Cache = BlobCache.LocalMachine;
            List<Chapter> getChaptersTask = await GetChaptersAsync(id);
            List<ChapterLevel> chapterLevels = FetchSortedLevels(getChaptersTask);
            await Cache.InsertObject("chapters", chapterLevels, DateTimeOffset.Now.AddHours(2));
            List<ChapterLevel> chapters = await Cache.GetObject<List<ChapterLevel>>("chapters");
            return chapters;
        }

        public async Task<List<IPage>> FetchPages(string id)
        {
            Cache = BlobCache.LocalMachine;
            List<IPage> getPagesTask = await GetPagesAsync(id);
            await Cache.InsertObject("pages", getPagesTask, DateTimeOffset.Now.AddHours(2));
            List<IPage> pages = await Cache.GetObject<List<IPage>>("pages");
            return pages;
        }

        //These methods call on our ServiceHelper which is implemented in RestInterface
        async Task<List<IPage>> GetPagesAsync(string id)
        {

            //JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            //jsonSerializerSettings.Converters.Add(new PageConverter());
            //var myApi = RestService.For<IOnlineStepApi>("https://online-step.herokuapp.com");
            //return await myApi.GetPages(id);

            Task<List<IPage>> getPagesTask = ServiceHelper.UserInitiated.GetPages(id);

            Debug.WriteLine(getPagesTask.Result);

           

            return await getPagesTask;
        }

        async Task<List<Chapter>> GetChaptersAsync(string id)
        {
            Task<List<Chapter>> getChaptersTask = OnlineStepApi.GetChapters(id);
            return await getChaptersTask;
        }

        async Task<List<Course>> GetCoursesAsync()
        {
            Task<List<Course>> getCourseTask = ServiceHelper.UserInitiated.GetCourses();
            return await getCourseTask;
        }

        //Had to create a local instance of this because the Request was not going through for chapters and pages
        Func<HttpMessageHandler, IOnlineStepApi> createClient = messageHandler =>
        {
            var client = new HttpClient(messageHandler)
            {
                BaseAddress = new Uri(url)
            };
            return RestService.For<IOnlineStepApi>(client);
        };

        private List<ChapterLevel> FetchSortedLevels(List<Chapter> chapterList)
        {

            List<ChapterLevel> listOfChapters = new List<ChapterLevel>();
            foreach (var chapter in chapterList)
            {
                while (int.Parse(chapter.Level) > listOfChapters.Count)
                {
                    listOfChapters.Add(new ChapterLevel() { Chapters = new List<Chapter>() });
                }
                listOfChapters[int.Parse(chapter.Level) - 1].Chapters.Add(chapter);
                listOfChapters[int.Parse(chapter.Level) - 1].Level = chapter.Level;
            }
            return listOfChapters;
        }

    }
}
