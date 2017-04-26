using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin.Cors;
using Ninject;
using Owin;
using SingalRServer.Configuration;
using SingalRServer.Hubs;
using SingalRServer.Soccer;
using SingalRServer.WebsyncService;

namespace SingalRServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var kernel = new StandardKernel();

            var resolver = new NinjectSignalRDependencyResolver(kernel);

            kernel.Bind<IConfiguration>().To<Configuration.Configuration>();
            kernel.Bind<IWebsynService>().To<WebsyncService.WebsyncService>().InSingletonScope();
            kernel.Bind<ISoccer>().To<Soccer.Soccer>().InSingletonScope();

            kernel.Bind(typeof(IHubConnectionContext<dynamic>)).ToMethod(context =>
                        resolver.Resolve<IConnectionManager>().GetHubContext<SoccerHub>().Clients
            ).WhenInjectedInto<ISoccer>();

            var config = new HubConfiguration {Resolver = resolver};
            ConfigureSignalR(app, config);
        }

        private void ConfigureSignalR(IAppBuilder app, HubConfiguration config)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR("/s", config);
        }
    }
}