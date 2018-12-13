using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModernShopping.Presentation.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public abstract class ApiBaseController : ControllerBase
    {
    }
}
