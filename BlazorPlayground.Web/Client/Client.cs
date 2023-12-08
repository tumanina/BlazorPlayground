using BlazorPlayground.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace BlazorPlayground.Web.Client
{
    public class Client : IClient
    {
        private readonly HttpClient _client;

        public Client(ApiClientConfiguration apiClientConfiguration)
        {
            _client = new HttpClient();
            _client.ConfigureClient(apiClientConfiguration);
        }


        public async Task CreateOrUpdateUser(User user)
        {
            await PutAsync($"users", user);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await GetListAsync<User>("users");
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await GetAsync<User>($"users/{userId}");
        }

        public async Task<List<Department>> GetUserDepartmentsById(Guid userId)
        {
            return await GetListAsync<Department>($"users/{userId}/departments");
        }

        public async Task AddDepartmentsToUser(Guid userId, List<Department> departments)
        {
            await PostAsync($"users/{userId}/departments", departments);
        }

        public async Task DeleteUserDepartment(Guid userId, Department department)
        {
            await DeleteAsync($"users/{userId}/departments/department");
        }

        private async Task<List<T>> GetListAsync<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(_client.BaseAddress, url));
            request.Headers.Add("Accept", "application/json");

            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<T>>(content);
            }

            throw new ClientException((int)response.StatusCode, content);
        }

        private async Task<T> GetAsync<T>(string url)
            where T : class, new()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(_client.BaseAddress, url));
            request.Headers.Add("Accept", "application/json");

            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(content);
            }

            throw new ClientException((int)response.StatusCode, content);
        }

        public async Task PostAsync(string url, object content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(_client.BaseAddress, url));
            request.Headers.Add("Accept", "application/json");
            request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new ClientException((int)response.StatusCode, responseContent);
        }

        public async Task PutAsync(string url, object content)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, new Uri(_client.BaseAddress, url));
            request.Headers.Add("Accept", "application/json");
            request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new ClientException((int)response.StatusCode, responseContent);
        }

        public async Task DeleteAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(_client.BaseAddress, url));
            request.Headers.Add("Accept", "application/json");

            var response = await _client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new ClientException((int)response.StatusCode, responseContent);
        }
    }
}