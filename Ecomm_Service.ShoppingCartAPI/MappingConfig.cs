using AutoMapper;
using Ecomm_Service.ShoppingCartAPI.Models;
using Ecomm_Service.ShoppingCartAPI.Models.DTO;

namespace Ecomm_Service.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {

                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                //config.CreateMap<CartHeaderDto, CartHeader>();

                config.CreateMap<CartDetail, CartDetailDto>().ReverseMap();
                //config.CreateMap<CartDetailDto, CartDetail>();
            });

            return mappingConfig;
        }
    }
}
