using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernShopping.Application.Contracts;
using ModernShopping.Application.Dtos;

namespace ModernShopping.Presentation.Controllers.Api
{
    [Route("api/data-source")]
    public class DataSourceController : ApiBaseController
    {
        private readonly IProductService _productService;

        public DataSourceController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("supplier-source/{query}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LabelValueObject>>> GetSupplierSource(string query)
        {
            var supplierSource = await _productService.GetSupplierSource(query);

            return Ok(supplierSource);
        }

        [HttpGet("category-source/{query}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LabelValueObject>>> GetCategorySource(string query)
        {
            var supplierSource = await _productService.GetCategorySource(query);

            return Ok(supplierSource);
        }
    }
}
