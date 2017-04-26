using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Models.Models;
using Models.Sportative;
using SingalRServer.Soccer;

namespace SingalRServer.Hubs
{
    [HubName("soccerHub")]
    public class SoccerHub: Hub
    {
        private readonly ISoccer _soccer;

        public SoccerHub(ISoccer soccer)
        {
            _soccer = soccer;
        }

        public IEnumerable<Event> GetEvents()
        {
            return _soccer.GetEvents();
        }

    }
}
