using CustomerMVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace CustomerMVC.Services
{
    public class CustomerServices
    {
        private HttpClient _httpClient;
        public CustomerServices(HttpClient httpClient)
        {
                _httpClient = httpClient;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7105/api/Customer");
            response.EnsureSuccessStatusCode();
            var content= await response.Content.ReadAsStringAsync();
         
            return JsonConvert.DeserializeObject<List<Customer>>(content);
        }
        public async Task<Customer> GetCustomerAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7105/api/Customer/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Customer>(content);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7105/api/Customer", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7105/api/Customer/{customer.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7105/api/Customer/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}

