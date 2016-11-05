using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SchedulesDirect.IconDownloader.Helpers
{
    using SchedulesDirect.IconDownloader.Models.JSON;
    using SchedulesDirect.IconDownloader.Constants;

    public static class HttpClientExtensions
    {
        public static HttpClient SchedulesDirectPostTokenRequest(this HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(PostHeaders.AcceptValue));
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US", 0.8));
            client.DefaultRequestHeaders.UserAgent.TryParseAdd(PostHeaders.UserAgentValue);

            return client;
        }

        public static HttpClient SchedulesDirectGetRequest(this HttpClient client, Token token)
        {
            if(String.IsNullOrWhiteSpace(token.token))
            {
                throw new ArgumentNullException("Token was null. Request will not be sent");
            }

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(PostHeaders.AcceptValue));
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US", 0.8));
            client.DefaultRequestHeaders.UserAgent.TryParseAdd(PostHeaders.UserAgentValue);
            client.DefaultRequestHeaders.Add(PostHeaders.Token, token.token);

            return client;
        }
    }
}
