using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.DTO.Countries
{
    public class CountryRes
    {
        public Guid id { get; set; }
        public string? CountryName { get; set; }
        public string?CountryCode { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() == typeof(CountryRes)){
                CountryRes? objRes = (CountryRes?)obj;
                return objRes.CountryName == this.CountryName && objRes.id == this.id;
            }
            return false;
        }
    }
    public static class CountryExtenxion
    {
        public static CountryRes ToCountryRes(this Entity.Country con)
        {
            return new CountryRes() { CountryName = con.CountryName, id = con.id,CountryCode=con.CountryCode};
        }
    }

}
