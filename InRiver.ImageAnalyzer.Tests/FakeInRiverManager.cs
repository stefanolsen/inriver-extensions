using inRiver.Remoting;

namespace InRiver.ImageAnalyzer.Tests
{
    public class FakeInRiverManager : IinRiverManager
    {
        public FakeInRiverManager()
        {
            DataService = new FakeDataService();
            ModelService = new FakeModelService();
            UtilityService = new FakeUtilityService();
        }

        public IDataService DataService { get; }
        public IChannelService ChannelService { get; }
        public IModelService ModelService { get; }
        public IUserService UserService { get; }
        public IUtilityService UtilityService { get; }
        public IPrintService PrintService { get; }
    }
}
