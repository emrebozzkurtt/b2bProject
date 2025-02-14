﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Concrete
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : GenericController<Product>
    {
        IProductService _productService;

        public ProductsController(IProductService productService) : base(productService)
        {
            _productService = productService;
        }


        [HttpGet("getproductdetailbyid")]
        public IActionResult GetProductDetail(int id)
        {
            var result = _productService.GetProductDetail(id);
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpGet("getproductbycategoryid")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            var result = _productService.GetByCategoryId(categoryId);
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }

        [HttpGet("getproductbysupplierid")]
        public IActionResult GetBySupplierId(int supplierId)
        {
            var result = _productService.GetBySupplierId(supplierId);
            return result.Success
                ? Ok(result.Data)
                : BadRequest(result.Message);
        }
    }
}
