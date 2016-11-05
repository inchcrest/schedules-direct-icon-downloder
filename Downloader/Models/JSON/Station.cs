using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulesDirect.IconDownloader.Models.JSON
{
    public class Station
    {
        public string stationID { get; set; }
        public string name { get; set; }
        public string callsign { get; set; }
        public List<string> broadcastLanguage { get; set; }
        public List<string> descriptionLanguage { get; set; }
        public Broadcaster broadcaster { get; set; }
        public string affiliate { get; set; }
        public bool? isCommercialFree { get; set; }
        public Logo logo { get; set; }
    }
}
