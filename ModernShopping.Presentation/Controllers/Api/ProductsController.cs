using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Dtos;
using ModernShopping.Application.Dtos.Products;
using ModernShopping.Application.Exceptions;

namespace ModernShopping.Presentation.Controllers.Api
{
    public class ProductsController : ApiBaseController
    {
		private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdAsync(id, cancellationToken);
            if (product == null)
                return NotFound();

            return product;
	    }

	    [HttpGet]
	    [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(CancellationToken cancellationToken)
	    {
		    return Ok(await _productService.GetProductsAsync(cancellationToken: cancellationToken));
	    }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _productService.DeleteProductAsync(id, cancellationToken);

                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductForCreationDto newProduct, CancellationToken cancellationToken)
        {
            var product = await _productService.AddProductAsync(newProduct, cancellationToken);
            
            return CreatedAtAction("GetProduct", new {id = product.ProductId}, product);

        }
	}
}
