using Fusillade;
using ModernHttpClient;
using OnlineStep.Helpers;
using Refit;
using System;
using System.Net.Http;

namespace OnlineStep.Services
{
    public class TestRestClientHelper : IServiceHelper
    {
        public const string ApiBaseAddress = "https://online-step.herokuapp.com";
        //Lazy is used to improve performance by deffering an objects creation until it is first used (lazy initialization)
        private readonly Lazy<IOnlineStepApi> backGround;
        private readonly Lazy<IOnlineStepApi> userInitiated;
        private readonly Lazy<IOnlineStepApi> speculative;

        public TestRestClientHelper(string apiBaseAddress = null)
        {
            //Encapsulated method that takes a param (HttpMessageHandler) and returns a result (RestInterface)
            //we create and specify our httpClient here so that our RestInterface uses modern httpclient
            //Modern http client places our network stack on the appropriate stack on android and IOS
            //We do this because Android and IOS has their own optimized networking and we want each respective system to use them
            Func<HttpMessageHandler, IOnlineStepApi> createClient = messageHandler =>
            {
                var client = new HttpClient(messageHandler)
                {
                    BaseAddress = new Uri(ApiBaseAddress ?? apiBaseAddress)
                };
                return RestService.For<IOnlineStepApi>(client);
            };

            //The fusillade nuget provides the Priority feature for us to us
            backGround = new Lazy<IOnlineStepApi>(() => createClient(new RateLimitedHttpMessageHandler(new NativeMessageHandler(), Priority.Background)));
            userInitiated = new Lazy<IOnlineStepApi>(() => createClient(new RateLimitedHttpMessageHandler(new NativeMessageHandler(), Priority.UserInitiated)));
            speculative = new Lazy<IOnlineStepApi>(() => createClient(new RateLimitedHttpMessageHandler(new NativeMessageHandler(), Priority.Speculative)));
        }

        public IOnlineStepApi BackGround { get { return backGround.Value; } }
        public IOnlineStepApi UserInitiated { get { return userInitiated.Value; } }
        public IOnlineStepApi Speculative { get { return speculative.Value; } }
    }
}
