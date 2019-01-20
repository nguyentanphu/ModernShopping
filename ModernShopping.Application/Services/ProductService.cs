using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using ModernShopping.Application.Common;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Dtos;
using ModernShopping.Application.Dtos.Products;
using ModernShopping.Application.Exceptions;
using ModernShopping.Application.Utils;
using ModernShopping.Application.Utils.Mappers;
using ModernShopping.Application.Utils.Queryable;
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

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(IEnumerable<int> ids = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.Products
                .ProductIncludes()
                .ApplyWhere(ids != null && ids.Any(), p => ids.Contains(p.ProductId))
                .Select(ProductMapper.EntityToDtoExpression)
	            .OrderByDescending(p => p.ProductId)
                .ToListAsync(cancellationToken);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await GetProductsAsync(new List<int> {id}, cancellationToken)).FirstOrDefault();
        }

        public async Task DeleteProductAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var productEntity = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id, cancellationToken);

            if (productEntity == null)
                throw  new EntityNotFoundException($"Cannot find product with id={id} in order to delete");

            _context.Products.Remove(productEntity);

            var affectedRow = await _context.SaveChangesAsync(cancellationToken);
            HelperCore.CheckSaveChange(affectedRow, 1);

        }

	    public async Task<ProductDto> AddProductAsync(ProductForCreationDto newProduct, CancellationToken cancellationToken = default(CancellationToken))
	    {
	        return (await AddProductsAsync(new List<ProductForCreationDto> {newProduct}, cancellationToken)).FirstOrDefault();
	    }

        public async Task<IEnumerable<ProductDto>> AddProductsAsync(
            IEnumerable<ProductForCreationDto> newProducts, CancellationToken cancellationToken = default(CancellationToken))
        {
            var productEntities = newProducts.Select(ProductMapper.CreationToEntityFunc).ToList();
            _context.Products.AddRange(productEntities);
            var affectedRow = await _context.SaveChangesAsync(cancellationToken);
            var expectedAffectedRow = productEntities.Count + productEntities.Sum(p => p.ProductImages.Count);

            HelperCore.CheckSaveChange(affectedRow, expectedAffectedRow);

            var returnProducts = productEntities.Select(ProductMapper.EntityToDtoFunc);

            return returnProducts;
        }

	    public async Task<IEnumerable<LabelValueObject>> GetSupplierSourceAsync(string query, CancellationToken cancellationToken = default(CancellationToken))
	    {
		    return await  _context.Suppliers
			    .Where(s => s.CompanyName.Contains(query))
			    .Select(s => new LabelValueObject
			    {
				    Label = s.CompanyName,
				    Value = s.SupplierId
			    })
			    .Take(ApplicationConst.MaximumTake)
			    .ToListAsync(cancellationToken);
	    }

	    public async Task<IEnumerable<LabelValueObject>> GetCategorySourceAsync(string query, CancellationToken cancellationToken = default(CancellationToken))
	    {
		    return await _context.Categories
			    .Where(c => c.CategoryName.Contains(query))
			    .Select(c => new LabelValueObject
			    {
				    Label = c.CategoryName,
				    Value = c.CategoryId
			    })
			    .Take(ApplicationConst.MaximumTake)
			    .ToListAsync(cancellationToken);
	    }
	}
}
