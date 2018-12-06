using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using ModernShopping.Application.Common;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Dtos;
using ModernShopping.Application.Dtos.Products;
using ModernShopping.Application.Utils.Mappers;
using ModernShopping.Persistence;
using ModernShopping.Persistence.Entities;

namespace ModernShopping.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly NorthwindContext _context;
        private readonly IQueryable<Product> _defaultProductQuery; 
        public ProductService(NorthwindContext context)
        {
            _context = context;
            _defaultProductQuery = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => !p.Discontinued);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            return await _defaultProductQuery
                .Select(ProductMapper.EntityToDtoExpression)
                .ToListAsync();
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            return (await GetProductByIds(new List<int> {id})).FirstOrDefault();
        }

        public async Task<IEnumerable<ProductDto>> GetProductByIds(IEnumerable<int> ids)
        {
            if (!ids.Any()) return Enumerable.Empty<ProductDto>();

            return await _defaultProductQuery
                .Where(p => ids.Contains(p.ProductId))
                .Select(ProductMapper.EntityToDtoExpression)
                .ToListAsync();
        }

        public async Task<DeleteResult> DeleteProduct(int id)
        {
            var productEntity = await _defaultProductQuery
                .FirstOrDefaultAsync(p => p.ProductId == id);

            var isFound = productEntity != null;

            if (isFound)
                productEntity.Discontinued = true;

            return new DeleteResult
            {
                IsFound = isFound,
                IsDeleted = (await _context.SaveChangesAsync()) > 0
            };
        }

	    public async Task<(ProductDto product, bool isAdded)> AddProduct(ProductForCreationDto newProduct)
	    {
		    var productEntity = newProduct.Map(ProductMapper.CreationToEntityFunc);
		    _context.Products.Add(productEntity);
		    var isAdded = await _context.SaveChangesAsync() > 0;
		    var returnProduct = productEntity.Map(ProductMapper.EntityToDtoFunc);

		    return (returnProduct, isAdded);
	    }

        public async Task<(IEnumerable<ProductDto> product, bool isAdded)> AddProducts(
            IEnumerable<ProductForCreationDto> newProducts)
        {
            var productEntities = newProducts.Select(ProductMapper.CreationToEntityFunc).ToList();
            _context.Products.AddRange(productEntities);
            var isAdded = await _context.SaveChangesAsync() == productEntities.Count;
            var returnProducts = productEntities.Select(ProductMapper.EntityToDtoFunc);

            return (returnProducts, isAdded);
        }
    }
}
