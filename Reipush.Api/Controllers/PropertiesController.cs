using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Reipush.Api.Entities;
using Reipush.Api.Entities.Property;
using Reipush.Api.Services;
using Reipush.Api.ViewModels.Property;

namespace Reipush.Api.Controllers
{
    [Route("api/property")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
  
            private readonly IConfiguration _config;
            private readonly ReipushContext _context;

            public PropertiesController(ReipushContext context, IConfiguration config)
            {
                _context = context;
                _config = config;
            }

            [HttpPost("quicksearch")]
            [ApiVersion("1")]
            [Produces("application/json")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]

            public ActionResult<IEnumerable<voPropertyAddress>> QuickSearch(viSearchAddress iAddress)
            {
                Services.PropertyService _PropertyService = new PropertyService(_context);

                string iStreet = iAddress.StreetAddress.Trim();

                if (iStreet.Length < 4)
                {

                    return BadRequest("The Address must contain 4 or more charcters");
                }

                List<voPropertyAddress> raddresses = new List<voPropertyAddress>();
                try
                {
                    raddresses = _PropertyService.QuickSearchAddress(iAddress);


                    if (raddresses == null)
                    {
                        return NotFound("No Address Found");
                    }

                }
                catch (Exception e)
                {

                    return BadRequest(e.Message.ToString());
                }
                return Ok(raddresses);
            }



        }


  
}