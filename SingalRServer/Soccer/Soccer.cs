using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using Models.Sportative;
using Newtonsoft.Json;
using SingalRServer.Configuration;
using SingalRServer.Constants;
using SingalRServer.WebsyncService;

namespace SingalRServer.Soccer
{
    public class Soccer : ISoccer
    {
        private readonly IWebsynService _websynService;
        private readonly IConfiguration _configuration;
        private readonly ConcurrentDictionary<string, Event> _events = new ConcurrentDictionary<string, Event>();
        private const string SportType = "0";

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public Soccer(IHubConnectionContext<dynamic> client, IWebsynService websynService, IConfiguration configuration)
        {
            _websynService = websynService;
            _configuration = configuration;
            Clients = client;
            LoadEvents();
        }

        private void LoadEvents()
        {
            var channelName = string.Format(_configuration.GetSetting(SignalrServerConstants.EventsChannel), SportType);
            _websynService.Subscrible(channelName, ReceiveData);
        }

        private void ReceiveData(ReceiveData data)
        {
            Console.WriteLine("Receive data");
            if (!string.IsNullOrEmpty(data.Data))
            {
                List<Event> events = JsonConvert.DeserializeObject<string[]>(data.Data)
                    .Select(d => new Event(d.Split(';')))
                    .ToList();
                if (data.Action.StartsWith("rm-ev", StringComparison.Ordinal))
                {
                    //remove events
                }
                else
                {
                    foreach (var ev in events)
                    {
                        if (_events.Values.Count > 0)
                        {
                            if (!_events.ContainsKey(ev.EventId))
                            {
                                _events.TryAdd(ev.EventId, ev);
                                Clients.All.updateEvents(ev);
                            }
                        }
                        else
                        {
                            _events.TryAdd(ev.EventId, ev);
                        }
                    }
                }
            }
        }

        public IEnumerable<Event> GetEvents()
        {
            return _events.Values;
        }
    }
}