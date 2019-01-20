using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(IEnumerable<int> ids, CancellationToken cancellationToken)
        {
            if (!ids.Any()) return NotFound();

            var returnProducts = await _productService.GetProductsAsync(ids, cancellationToken);
            if (returnProducts.Count() != ids.Count())
                return NotFound();

            return Ok(returnProducts);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> CreateProduct(IEnumerable<ProductForCreationDto> newProduct, CancellationToken cancellationToken)
        {
            var products = await _productService.AddProductsAsync(newProduct, cancellationToken);
            var newProductIds = products.Select(p => p.ProductId);

            return CreatedAtAction("GetProducts", new {ids = string.Join(',', newProductIds)}, products);
        }
    }
}
