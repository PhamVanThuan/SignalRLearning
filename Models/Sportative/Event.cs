using System;

namespace Models.Sportative
{
    [DataContract]
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

        public Event(string[] data)
        {
            TimeStamp1 = Convert.ToDateTime(data[0]);
            TimeStamp2 = Convert.ToDateTime(data[1]);
            SportId = Convert.ToInt32(data[2]);
            EventId = data[3];

            if (data.Length > 4)
            {
                HomeTeam = data[4];
                AwayTeam = data[5];
                LeagueId = Convert.ToInt32(data[6]);
                LeagueName = data[7];
                MarketId = Convert.ToInt32(data[8]);
                MatchStartTime = DateTime.Parse(data[9]);
                NeutralGround = Convert.ToInt32(data[10]);
            }
        }
    }
}
