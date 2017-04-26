using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Sportative;

namespace SingalRServer.WebsyncService
{
    public interface IWebsynService
    {
        /// <summary>
        /// Subscribe channel by name
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="receiveData"></param>
        void Subscrible(string channelName, Action<ReceiveData> receiveData );

        void DisconnectFromWebsync();
    }
}