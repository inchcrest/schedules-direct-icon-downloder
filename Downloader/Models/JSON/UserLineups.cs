using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulesDirect.IconDownloader.Models.JSON
{
   public class UserLineups
    {
        public int code { get; set; }
        public string serverID { get; set; }
        public string datetime { get; set; }
        public List<Lineup> lineups { get; set; }
    }


}
