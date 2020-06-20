using OnlineStep.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStep.Helpers
{
    //This class uses the Refit package, which is a type-safe REST lib for xamarin and .Net
    [Headers("Accept: application/json")]
    public interface IOnlineStepApi
    {
        [Get("/courses")]
        Task<List<Course>> GetCourses();

        [Get("/courses/chapters/{id}")]
        Task<List<Chapter>> GetChapters(string id);

        [Get("/chapters/pages/{id}")]
        Task<List<IPage>> GetPages(string id);
    }
}
