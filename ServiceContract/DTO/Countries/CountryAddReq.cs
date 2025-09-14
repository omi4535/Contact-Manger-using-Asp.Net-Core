using Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.DTO.Countries
{
    public class CountryAddReq
    {
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }
        public Country ToCountry()
        {
            return new Country() { CountryName = CountryName, CountryCode = CountryCode };
        }
    }
}
