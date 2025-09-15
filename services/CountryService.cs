using ServiceContract;
using Entity;
using ServiceContract.DTO.Countries;

namespace services
{
    public class CountryService : ICountryService
    {
        private readonly ContactMangerDBContext _Db;


        public CountryService(ContactMangerDBContext dbContext, bool init=true)
        {
            _Db = dbContext;
           
        }
        public Guid id { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }

        public CountryRes AddCountry(CountryAddReq countryAddReq)
        {
             if(countryAddReq == null)
            {
                throw new ArgumentNullException(nameof(countryAddReq));
            }

            if (countryAddReq.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddReq.CountryName));
            }

            if(_Db.countries.Where(temp => temp.CountryName == countryAddReq.CountryName).Count()>0)
            {

                throw new ArgumentException("Country already exists");
            }

            Entity.Country con = countryAddReq.ToCountry();
            con.id = Guid.NewGuid();
            _Db.countries.Add(con);
            _Db.SaveChanges();
            return con.ToCountryRes();


        }

        public List<CountryRes> GetAllCountries()
        {
            List<CountryRes> resList = new List<CountryRes>();
            foreach(Entity.Country con in _Db.countries)
            {
                resList.Add(con.ToCountryRes());
            }
            return resList;
        }

        public CountryRes? GetCountryById(Guid? guid)
        {
            return _Db.countries.FirstOrDefault(con => con.id == guid)?.ToCountryRes() ;
        }
        public bool DeleteCountry(Guid id)
        {
            // Find the country in the list
            var country = _Db.countries.FirstOrDefault(c => c.id == id);

            if (country == null)
            {
                // Country not found
                return false;
            }

            // Remove from the list
            _Db.countries.Remove(country);
            _Db.SaveChanges();
            return true;
        }

        public CountryRes? EditCountry(Guid id, CountryAddReq countryUpdateReq)
        {
            if (countryUpdateReq == null)
                throw new ArgumentNullException(nameof(countryUpdateReq));

            if (string.IsNullOrWhiteSpace(countryUpdateReq.CountryName))
                throw new ArgumentException(nameof(countryUpdateReq.CountryName));

            // Find the existing country
            var country = _Db.countries.FirstOrDefault(c => c.id == id);
            if (country == null)
                return null; // country not found

            // Check if the new name already exists in another country
            if (_Db.countries.Any(c => c.id != id && c.CountryName == countryUpdateReq.CountryName))
                throw new ArgumentException("Another country with the same name already exists");

            // Update country properties
            country.CountryName = countryUpdateReq.CountryName;
            country.CountryCode = countryUpdateReq.CountryCode;

            return country.ToCountryRes();
        }

    }
}
