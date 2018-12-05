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
        Task<DeleteResult> DeleteProduct(int id);
	    Task<(ProductDto product, bool isAdded)> AddProduct(ProductForCreationDto newProduct);

    }
}
