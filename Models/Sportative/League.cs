using System.Collections.Generic;

namespace Models.Sportative
{
    public class League
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public int SportTypeId { get; set; }
        public int MarketId { get; set; }
        public List<string> EventId { get; set; }
    }
}