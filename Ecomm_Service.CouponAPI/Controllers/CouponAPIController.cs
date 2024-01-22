using AutoMapper;
using Ecomm_Service.CouponAPI.Data;
using Ecomm_Service.CouponAPI.Models;
using Ecomm_Service.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using static Azure.Core.HttpHeader;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Ecomm_Service.CouponAPI.Controllers
{
    [Route("api/Coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ResponseDto responseDto;
        private IMapper _mapper;
        public CouponAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _context = db;
            responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                var objcoupons = _context.coupons.ToList();
                if (objcoupons != null && objcoupons.Count > 0)
                {
                    responseDto.IsSuccess = true;
                    responseDto.Data = JsonConvert.SerializeObject(_mapper.Map<List<CouponDto>>(objcoupons));
                }
                else
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "No data found !!";
                }

            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
            }

            return responseDto;
        }

        [HttpGet]
        [Route("{CouponId:int}")]
        public ResponseDto Get(int CouponId)
        {
            try
            {
                var objcoupons = _context.coupons.Where(a => a.CouponId == CouponId).FirstOrDefault();
                if (objcoupons != null)
                {
                    responseDto.IsSuccess = true;
                    responseDto.Data = JsonConvert.SerializeObject(_mapper.Map<CouponDto>(objcoupons));
                }
                else
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "No data found !!";
                }
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
            }

            return responseDto;
        }

        [HttpGet]
        [Route("GetByCode/{CouponCode}")]
        public ResponseDto GetByCode(string CouponCode)
        {
            try
            {
                var objcoupons = _context.coupons.FirstOrDefault(a => a.CouponCode == CouponCode);
                if (objcoupons != null)
                {
                    responseDto.IsSuccess = true;
                    responseDto.Data = JsonConvert.SerializeObject(_mapper.Map<CouponDto>(objcoupons));
                }
                else
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "No data found !!";
                }
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                _context.coupons.Add(coupon);
                _context.SaveChanges();
                if (coupon.CouponId > 0)
                {
                    responseDto.IsSuccess = true;
                    responseDto.Data = JsonConvert.SerializeObject(_mapper.Map<CouponDto>(coupon));
                }
                else
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "No data Inserted !!";
                }
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
            }

            return responseDto;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                if (_context.coupons.Any(a => a.CouponId == couponDto.CouponId))
                {
                    var objcoupon = _mapper.Map<Coupon>(couponDto);

                    _context.Update(objcoupon);
                    _context.SaveChanges();
                    responseDto.IsSuccess = true;
                    responseDto.Data = JsonConvert.SerializeObject(_mapper.Map<CouponDto>(objcoupon));
                }
                else
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "No data Found for Update !!";
                }
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
            }

            return responseDto;
        }

        [HttpDelete]
        [Route("{CouponId:int}")]
        public ResponseDto Delete(int CouponId)
        {
            try
            {
                var objcoupon = _context.coupons.First(a => a.CouponId == CouponId);
                if (objcoupon != null)
                {
                    _context.coupons.Remove(objcoupon);
                    _context.SaveChanges();

                    responseDto.IsSuccess = true;
                    responseDto.Data = JsonConvert.SerializeObject(_mapper.Map<CouponDto>(objcoupon));
                }
                else
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "No data Found for Delete !!";
                }
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
            }

            return responseDto;
        }
    }

}
