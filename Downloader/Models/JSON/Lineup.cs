using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulesDirect.IconDownloader.Models.JSON
{
    public class Lineup
    {
        public string lineup { get; set; }
        public string name { get; set; }
        public string transport { get; set; }
        public string location { get; set; }
        public string uri { get; set; }
        public bool? isDeleted { get; set; }
    }

}
