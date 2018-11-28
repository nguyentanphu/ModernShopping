using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernShopping.Persistence;
using ModernShopping.Persistence.Entities;

namespace ModernShopping.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
		private readonly NorthwindContext _context;

		public ProductsController(NorthwindContext context)
	    {
		    _context = context;
	    }

	    [HttpGet("{id}")]
	    public async Task<ActionResult<Product>> GetProduct(int id)
	    {
		    return await _context.Products.FindAsync(id);
	    }

	    [HttpGet]
	    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
	    {
		    return await _context.Products.ToListAsync();
	    }
	}
}