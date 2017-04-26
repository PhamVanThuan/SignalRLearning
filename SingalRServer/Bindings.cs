using Ninject.Modules;
using SingalRServer.Configuration;
using SingalRServer.WebsyncService;

namespace SingalRServer
{
    public class Bindings: NinjectModule
    {
        public override void Load()
        {
            Bind<IConfiguration>().To<Configuration.Configuration>();
            Bind<IWebsynService>().To<WebsyncService.WebsyncService>().InSingletonScope();
        }
    }
}
