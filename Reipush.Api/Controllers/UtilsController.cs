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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public viUsCountriesStates GetCountriesUSStatesProv()
        {

            Services.UtilService  _UtilsService = new UtilService(_context);
            return  _UtilsService.GetUCountryUSStates(1);

        }

        // POST: api/Util/countriesusstates
        [HttpPost("statesprovbycountryid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<viStateProvince>> GetStatesProvByCntryId(viCountryID CntryId) 
        {

            Services.UtilService _UtilsService = new UtilService(_context);
            return _UtilsService.GetStateProvincesByCntryId(CntryId.CountryId);

        }


    }
}
