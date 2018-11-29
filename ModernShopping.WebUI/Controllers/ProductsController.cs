using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Dtos;
using ModernShopping.Persistence;
using ModernShopping.Persistence.Entities;

namespace ModernShopping.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
		private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
	    {
		    return await _productService.GetProductById(id);
	    }

	    [HttpGet]
	    [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
	    {
		    return Ok(await _productService.GetProducts());
	    }
	}
}