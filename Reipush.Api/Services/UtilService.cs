using Reipush.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Internal;
using Reipush.Api.ViewModels;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text;
using Reipush.Api.Entities.Util;
using Reipush.Api.ViewModels.Util;

namespace Reipush.Api.Services
{

    public class UtilService
    {
        private readonly ReipushContext _reipushcontext;

        public UtilService(ReipushContext context)
        {
            _reipushcontext = context;
        }


        public viUsCountriesStates GetUCountryUSStates(int icountryid)
        {
            viUsCountriesStates vUSCS = new viUsCountriesStates();

            var result = _reipushcontext.Countries.ToList()
                .Select(p => new viCountries()
                {
                    CountryId = p.CountryId,
                    Name = p.Name,
                    NumericISOCode = p.NumericISOCode,
                    ThreeLetterISOCode = p.ThreeLetterISOCode,
                    TwoLetterISOCode = p.TwoLetterISOCode
                });

            vUSCS.Countries = result.ToArray();
            vUSCS.UsStateProvinces = GetStateProvincesByCntryId(1);

            return vUSCS;
        }

        public List<viStateProvince> GetStateProvincesByCntryId(int CountryId )
        {
            List<viStateProvince> result = new List<viStateProvince>();

            try {
                result = _reipushcontext.StateProvinces
                       .Where(s => s.CountryId == CountryId)
                       .Select(s => new viStateProvince()
                       {
                           Abbreviation = s.Abbreviation,
                           CountryId = s.CountryId,
                           Name = s.Name,
                           StateProvinceId = s.StateProvinceId
                       }).ToList();

            }
            catch (Exception e)
            {
               
            }
            return result;
        }

      
    }

 


}
