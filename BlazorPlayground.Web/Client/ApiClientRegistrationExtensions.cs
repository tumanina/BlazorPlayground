using System.Net.Http.Headers;

namespace BlazorPlayground.Web.Client
{
    public static class ApiClientRegistrationExtensions
    {

        public static ApiClientConfiguration GetApiClientConfiguration(this IConfiguration userConfigSection, string clientName)
        {
            var apiConfiguration = userConfigSection.Get<ApiClientConfiguration>();
            return apiConfiguration;
        }

        public static void ConfigureClient(this HttpClient client, ApiClientConfiguration apiConfiguration)
        {
            client.BaseAddress = new Uri(apiConfiguration.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(apiConfiguration.TimeoutInSeconds);
        }
    }
}