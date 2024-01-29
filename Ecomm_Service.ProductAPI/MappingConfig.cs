using AutoMapper;
using Ecomm_Service.ProductAPI.Models;
using Ecomm_Service.ProductAPI.Models.dto;

namespace Ecomm_Service.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {

                config.CreateMap<Product, ProductDto>();
                config.CreateMap<ProductDto, Product>();
            });

            return mappingConfig;
        }
    }
}
