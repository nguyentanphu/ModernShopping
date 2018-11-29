using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Dtos;
using ModernShopping.Application.Utils.Mappers;
using ModernShopping.Persistence;
using ModernShopping.Persistence.Entities;

namespace ModernShopping.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly NorthwindContext _context;

        public ProductService(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            return await _context.Products
                .Select(ProductMapper.EntityToDtoExpressionFunc)
                .ToListAsync();
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var productEntity = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            return productEntity.Map(ProductMapper.EntityToDtoFunc);
        }
    }
}
