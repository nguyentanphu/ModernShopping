using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Dtos.Products;
using ModernShopping.Presentation.ModelBinders;

namespace ModernShopping.Presentation.Controllers.Api
{
    [Route("api/products-collections")]
    [ApiController]
    public class ProductsCollectionsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsCollectionsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("({ids})")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(IEnumerable<int> ids)
        {
            if (!ids.Any()) return NotFound();

            var returnProducts = await _productService.GetProducts(ids);
            if (returnProducts.Count() != ids.Count())
                return NotFound();

            return Ok(returnProducts);
        }
    }
}
