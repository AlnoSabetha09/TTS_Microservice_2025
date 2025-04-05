using System.Net.Http.Json;
using SalesService.DTOs;

namespace SalesService.Services
{
    public class ProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetProductNameAsync(Guid productId)
        {
            var response = await _httpClient.GetFromJsonAsync<ProductDto>($"api/products/{productId}");
            return response?.ProductName ?? "Unknown Product";
        }
    }
}
