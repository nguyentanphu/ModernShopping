using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModernShopping.Application.Common;
using ModernShopping.Application.Dtos;
using ModernShopping.Application.Dtos.Products;

namespace ModernShopping.Application.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int id);
        Task<IEnumerable<ProductDto>> GetProductByIds(IEnumerable<int> ids);
        Task<DeleteResult> DeleteProduct(int id);
	    Task<(ProductDto product, bool isAdded)> AddProduct(ProductForCreationDto newProduct);
        Task<(IEnumerable<ProductDto> product, bool isAdded)> AddProducts(IEnumerable<ProductForCreationDto> newProducts);

    }
}
