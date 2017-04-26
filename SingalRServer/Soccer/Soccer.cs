using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using Models.Models;
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
        private readonly ConcurrentDictionary<string, League> _leagues = new ConcurrentDictionary<string, League>();

        private readonly ConcurrentDictionary<string, PriceDataJson> _prices =
            new ConcurrentDictionary<string, PriceDataJson>();

        private const string SportType = "0";

        private IHubConnectionContext<dynamic> Clients { get; set; }

        public Soccer(IHubConnectionContext<dynamic> client, IWebsynService websynService, IConfiguration configuration)
        {
            _websynService = websynService;
            _configuration = configuration;
            Clients = client;
            LoadEvents();
            //LoadPrices();
        }

        private void LoadPrices()
        {
            var channelName = string.Format(_configuration.GetSetting(SignalrServerConstants.PricesChannel), SportType,
                0, 1);
            _websynService.Subscrible(channelName, ReceivePriceData);
        }

        private void ReceivePriceData(ReceiveData data)
        {
            if (!string.IsNullOrEmpty(data.Data))
            {
                List<PriceDataJson> prices = JsonConvert.DeserializeObject<string[]>(data.Data)
                    .Select(d => new PriceDataJson(d.Split(';')))
                    .ToList();
                if (data.Action.StartsWith("rm-ev", StringComparison.Ordinal))
                {
                    //remove events
                }
                else
                {
                    foreach (var priceData in prices)
                    {
                    }
                }
            }
        }

        private void LoadEvents()
        {
            var channelName = string.Format(_configuration.GetSetting(SignalrServerConstants.EventsChannel), SportType);
            _websynService.Subscrible(channelName, ReceiveData);
        }

        private void ReceiveData(ReceiveData data)
        {
            if (!string.IsNullOrEmpty(data.Data))
            {
                List<EventJson> events = JsonConvert.DeserializeObject<string[]>(data.Data)
                    .Select(d => new EventJson(d.Split(';')))
                    .ToList();
                if (data.Action.StartsWith("rm-ev", StringComparison.Ordinal))
                {
                    foreach (var ev in events)
                    {
                        if (_events.ContainsKey(ev.EventId))
                        {
                            Event eventRemoved;
                            _events.TryRemove(ev.EventId, out eventRemoved);
                            Console.WriteLine($"Event {eventRemoved.EventId} was removed");
                            Clients.All.removeEvent(eventRemoved);
                        }
                    }
                }
                else
                {
                    foreach (var ev in events)
                    {
                        if (!_events.ContainsKey(ev.EventId))
                        {
                            var newEvent = new Event
                            {
                                EventId = ev.EventId,
                                AwayTeam = ev.AwayTeam,
                                HomeTeam = ev.HomeTeam,
                                LeagueId = ev.LeagueId,
                                LeagueName = ev.LeagueName,
                                MarketId = ev.MarketId,
                                SportId = ev.SportId,
                                MatchStartTime = ev.MatchStartTime,
                                NeutralGround = ev.NeutralGround,
                                TimeStamp1 = ev.TimeStamp1,
                                TimeStamp2 = ev.TimeStamp2
                            };
                            _events.TryAdd(ev.EventId, newEvent);
                            Clients.All.updateEvents(newEvent);
                        }
                    }
                }
            }
        }

        public IEnumerable<Event> GetEvents()
        {
            return _events.Values;
        }

        public IEnumerable<PriceDataJson> GetPricesData()
        {
            return _prices.Values;
        }
    }
}