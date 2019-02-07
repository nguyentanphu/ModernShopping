using System.Threading;
using System.Threading.Tasks;
using ModernShopping.Application.Dtos.Carts;

namespace ModernShopping.Application.Contracts
{
	public interface ICartService
	{
		Task<CartLineDto> AddCartLine(string customerId, CartLineForCreationDto cartLine, CancellationToken cancellationToken = default(CancellationToken));
		Task<CartDto> GetUserCart(string customerId, CancellationToken cancellationToken = default(CancellationToken));
		Task DeleteCart(string customerId, CancellationToken cancellationToken = default(CancellationToken));
	}
}