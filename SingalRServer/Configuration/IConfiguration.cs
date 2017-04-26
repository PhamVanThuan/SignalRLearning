using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingalRServer.Configuration
{
    public interface IConfiguration
    {
        string GetSetting(string key);
    }
}
