using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulesDirect.IconDownloader.Constants
{
    public static class Endpoints
    {
        public static string RequestTokenEndpoint = "https://json.schedulesdirect.org/20141201/token";
        public static string RequestUserLineupsEndpoint = "https://json.schedulesdirect.org/20141201/lineups";
        public static string RequestChannelMappingLineupEndpoint = "https://json.schedulesdirect.org/20141201/lineups/";
    }
}
