using AutoMapper;
using Ecomm_Service.ProductAPI.Data;
using Ecomm_Service.ProductAPI.Models;
using Ecomm_Service.ProductAPI.Models.dto;
using Ecomm_Service.ProductAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ecomm_Service.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductAPIController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private ResponseDto responseDto;
        private IMapper _mapper;
        public ProductAPIController(ApplicationDbContext db, IMapper mapper)
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
                var objproducts = _context.products.ToList();
                if (objproducts != null && objproducts.Count > 0)
                {
                    responseDto.IsSuccess = true;
                    responseDto.Data = JsonConvert.SerializeObject(_mapper.Map<List<ProductDto>>(objproducts));
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
        [Route("{ProductId:int}")]
        public ResponseDto Get(int ProductId)
        {
            try
            {
                var objproducts = _context.products.Where(a => a.ProductId == ProductId).FirstOrDefault();
                if (objproducts != null)
                {
                    responseDto.IsSuccess = true;
                    responseDto.Data = JsonConvert.SerializeObject(_mapper.Map<ProductDto>(objproducts));
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
        [Authorize(Roles = "Admin")]
        public ResponseDto Post(ProductDto productDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                _context.products.Add(product);
                _context.SaveChanges();

                if (productDto.Image != null)
                {

                    string fileName = product.ProductId + Path.GetExtension(productDto.Image.FileName);
                    string filePath = @"wwwroot\ProductImages\" + fileName;

                    //I have added the if condition to remove the any image with same name if that exist in the folder by any change
                    var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    FileInfo file = new FileInfo(directoryLocation);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        productDto.Image.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                    product.ImageLocalPath = filePath;
                }
                else
                {
                    product.ImageUrl = "https://placehold.co/600x400";
                }

                if (product.ProductId > 0)
                {
                    responseDto.IsSuccess = true;
                    responseDto.Data = JsonConvert.SerializeObject(_mapper.Map<ProductDto>(product));
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
        [Authorize(Roles = "Admin")]
        public ResponseDto Put(ProductDto productDto)
        {
            try
            {
                if (_context.products.Any(a => a.ProductId == productDto.ProductId))
                {
                    var objproduct = _mapper.Map<Product>(productDto);
                    if (productDto.Image != null)
                    {
                        if (!string.IsNullOrEmpty(objproduct.ImageLocalPath))
                        {
                            var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), objproduct.ImageLocalPath);
                            FileInfo file = new FileInfo(oldFilePathDirectory);
                            if (file.Exists)
                            {
                                file.Delete();
                            }
                        }

                        string fileName = objproduct.ProductId + Path.GetExtension(productDto.Image.FileName);
                        string filePath = @"wwwroot\ProductImages\" + fileName;
                        var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                        using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                        {
                            productDto.Image.CopyTo(fileStream);
                        }
                        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                        objproduct.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                        objproduct.ImageLocalPath = filePath;
                    }
                    _context.Update(objproduct);
                    _context.SaveChanges();
                    responseDto.IsSuccess = true;
                    responseDto.Data = JsonConvert.SerializeObject(_mapper.Map<ProductDto>(objproduct));
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
        [Route("{productId:int}")]
        [Authorize(Roles = "Admin")]
        public ResponseDto Delete(int productId)
        {
            try
            {
                var objproduct = _context.products.First(a => a.ProductId == productId);
                if (objproduct != null)
                {
                    string ImageLocalPath = objproduct.ImageLocalPath;
                    _context.products.Remove(objproduct);
                    _context.SaveChanges();

                    if (!string.IsNullOrEmpty(ImageLocalPath))
                    {
                        var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), ImageLocalPath);
                        FileInfo file = new FileInfo(oldFilePathDirectory);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                    responseDto.IsSuccess = true;
                    responseDto.Data = JsonConvert.SerializeObject(_mapper.Map<ProductDto>(objproduct));
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
