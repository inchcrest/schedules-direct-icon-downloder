using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulesDirectDownloader
{
    using SchedulesDirect.IconDownloader;

    class Program
    {
        static void Main(string[] args)
        {
            // parse command args here


            var downloader = new IconDownloader();
            downloader.DoWork();
        }
    }
}
