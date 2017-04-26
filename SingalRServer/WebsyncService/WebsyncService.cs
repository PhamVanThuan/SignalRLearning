using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FM;
using FM.WebSync;
using Models.Sportative;
using Newtonsoft.Json;
using SingalRServer.Configuration;
using SingalRServer.Constants;

namespace SingalRServer.WebsyncService
{
    public class WebsyncService : IWebsynService
    {
        private Client _client;
        private IConfiguration _configuration;

        public WebsyncService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectToWebsync();
        }

        private void ConnectToWebsync()
        {
            _client = new Client(_configuration.GetSetting(SignalrServerConstants.GenesisUrl));
            _client.Connect(new ConnectArgs
            {
                OnSuccess = e => { Console.WriteLine("Websync connected"); },
                OnFailure = e =>
                {
                    Console.WriteLine(e.ErrorMessage);
                    e.Retry = true;
                },
                OnStreamFailure = e =>
                {
                    Console.WriteLine(e.ErrorMessage);
                    e.Retry = true;
                },
                MetaJson =
                    Json.Serialize(new Authentication
                    {
                        SN = _configuration.GetSetting(SignalrServerConstants.GenesisSn),
                        SK = _configuration.GetSetting(SignalrServerConstants.GenesisSk)
                    })
            });
        }

        public void Subscrible(string channelName, Action<ReceiveData> callback)
        {
            _client.Subscribe(
                new SubscribeArgs(channelName)
                {
                    OnSuccess = data =>
                    {
                        var message = FromJson<ReceiveData>(data.GetExtensionValueJson("data"));
                        callback(message);
                    },
                    OnReceive = data =>
                    {
                        var message = FromJson<ReceiveData>(data.DataJson);
                        callback(message);
                    }
                });
        }

        public void DisconnectFromWebsync()
        {
            if (!_client.IsConnected) return;
            _client.Disconnect();
        }

        private static T FromJson<T>(string json)
        {
            var data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }
    }
}