using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulesDirectDownloader
{
    class Options
    {
        [Option('f', "folder", HelpText = "The name of the download folder you would like to create", DefaultValue = "ChannelIcons")]
        public string Folder { get; set; }

        [Option('l', "logging", HelpText = "Turns logging on and off. Will write logfile to path")]
        public bool Logging { get; set; }

        [Option('p', "path", HelpText = "The folder path to download icons to")]
        public string Path { get; set; }

        [Option('u', "username", HelpText = "Your SchedulesDirect username")]
        public string Username { get; set; }        

        // For getting parsing errors
        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("SchedulesDirectIconDownloader", "1.0.0"),
                Copyright = new CopyrightInfo("Mikyle Baksh", 2016),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };

            help.AddOptions(this);

            if (this.LastParserState?.Errors.Any() == true)
            {
                var errors = help.RenderParsingErrorsText(this, 2); // indent with two spaces

                if (!string.IsNullOrEmpty(errors))
                {
                    help.AddPreOptionsLine(string.Concat(Environment.NewLine, "ERROR(S):"));
                    help.AddPreOptionsLine(errors);
                }
            }

            return help;
        }

    }
}
