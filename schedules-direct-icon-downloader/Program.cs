using CommandLine;
using CommandLine.Text;
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
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                try
                {
                    var downloader = new IconDownloader(options.Path, options.Folder, options.Username, options.Logging);
                    downloader.DoWork().Wait();
                    Console.ReadKey();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Exiting...");
                    
                }
                
            }
            else
            {
                // Display the default usage information
                Console.WriteLine(options.GetUsage());
            }


            
        }
    }
}
