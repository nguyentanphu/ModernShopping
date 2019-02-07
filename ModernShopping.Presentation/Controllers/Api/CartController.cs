using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Dtos.Carts;

namespace ModernShopping.Presentation.Controllers.Api
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class CartController : ApiBaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCart(CancellationToken cancellationToken)
        {
            return await _cartService.GetUserCart(cancellationToken);
        }

        [HttpPost("cart-line")]
        public async Task<ActionResult<CartLineDto>> AddCartLine(CartLineForCreationDto cartLine, CancellationToken cancellationToken)
        {
            return await _cartService.AddCartLine(cartLine, cancellationToken);
        }

    }
}
