using Instally.App.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Instally.App.DataServices
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSeriarizerOptions;

        public RestDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5272" : "http://localhost:5272";
            _url = $"{_baseAddress}/api";

            _jsonSeriarizerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        public async Task AddUserAsync(User user)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("----No internet acess----");
                return;
            }

            try
            {
                string jsonUser = JsonSerializer.Serialize(user, _jsonSeriarizerOptions);
                StringContent content = new StringContent(jsonUser, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/user", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("User successfully created");
                }
                else
                {
                    Debug.WriteLine("----No Http 2xx resonse----");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"----Exception throw: {ex.Message}----");
            }

            return;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new List<User>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("----No internet acess----");
                return users;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/user");
                
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    users = JsonSerializer.Deserialize<List<User>>(content, _jsonSeriarizerOptions);
                }
                else
                {
                    Debug.WriteLine("----No Http 2xx resonse----");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"----Exception throw: {ex.Message}----");
            }

            return users;
        }

        public async Task UpdateUserAsync(User user)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("----No internet acess----");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize(user, _jsonSeriarizerOptions);
                StringContent content = new StringContent(jsonToDo, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/user/{user.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("User successfully updated");
                }
                else
                {
                    Debug.WriteLine("----No Http 2xx resonse----");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"----Exception throw: {ex.Message}----");
            }

            return;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("----No internet acess----");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/user/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("User successfully deleted");
                }
                else
                {
                    Debug.WriteLine("----No Http 2xx resonse----");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"----Exception throw: {ex.Message}----");
            }

            return;
        }
    }
}
