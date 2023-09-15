using System.Net.Http;
using System.Threading.Tasks;

namespace InstallyApp.Application.Functions
{
    public static class API
    {
        public static string BaseUrl = "https://instally.app/api";

        public static async Task<string> Get(string pathname)
        {
            var client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync(BaseUrl + pathname);
            
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
