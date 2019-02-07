using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Dtos.Carts;
using ModernShopping.Application.Exceptions;
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

		private async Task<Cart> GetUserCartEntity(string customerId, CancellationToken cancellationToken)
		{
            return await _context.Carts
				.Include(c => c.CartLines)
				.ThenInclude(cd => cd.Product)
				.FirstOrDefaultAsync(c => c.CustomerId == customerId, cancellationToken);
		}

		public async Task<CartDto> GetUserCart(string customerId, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (string.IsNullOrEmpty(customerId))
				throw new ArgumentNullException(nameof(customerId));

			var userCart = await GetUserCartEntity(customerId, cancellationToken) ?? new Cart();

			return userCart.Map(CartMapper.EntityToDtoFunc);
		}

		public async Task<CartLineDto> AddCartLine(string customerId, CartLineForCreationDto cartLine, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (string.IsNullOrEmpty(customerId))
				throw new ArgumentNullException(nameof(customerId));

			var userCart = await GetUserCartEntity(customerId, cancellationToken);

		    if (userCart == null)
		    {
		        userCart = new Cart { CustomerId = customerId };
		        _context.Add(userCart);
		    }

		    var result = userCart.AddCartLine(cartLine.Map(CartMapper.CartLineForCreationDtoToEntityFunc));

		    await _context.SaveChangesAsync(cancellationToken);

		    var cartLineDto = result.Map(CartMapper.CartLineEntityToDto);

			if (cartLineDto != null && string.IsNullOrEmpty(cartLineDto.ProductName))
				cartLineDto.ProductName = cartLine.ProductName;

			return cartLineDto;
		}

		public async Task DeleteCart(string customerId, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (string.IsNullOrEmpty(customerId))
				throw new ArgumentNullException(nameof(customerId));

			var userCart = await GetUserCartEntity(customerId, cancellationToken);
			if (userCart == null) 
				throw new EntityNotFoundException("Current user's cart does not exist!");

			userCart.CartLines.Clear();
			_context.Carts.Remove(userCart);

			await _context.SaveChangesAsync(cancellationToken);
		}

	}
}
