﻿using Ecomm_Service.ShoppingCartAPI.Models.dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomm_Service.ShoppingCartAPI.Models
{
    public class CartDetail
    {
        [Key]
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public ProductDto Product { get; set; }
        public int Count { get; set; }
    }
}
