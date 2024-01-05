using System.Net.Http;

namespace Instally.App.Application.Functions
{
    public static class API
    {
        public static string SiteUrl = "https://instally.app";
        public static string BaseUrl = $"{SiteUrl}/api";

        public static async Task<string> Get(string pathname)
        {
            try
            {
                var client = new HttpClient();
                using HttpResponseMessage response = await client.GetAsync(BaseUrl + pathname);
            
                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
    }
}
