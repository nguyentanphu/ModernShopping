using System.Threading;
using System.Threading.Tasks;
using ModernShopping.Application.Dtos.Carts;

namespace ModernShopping.Application.Contracts
{
	public interface ICartService
	{
		Task<CartLineDto> AddCartLine(CartLineForCreationDto cartLine, CancellationToken cancellationToken = default(CancellationToken));
		Task<CartDto> GetUserCart(CancellationToken cancellationToken = default(CancellationToken));
	}
}