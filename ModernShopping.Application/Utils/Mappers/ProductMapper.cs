using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ModernShopping.Application.Dtos;
using ModernShopping.Persistence.Entities;

namespace ModernShopping.Application.Utils.Mappers
{
    public static class ProductMapper
    {
        public static Expression<Func<Product, ProductDto>> EntityToDtoExpressionFunc =>
            p => new ProductDto
            {
                ProductId = p.ProductId,
                Category = p.Category.CategoryName,
                Supplier = p.Supplier.CompanyName,
                ProductName = p.ProductName,
                Discontinued = p.Discontinued,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock
            };

        public static Func<Product, ProductDto> EntityToDtoFunc => EntityToDtoExpressionFunc.Compile();
    }
}
