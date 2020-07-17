using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Reipush.Api.Entities;
using Reipush.Api.Entities.Property;
using Reipush.Api.ViewModels.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reipush.Api.Services
{
    public class PropertyService
    {
        private readonly ReipushContext _reipushcontext;

        public PropertyService(ReipushContext context)
        {
            _reipushcontext = context;
        }

        public List<voPropertyAddress> QuickSearchAddress(viSearchAddress iaddress )
        {
            List<voPropertyAddress> rAddresses = new List<voPropertyAddress>();

            try
            {
              var  vAddresses = _reipushcontext.voPropertyAddress.FromSqlRaw("SearchStreetAddress @StreetAddress, @City, @State, @Zip",
                            new SqlParameter("StreetAddress", iaddress.StreetAddress),
                            new SqlParameter("City", iaddress.City),
                            new SqlParameter("State", iaddress.State),
                            new SqlParameter("Zip", iaddress.Zip))
                           .AsEnumerable();

                rAddresses = vAddresses.ToList();
            }
            catch (Exception e)
            {

            }
            return rAddresses;
        }


    }
}
