namespace OnlineStep.Helpers
{
        //It's better to use this interface to reach our DbService so the logic inside it is separated
        //Methods added here must be implemented in TestRestClientHelper
        public interface IServiceHelper
        {
            IOnlineStepApi Speculative { get; }
            IOnlineStepApi UserInitiated { get; }
            IOnlineStepApi BackGround { get; }           
        }
}
