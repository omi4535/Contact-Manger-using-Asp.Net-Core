using ServiceContract.DTO.Countries;

namespace ServiceContract
{
    public interface ICountryService
    {
        CountryRes AddCountry(CountryAddReq countryAddReq);
        List<CountryRes> GetAllCountries();

        CountryRes GetCountryById(Guid? guid);
        bool DeleteCountry(Guid id);
    }
}
