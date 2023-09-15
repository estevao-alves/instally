using System.Net.Http;
using System.Threading.Tasks;

namespace InstallyApp.Application.Functions
{
    public static class API
    {
        public static string SiteUrl = "https://instally.app";
        public static string BaseUrl = $"{SiteUrl}/api";

        public static async Task<string> Get(string pathname)
        {
            var client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync(BaseUrl + pathname);
            
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
