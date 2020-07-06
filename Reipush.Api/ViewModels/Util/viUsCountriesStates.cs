using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reipush.Api.ViewModels.Util
{
 
    public class viUsCountriesStates
    {
        public ICollection<viCountries> Countries { get; set; }
        public ICollection<viStateProvince> UsStateProvinces { get; set; }

        
    }
}
