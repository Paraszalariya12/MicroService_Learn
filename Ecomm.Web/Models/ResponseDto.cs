﻿namespace Ecomm.Web.Models
{
    public class ResponseDto
    {
        public object? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
