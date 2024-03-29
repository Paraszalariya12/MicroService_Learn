﻿using Ecomm_Service.ShoppingCartAPI.Models.dto;

namespace Ecomm_Service.ShoppingCartAPI.Models.DTO
{
    public class CartDetailDto
    {

        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public CartHeaderDto? CartHeader { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }= 0;
    }
}
