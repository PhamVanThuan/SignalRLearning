using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Models.Sportative;
using Ninject;
using SingalRServer.Configuration;
using SingalRServer.Constants;
using SingalRServer.WebsyncService;

namespace SingalRServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            IConfiguration _configuration = kernel.Get<IConfiguration>();
            IWebsynService _websynService = kernel.Get<IWebsynService>();

            Console.WriteLine(_configuration.GetSetting(SignalrServerConstants.GenesisSk));

            _websynService.Subscrible("/events/0", ReceiveData);

            Console.ReadLine();
        }

        private static void ReceiveData(ReceiveData receiveData)
        {
            Console.WriteLine(receiveData.Data);
        }
    }
}
