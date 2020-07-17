using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reipush.Api.ViewModels.Util;
using Reipush.Api.Services;
using Reipush.Api.Entities;

namespace Reipush.Api.Controllers
{
    [Route("api/Utils")]
    [ApiController]
    public class UtilsController : ControllerBase
    {
        private readonly ReipushContext _context;

        public UtilsController(ReipushContext context)
        {
            _context = context;
        }

        // GET: api/Util/countriesusstates
        [HttpGet("countriesusstates")]
        [ApiVersion("1")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<viUsCountriesStates> GetCountriesUSStatesProv()
        {
            viUsCountriesStates result;
            try
            {
                Services.UtilService _UtilsService = new UtilService(_context);
                result = _UtilsService.GetUCountryUSStates(1);

            }catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }

            return Ok(result);
        }

        // POST: api/Util/countriesusstates
        [HttpPost("statesprovbycountryid")]
        [ApiVersion("1")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<viStateProvince>> GetStatesProvByCntryId(viCountryID CntryId) 
        {
            List<viStateProvince> result;
            try
            {
                Services.UtilService _UtilsService = new UtilService(_context);
                result =  _UtilsService.GetStateProvincesByCntryId(CntryId.CountryId);

                if (result == null)
                {
                    return NotFound("No Data Found");
                }

            }catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }

            return Ok(result);

        }


    }
}
