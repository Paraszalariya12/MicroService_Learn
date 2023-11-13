using AutoMapper;
using Ecomm_Service.CouponAPI.Models;
using Ecomm_Service.CouponAPI.Models.DTO;

namespace Ecomm_Service.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {

                config.CreateMap<Coupon, CouponDto>();
                config.CreateMap<CouponDto, Coupon>();
            });

            return mappingConfig;
        }
    }
}
