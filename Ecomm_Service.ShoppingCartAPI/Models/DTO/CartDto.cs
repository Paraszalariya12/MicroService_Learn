﻿namespace Ecomm_Service.ShoppingCartAPI.Models.DTO
{
    public class CartDto
    {
        public CartHeaderDto? CartHeader { get; set; }
        public IEnumerable<CartDetailDto>? CartDetails { get; set; }
    }
}
