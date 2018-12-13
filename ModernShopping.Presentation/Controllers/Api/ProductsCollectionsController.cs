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
    public class ProductsCollectionsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsCollectionsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("({ids})")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(IEnumerable<int> ids)
        {
            if (!ids.Any()) return NotFound();

            var returnProducts = await _productService.GetProducts(ids);
            if (returnProducts.Count() != ids.Count())
                return NotFound();

            return Ok(returnProducts);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> CreateProduct(IEnumerable<ProductForCreationDto> newProduct)
        {
            var result = await _productService.AddProducts(newProduct);
            var newProductIds = result.Products.Select(p => p.ProductId);

            if (result.IsAdded)
                return CreatedAtAction("GetProducts", new {ids = string.Join(',', newProductIds)}, result.Products);

            return BadRequest();
        }
    }
}
