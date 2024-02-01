﻿using Ecomm_Service.ShoppingCartAPI.Models.dto;
using Ecomm_Service.ShoppingCartAPI.Models.DTO;
using Ecomm_Service.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Ecomm_Service.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/product");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto?>(apiContet);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.Data));
            }
            return new List<ProductDto>();
        }
    }
}
