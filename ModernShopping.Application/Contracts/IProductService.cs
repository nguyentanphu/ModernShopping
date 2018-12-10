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
        Task<IEnumerable<ProductDto>> GetProducts(IEnumerable<int> ids = null);
        Task<ProductDto> GetProductById(int id);
        Task<(bool IsFound, bool IsDeleted)> DeleteProduct(int id);
	    Task<(ProductDto Product, bool IsAdded)> AddProduct(ProductForCreationDto newProduct);
        Task<(IEnumerable<ProductDto> Products, bool IsAdded)> AddProducts(IEnumerable<ProductForCreationDto> newProducts);

    }
}
