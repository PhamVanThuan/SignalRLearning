using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Event
    {
        public DateTime TimeStamp1 { get; set; }
        public DateTime TimeStamp2 { get; set; }
        public int SportId { get; set; }
        public string EventId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public int MarketId { get; set; }
        public DateTime MatchStartTime { get; set; }
        public int NeutralGround { get; set; }
    }
}