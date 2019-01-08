using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ModernShopping.Application.Dtos;
using ModernShopping.Application.Dtos.Products;
using ModernShopping.Persistence.Entities;

namespace ModernShopping.Application.Utils.Mappers
{
    public static class ProductMapper
    {
        public static Expression<Func<Product, ProductDto>> EntityToDtoExpression =>
            p => new ProductDto
            {
                ProductId = p.ProductId,
                Category = p.Category == null ? string.Empty : p.Category.CategoryName,
                Supplier = p.Supplier == null ? string.Empty : p.Supplier.CompanyName,
                ProductName = p.ProductName,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock
            };

        public static Func<Product, ProductDto> EntityToDtoFunc => EntityToDtoExpression.Compile();

	    public static Expression<Func<ProductForCreationDto, Product>> CreationToEntityExpression =>
		    p => new Product
		    {
			    CategoryId = p.Category == null ? (int?)null : p.Category.Value,
				SupplierId = p.Supplier == null ? (int?)null : p.Supplier.Value,
                ProductName = p.ProductName,
			    QuantityPerUnit = p.QuantityPerUnit,
			    UnitPrice = p.UnitPrice,
			    UnitsInStock = p.UnitsInStock,
                UnitsOnOrder = p.UnitsOnOrder,
                ProductImages = new HashSet<ProductImage> { new ProductImage { ImageId = p.ImageId} }
		    };

	    public static Func<ProductForCreationDto, Product> CreationToEntityFunc = CreationToEntityExpression.Compile();

        public static IQueryable<Product> ProductIncludes(this IQueryable<Product> query)
        {
            return query
                .Include(p => p.Category)
                .Include(p => p.Supplier);
        }
    }
}
