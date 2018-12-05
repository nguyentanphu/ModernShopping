using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
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
                Discontinued = p.Discontinued,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock
            };

        public static Func<Product, ProductDto> EntityToDtoFunc => EntityToDtoExpression.Compile();

	    public static Expression<Func<ProductForCreationDto, Product>> CreationToEntityExpression =>
		    p => new Product
		    {
			    CategoryId = p.CategoryId,
				SupplierId = p.SupplierId,
			    ProductName = p.ProductName,
			    QuantityPerUnit = p.QuantityPerUnit,
			    UnitPrice = p.UnitPrice,
			    UnitsInStock = p.UnitsInStock,
				Discontinued = false,
		    };

	    public static Func<ProductForCreationDto, Product> CreationToEntityFunc = CreationToEntityExpression.Compile();
    }
}
