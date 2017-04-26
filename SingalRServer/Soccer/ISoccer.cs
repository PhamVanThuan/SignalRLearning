using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using Models.Sportative;

namespace SingalRServer.Soccer
{
    public interface ISoccer
    {
        IEnumerable<Event> GetEvents();

        IEnumerable<PriceDataJson> GetPricesData();
    }
}
