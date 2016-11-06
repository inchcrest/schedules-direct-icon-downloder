using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchedulesDirect.IconDownloader
{
    using Newtonsoft.Json;
    using SchedulesDirect.IconDownloader.Constants;
    using SchedulesDirect.IconDownloader.Helpers;
    using SchedulesDirect.IconDownloader.Models;
    using SchedulesDirect.IconDownloader.Models.JSON;
    using System.Net.Http.Headers;

    public class IconDownloader
    {
        public IconDownloader()
        {
            throw new NotImplementedException();          
        }

        public IconDownloader(string path, string folder, string username, bool logging)
        {
            Credentials = new Credentials();
            Token = new Token();

            if (String.IsNullOrEmpty(path))
            {
                this.Path = AppDomain.CurrentDomain.BaseDirectory;
            }
            else if (!Uri.IsWellFormedUriString(path, UriKind.Absolute))
            {
                throw new ArgumentException("Path is invalid");
            }
            this.Folder = folder;
            this.Credentials.Username = username;
            this.Logging = logging;
        }

        public async Task DoWork()
        {
            PromptUserNamePassword();
            GetToken().Wait();
            UserLineups userLineups = await GetUserLineups();
            var lineup = PromptChooseLineup(userLineups);
            if(lineup.lineup == null)
            {
                throw new ArgumentNullException("Lineup was null!");
            }
            var channelMappingLineup = await GetChannelMappingLineup(lineup);
            DownloadIcons(channelMappingLineup);
        }

        #region HTTP Requests
        private async Task GetToken()
        {
            using (var client = new HttpClient().SchedulesDirectPostTokenRequest())
            {
                var postParams = new Dictionary<string, string>
                {
                    { PostParameters.Username, Credentials.Username },
                    { PostParameters.Password, Credentials.Password }
                };

                var serializedPostParams = await Task.Run(() => JsonConvert.SerializeObject(postParams));

                var content = new StringContent(serializedPostParams);
                content.Headers.TryAddWithoutValidation(PostHeaders.ContentType, PostHeaders.ContentTypeValue);                    

                var response = await client.PostAsync(Endpoints.RequestTokenEndpoint, content);
                if(response.Content == null)
                {
                    throw new ArgumentNullException("Response Content Null.");
                }
                var responseContent = await response.Content.ReadAsStringAsync();

                Token = await Task.Run(() => JsonConvert.DeserializeObject<Token>(responseContent));
            }
        }

        private async Task<UserLineups> GetUserLineups()
        {
            using (var client = new HttpClient().SchedulesDirectGetRequest(Token))
            {
                var response = await client.GetAsync(Endpoints.RequestUserLineupsEndpoint);
                if (response.Content == null)
                {
                    throw new ArgumentNullException("Response Content Null.");
                }
                var responseContent = await response.Content.ReadAsStringAsync();

                return await Task.Run(() => JsonConvert.DeserializeObject<UserLineups>(responseContent));
            }
        }

        private async Task<ChannelMappingLineup> GetChannelMappingLineup(Lineup lineup)
        {
            using (var client = new HttpClient().SchedulesDirectGetRequest(Token))
            {
                var response = await client.GetAsync(Endpoints.RequestChannelMappingLineupEndpoint + lineup.lineup);
                if (response.Content == null)
                {
                    throw new ArgumentNullException("Response Content Null.");
                }
                var responseContent = await response.Content.ReadAsStringAsync();

                return await Task.Run(() => JsonConvert.DeserializeObject<ChannelMappingLineup>(responseContent));
            }
        }
        
        private void DownloadIcons(ChannelMappingLineup channelMappingLineup)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Prompts
        private void PromptUserNamePassword()
        {
            if(String.IsNullOrEmpty(Credentials.Username))
            {
                Console.Write("Enter your SchedulesDirect username: ");
                Credentials.Username = Console.ReadLine();
            }
            
            Console.Write("Enter your SchedulesDirect password: ");
            Credentials.Password = ConsolePassword.ReadPassword();
        }

        private Lineup PromptChooseLineup(UserLineups userLineups)
        {
            int count = 0;
            int selectionInt;
            bool validSelection = false;

            Lineup selectedLineup = new Lineup();

            while(!validSelection)
            {
                Console.WriteLine("Please select a lineup to download icons from");

                foreach (var lineup in userLineups.lineups)
                {
                    Console.WriteLine(String.Format("[{0}]", count));
                    Console.WriteLine(lineup.lineup.PadLeft(4));
                    Console.WriteLine(lineup.name.PadLeft(4));
                    Console.WriteLine(lineup.transport.PadLeft(4));
                    Console.WriteLine(lineup.location.PadLeft(4));
                    Console.WriteLine(lineup.uri.PadLeft(4));
                    Console.WriteLine(lineup.isDeleted.ToString().PadLeft(4));
                }
                Console.Write(">");

                var selection = Console.ReadLine();

                if (Int32.TryParse(selection, out selectionInt))
                {
                    if (selectionInt > userLineups.lineups.Count)
                    {
                        Console.WriteLine("Choose a valid selection");
                        validSelection = false;
                    }
                    else
                    {
                        validSelection = true;
                        selectedLineup = userLineups.lineups[selectionInt];
                    }
                }
                else
                {
                    Console.WriteLine("Choose a valid selection");
                    validSelection = false;
                }
            }
            return selectedLineup;
        }
        #endregion


        // PRIVATE FIELDS
        #region Private Fields
        private Credentials Credentials;
        private Token Token;
        private string Folder;
        private string Path;
        private bool Logging;
        #endregion
    }
}
