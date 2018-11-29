using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModernShopping.Application.Dtos;

namespace ModernShopping.Application.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int id);
    }
}
