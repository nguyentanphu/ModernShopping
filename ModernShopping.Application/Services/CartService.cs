using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Dtos.Carts;
using ModernShopping.Application.Utils.Mappers;
using ModernShopping.Persistence;
using ModernShopping.Persistence.Entities;

namespace ModernShopping.Application.Services
{
	public class CartService : ICartService
	{
		private readonly NorthwindContext _context;

		public CartService(NorthwindContext context)
		{
			_context = context;
		}

		private async Task<Cart> GetUserCartEntity(CancellationToken cancellationToken)
		{
            return await _context.Carts
				.Include(c => c.CartLines)
				.ThenInclude(cd => cd.Product)
				.FirstOrDefaultAsync(c => c.CustomerId == "CACTU", cancellationToken);
		}

		public async Task<CartDto> GetUserCart(CancellationToken cancellationToken = default(CancellationToken))
		{
			var userCart = await GetUserCartEntity(cancellationToken);

			return userCart.Map(CartMapper.EntityToDtoFunc);
		}

		public async Task<CartLineDto> AddCartLine(CartLineDto cartLine, CancellationToken cancellationToken = default(CancellationToken))
		{
		    var userCart = await GetUserCartEntity(cancellationToken);

		    if (userCart == null)
		    {
		        userCart = new Cart { CustomerId = "CACTU" };
		        _context.Add(userCart);
		    }

		    var result = userCart.AddCartLine(cartLine.Map(CartMapper.CartLineDtoToEntityFunc));

		    await _context.SaveChangesAsync(cancellationToken);

		    return result.Map(CartMapper.CartLineEntityToDto);
		}

	}
}
