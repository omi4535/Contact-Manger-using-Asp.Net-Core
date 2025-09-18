using ServiceContract;
using Entity;
using ServiceContract.DTO.Countries;
using Microsoft.EntityFrameworkCore;

namespace services
{
    public class CountryService : ICountryService
    {
        private readonly ContactMangerDBContext _Db;

        public CountryService(ContactMangerDBContext dbContext, bool init = true)
        {
            _Db = dbContext;
        }

        public async Task<CountryRes> AddCountryAsync(CountryAddReq countryAddReq)
        {
            if (countryAddReq == null)
                throw new ArgumentNullException(nameof(countryAddReq));

            if (string.IsNullOrWhiteSpace(countryAddReq.CountryName))
                throw new ArgumentException(nameof(countryAddReq.CountryName));

            bool exists = await _Db.countries
                .AnyAsync(temp => temp.CountryName == countryAddReq.CountryName);

            if (exists)
                throw new ArgumentException("Country already exists");

            Entity.Country con = countryAddReq.ToCountry();
            con.id = Guid.NewGuid();

            await _Db.countries.AddAsync(con);
            await _Db.SaveChangesAsync();

            return con.ToCountryRes();
        }

        public async Task<List<CountryRes>> GetAllCountriesAsync()
        {
            var countries = await _Db.countries
                .Include(c => c.PeopleFromCountry)
                .ToListAsync();

            return countries.Select(con => con.ToCountryRes()).ToList();
        }

        public async Task<CountryRes?> GetCountryByIdAsync(Guid? guid)
        {
            if (guid == null)
                throw new ArgumentNullException(nameof(guid));

            var country = await _Db.countries.FirstOrDefaultAsync(con => con.id == guid);
            return country?.ToCountryRes();
        }

        public async Task<bool> DeleteCountryAsync(Guid id)
        {
            var country = await _Db.countries.FirstOrDefaultAsync(c => c.id == id);

            if (country == null)
                return false;

            _Db.countries.Remove(country);
            await _Db.SaveChangesAsync();
            return true;
        }

        // Not in interface, but keeping async for consistency
        public async Task<CountryRes?> EditCountryAsync(Guid id, CountryAddReq countryUpdateReq)
        {
            if (countryUpdateReq == null)
                throw new ArgumentNullException(nameof(countryUpdateReq));

            if (string.IsNullOrWhiteSpace(countryUpdateReq.CountryName))
                throw new ArgumentException(nameof(countryUpdateReq.CountryName));

            var country = await _Db.countries.FirstOrDefaultAsync(c => c.id == id);

            if (country == null)
                return null;

            bool exists = await _Db.countries
                .AnyAsync(c => c.id != id && c.CountryName == countryUpdateReq.CountryName);

            if (exists)
                throw new ArgumentException("Another country with the same name already exists");

            country.CountryName = countryUpdateReq.CountryName;
            country.CountryCode = countryUpdateReq.CountryCode;

            await _Db.SaveChangesAsync();
            return country.ToCountryRes();
        }
    }
}
