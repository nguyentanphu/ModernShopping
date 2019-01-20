using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModernShopping.Application.Common;
using ModernShopping.Application.Dtos;
using ModernShopping.Application.Dtos.Products;

namespace ModernShopping.Application.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(IEnumerable<int> ids = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<ProductDto> GetProductByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteProductAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
	    Task<ProductDto> AddProductAsync(ProductForCreationDto newProduct, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<ProductDto>> AddProductsAsync(IEnumerable<ProductForCreationDto> newProducts, CancellationToken cancellationToken = default(CancellationToken));
	    Task<IEnumerable<LabelValueObject>> GetSupplierSourceAsync(string query, CancellationToken cancellationToken = default(CancellationToken));
	    Task<IEnumerable<LabelValueObject>> GetCategorySourceAsync(string query, CancellationToken cancellationToken = default(CancellationToken));
    }
}
