using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchedulesDirect.IconDownloader
{
    using SchedulesDirect.IconDownloader.Constants;
    using SchedulesDirect.IconDownloader.Helpers;
    using SchedulesDirect.IconDownloader.Models;
    using SchedulesDirect.IconDownloader.Models.JSON;


    public class IconDownloader
    {
        public IconDownloader()
        {
            Credentials = new Credentials();
            Token = new Token();
            // Setup HTTP objs here
        }
        
        public void DoWork()
        {
            GetUserNamePassword();
            GetToken();
        }

        private void GetUserNamePassword()
        {
            Console.WriteLine("Enter your user name");
            Credentials.UserName = Console.ReadLine();
            Console.WriteLine("Enter your password");
            Credentials.Password = ConsolePassword.ReadPassword();
        }

        private async void GetToken()
        {
            using (var client = new HttpClient())
            {
                var postParams = new Dictionary<string, string>
                {
                   { PostParameters.Username, Credentials.UserName },
                   { PostParameters.Password, Credentials.Password }
                };

                var content = new FormUrlEncodedContent(postParams);

                var response = await client.PostAsync(Endpoints.RequestTokenEndpoint, content);

                var responseString = await response.Content.ReadAsStringAsync();
            }
        }

        
        // PRIVATE FIELDS
        #region Private Fields
        private Credentials Credentials;
        private Token Token;
        #endregion
    }
}
