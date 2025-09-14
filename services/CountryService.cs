using ServiceContract;
using Entity;
using ServiceContract.DTO.Countries;

namespace services
{
    public class CountryService : ICountryService
    {
        private readonly List<Entity.Country> _Countries;


        public CountryService(bool init=true)
        {
            if (init)
            {
                _Countries = new List<Country>{
            new Entity.Country { id = Guid.NewGuid(), CountryName = "India" ,CountryCode ="+91"},
            new Entity.Country { id = Guid.NewGuid(), CountryName = "USA",CountryCode="+11" },
            new Entity.Country { id = Guid.NewGuid(), CountryName = "Germany",CountryCode = "+99" }
        };
            }
            else
            {
                _Countries = new List<Country>();
            }
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

            if(_Countries.Where(temp => temp.CountryName == countryAddReq.CountryName).Count()>0)
            {

                throw new ArgumentException("Country already exists");
            }

            Entity.Country con = countryAddReq.ToCountry();
            con.id = Guid.NewGuid();
            _Countries.Add(con);
            return con.ToCountryRes();


        }

        public List<CountryRes> GetAllCountries()
        {
            List<CountryRes> resList = new List<CountryRes>();
            foreach(Entity.Country con in _Countries)
            {
                resList.Add(con.ToCountryRes());
            }
            return resList;
        }

        public CountryRes? GetCountryById(Guid? guid)
        {
            return _Countries.FirstOrDefault(con => con.id == guid)?.ToCountryRes() ;
        }
        public bool DeleteCountry(Guid id)
        {
            // Find the country in the list
            var country = _Countries.FirstOrDefault(c => c.id == id);

            if (country == null)
            {
                // Country not found
                return false;
            }

            // Remove from the list
            _Countries.Remove(country);
            return true;
        }

        public CountryRes? EditCountry(Guid id, CountryAddReq countryUpdateReq)
        {
            if (countryUpdateReq == null)
                throw new ArgumentNullException(nameof(countryUpdateReq));

            if (string.IsNullOrWhiteSpace(countryUpdateReq.CountryName))
                throw new ArgumentException(nameof(countryUpdateReq.CountryName));

            // Find the existing country
            var country = _Countries.FirstOrDefault(c => c.id == id);
            if (country == null)
                return null; // country not found

            // Check if the new name already exists in another country
            if (_Countries.Any(c => c.id != id && c.CountryName == countryUpdateReq.CountryName))
                throw new ArgumentException("Another country with the same name already exists");

            // Update country properties
            country.CountryName = countryUpdateReq.CountryName;
            country.CountryCode = countryUpdateReq.CountryCode;

            return country.ToCountryRes();
        }

    }
}
