using AutoMapper;
using Azure;
using Ecomm_Service.ShoppingCartAPI.Data;
using Ecomm_Service.ShoppingCartAPI.Models;
using Ecomm_Service.ShoppingCartAPI.Models.DTO;
using Ecomm_Service.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecomm_Service.ShoppingCartAPI.Controllers
{
    [Route("api/shoppingcart")]
    [ApiController]
    public class ShoppingCartAPIController : ControllerBase
    {
        readonly ApplicationDbContext _context;
        IMapper _mapper;
        ResponseDto _responseDto;
        readonly IProductService _productService;
        readonly ICouponService _couponService;
        public ShoppingCartAPIController(ApplicationDbContext context, IMapper mapper, IProductService productService, ICouponService couponService)
        {
            _context = context;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _productService = productService;
            _couponService = couponService;
        }

        [HttpGet("GetCart/{UserId}")]
        public async Task<IActionResult> GetCartByUserId(string UserId)
        {
            try
            {
                CartDto objcart = new CartDto();

                objcart.CartHeader = _mapper.Map<CartHeaderDto>(_context.cartHeaders.FirstOrDefault(a => a.UserId == UserId));
                if (objcart.CartHeader != null)
                {
                    objcart.CartDetails = _mapper.Map<IEnumerable<CartDetailDto>?>(_context.cartDetails.Where(a => a.CartHeaderId == objcart.CartHeader.CartHeaderId).ToList());
                }
                else
                {
                    objcart.CartDetails = new List<CartDetailDto>();
                }
                var lstproducts = await _productService.GetProducts();
                foreach (var item in objcart.CartDetails)
                {
                    var objProduct = lstproducts?.FirstOrDefault(a => a.ProductId == item.ProductId);
                    if (objProduct != null)
                    {
                        item.Product = objProduct;
                        objcart.CartHeader.CartTotal += (objProduct.Price * item.Count);
                    }
                }

                if (objcart.CartHeader != null && !string.IsNullOrEmpty(objcart.CartHeader.CouponCode))
                {
                    CouponDto couponDto = await _couponService.GetCoupon(objcart.CartHeader.CouponCode);
                    if (couponDto != null && couponDto.MinAmount <= objcart.CartHeader.CartTotal)
                    {
                        objcart.CartHeader.Discount = couponDto.DiscountAmount;
                        objcart.CartHeader.CartTotal -= couponDto.DiscountAmount;
                    }
                }

                _responseDto.IsSuccess = true;
                _responseDto.Message = "";
                _responseDto.Data = objcart;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message.ToString();
            }
            return Ok(_responseDto);

        }


        [HttpPost("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            try
            {
                var cartheaderdetails = _context.cartHeaders.FirstOrDefault(a => a.UserId == cartDto.CartHeader.UserId);
                if (cartheaderdetails != null)
                {
                    cartheaderdetails.CouponCode = cartDto.CartHeader.CouponCode;
                    await _context.SaveChangesAsync();
                    _responseDto.Message = "";
                    _responseDto.IsSuccess = true;
                }
                else
                {
                    _responseDto.Message = "No Cart Header Found..!!";
                    _responseDto.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }
            return Ok(_responseDto);
        }

        [HttpPost("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            try
            {
                var cartheaderdetails = _context.cartHeaders.FirstOrDefault(a => a.UserId == cartDto.CartHeader.UserId);
                if (cartheaderdetails != null)
                {
                    cartheaderdetails.CouponCode = "";
                    await _context.SaveChangesAsync();
                    _responseDto.Message = "";
                    _responseDto.IsSuccess = true;
                }
                else
                {
                    _responseDto.Message = "No Cart Header Found..!!";
                    _responseDto.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }
            return Ok(_responseDto);
        }

        [HttpPost("CartUpsert")]
        public async Task<IActionResult> CartUpsert(CartDto cartDto)
        {
            try
            {
                var cartheaderdetails = _context.cartHeaders.FirstOrDefault(a => a.UserId == cartDto.CartHeader.UserId);
                if (cartheaderdetails != null)
                {
                    var objcartdetailsexists = _context.cartDetails.FirstOrDefault(a => a.ProductId == cartDto.CartDetails.First().ProductId && a.CartHeaderId == cartheaderdetails.CartHeaderId);
                    if (objcartdetailsexists != null)
                    {
                        objcartdetailsexists.Count = objcartdetailsexists.Count + cartDto.CartDetails.First().Count;
                        await _context.SaveChangesAsync();
                        _responseDto.IsSuccess = true;
                        _responseDto.Message = "Item Added to Cart";

                        cartDto.CartDetails.First().Count = objcartdetailsexists.Count;
                        _responseDto.Data = cartDto;
                    }
                    else
                    {
                        CartDetail objcartdetail = _mapper.Map<CartDetail>(cartDto.CartDetails.First());
                        objcartdetail.CartHeaderId = cartheaderdetails.CartHeaderId;
                        _context.cartDetails.Add(objcartdetail);
                        await _context.SaveChangesAsync(true);
                        _responseDto.IsSuccess = true;
                        _responseDto.Message = "Item Added to Cart";

                        cartDto.CartDetails.First().CartHeaderId = cartheaderdetails.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = objcartdetail.CartDetailsId;
                        cartDto.CartDetails.First().Count = objcartdetail.Count;
                        _responseDto.Data = cartDto;
                    }
                }
                else
                {
                    CartHeader objcartheader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _context.cartHeaders.Add(objcartheader);
                    await _context.SaveChangesAsync();

                    CartDetail objcartdetail = _mapper.Map<CartDetail>(cartDto.CartDetails.First());
                    objcartdetail.CartHeaderId = objcartheader.CartHeaderId;
                    _context.cartDetails.Add(objcartdetail);
                    await _context.SaveChangesAsync(true);
                    _responseDto.IsSuccess = true;
                    _responseDto.Message = "Item Added to Cart";

                    cartDto.CartDetails.First().CartHeaderId = objcartheader.CartHeaderId;
                    cartDto.CartDetails.First().CartDetailsId = objcartdetail.CartDetailsId;
                    cartDto.CartDetails.First().Count = objcartdetail.Count;
                    _responseDto.Data = cartDto;
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }
            return Ok(_responseDto);
        }


        [HttpDelete("DeleteCartDetail")]
        public async Task<IActionResult> DeleteCartDetail(int cartDetailId)
        {
            try
            {
                CartDetail cartDetails = _context.cartDetails
                   .First(u => u.CartDetailsId == cartDetailId);

                int totalCountofCartItem = _context.cartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
                _context.cartDetails.Remove(cartDetails);
                if (totalCountofCartItem == 1)
                {
                    var cartHeaderToRemove = await _context.cartHeaders
                       .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);

                    _context.cartHeaders.Remove(cartHeaderToRemove);
                }
                await _context.SaveChangesAsync();

                _responseDto.Data = true;

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message.ToString();
            }
            return Ok(_responseDto);
        }
    }
}
