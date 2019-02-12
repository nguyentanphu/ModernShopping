using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Dtos.Carts;
using ModernShopping.Application.Exceptions;

namespace ModernShopping.Presentation.Controllers.Api
{
    
    public class CartController : ApiBaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CartDto>> GetCart(CancellationToken cancellationToken)
        {
            // No longer need to do this as using the [ApiController]
            //if (ModelState.IsValid)
            //    return BadRequest();

            return await _cartService.GetUserCart("CACTU", cancellationToken);
        }

        [HttpPost("cart-line")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CartLineDto>> AdjustCartLine(CartLineForCreationDto cartLine, CancellationToken cancellationToken)
        {
            return await _cartService.AddCartLine("CACTU", cartLine, cancellationToken);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCart(CancellationToken cancellationToken)
        {
            try
            {
                await _cartService.DeleteCart("CACTU", cancellationToken);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

    }
}
