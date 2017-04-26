using System;

namespace Models.Sportative
{
    public class PriceData
    {
        public DateTime TimeStamp1 { get; set; }
        public DateTime TimeStamp2 { get; set; }
        public int SiteId { get; set; }
        public int MarketId { get; set; }
        public int BetTypeId { get; set; }
        public string EventId { get; set; }
        public string MatchId { get; set; }
        public int HandicapKey { get; set; }
        public string OddsData { get; set; }
        //
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int HomeRedCard { get; set; }
        public int AwayRedCard { get; set; }
        public int MatchTimeHalf { get; set; }
        public int MatchTimeMinute { get; set; }
        //
        public int? FavouriteType { get; set; }

        //public MatchInformation MatchInformation { get; set; }

        public PriceData(string[] data)
        {
            TimeStamp1 = Convert.ToDateTime(data[0]);
            TimeStamp2 = Convert.ToDateTime(data[1]);
            SiteId = Convert.ToInt32(data[2]);
            MarketId = Convert.ToInt32(data[3]);
            BetTypeId = Convert.ToInt32(data[4]);
            EventId = data[5];
            MatchId = data[6];
            HandicapKey = Convert.ToInt32(data[7]);
            if (data.Length >= 9)
            {
                OddsData = data[8];
                //MatchInformation = data[9];
                //
                var infoSplit = data[9].Split('|');
                HomeScore = Convert.ToInt32(infoSplit[0]);
                AwayScore = Convert.ToInt32(infoSplit[1]);
                HomeRedCard = Convert.ToInt32(infoSplit[2]);
                AwayRedCard = Convert.ToInt32(infoSplit[3]);
                MatchTimeHalf = Convert.ToInt32(infoSplit[4]);
                MatchTimeMinute = Convert.ToInt32(infoSplit[5]);
                //
                var BetType_0_2 = OddsData.Split('^');
                FavouriteType = (BetType_0_2.Length > 4) ? (int?) Convert.ToInt32(BetType_0_2[4]) : null;
            }
        }
    }
}