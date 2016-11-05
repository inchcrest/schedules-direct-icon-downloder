using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulesDirect.IconDownloader.Models.JSON
{
    public class ChannelMappingLineup
    {
        public List<Map> map { get; set; }
        public List<Station> stations { get; set; }
        public Metadata metadata { get; set; }
    }

}
