using ZeKju.App.Helper;

namespace ZeKju.App
{
    public class Startup
    {
        IServiceProvider _serviceProvider;
        public Startup(IServiceProvider ServiceProvider)
        {
            _serviceProvider = ServiceProvider;
        }
        public async Task RunAsync()
        {
            MenuHelper.HandelMenu(_serviceProvider);
        }
    }
}
