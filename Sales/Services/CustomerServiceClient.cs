using System.Net.Http.Json;
using SalesService.DTOs;

namespace SalesService.Services
{
    public class CustomerServiceClient
    {
        private readonly HttpClient _httpClient;

        public CustomerServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetCustomerNameAsync(Guid customerId)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomerDto>($"api/customers/{customerId}");
            return response?.CustomerName ?? "Unknown Customer";
        }
    }
}
