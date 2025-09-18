using ServiceContract.DTO.Countries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceContract
{
    public interface ICountryService
    {
        Task<CountryRes> AddCountryAsync(CountryAddReq countryAddReq);
        Task<List<CountryRes>> GetAllCountriesAsync();
        Task<CountryRes?> GetCountryByIdAsync(Guid? guid);
        Task<bool> DeleteCountryAsync(Guid id);
       
    }
}
