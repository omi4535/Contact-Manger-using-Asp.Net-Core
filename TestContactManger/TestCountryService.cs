
using ServiceContract;
using ServiceContract.DTO.Countries;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestContactManger
{
    public class TestCountryService
    {
        private readonly ICountryService _country;
        public TestCountryService()
        {
            _country = new CountryService(false);
        }
        #region AddCountry


        // null parameter
        [Fact]
        public void TestNullPara()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _country.AddCountry(null);
            });

        }
        // null name
        [Fact]
        public void TestNullName()
        {
            CountryAddReq countryReq = new CountryAddReq() { CountryName = null };
            Assert.Throws<ArgumentException>(() =>
            {
                _country.AddCountry(countryReq);
            });

        }
        // duplicate name
        [Fact]
        public void TestDuplicate()
        {
            CountryAddReq countryReq = new CountryAddReq() { CountryName = "USA" };
            CountryAddReq countryReq1 = new CountryAddReq() { CountryName = "USA" };
            Assert.Throws<ArgumentException>(() =>
            {
                _country.AddCountry(countryReq);
                _country.AddCountry(countryReq1);
            });

        }
        // proper object
        [Fact]
        public void TestAddition()
        {
            //   CountryAddReq countryReq = new CountryAddReq() { CountryName = "USA" };
            CountryAddReq countryReq1 = new CountryAddReq() { CountryName = "India" };

            // _country.AddCountry(countryReq);
            CountryRes res = _country.AddCountry(countryReq1);
            List<CountryRes> listres = _country.GetAllCountries();

            Assert.True(res.id != Guid.Empty);
            Assert.Contains(res, listres);

        }
        #endregion

        #region GetAllCountries
        [Fact]
        public void TestGetAllCountries_Empty()
        {
            Assert.Empty(_country.GetAllCountries());
        }

        [Fact]
        public void TestGetAllCountries_AddFew()
        {
            List<CountryAddReq> countryAddReqs = new List<CountryAddReq>()
            {
                new CountryAddReq(){CountryName= "India",CountryCode = "+91"},
                new CountryAddReq(){CountryName= "Japan",CountryCode = "+1"},

            };
            List<CountryRes> Aded_res = new List<CountryRes>();
            foreach(CountryAddReq req in countryAddReqs)
            {
                Aded_res.Add(_country.AddCountry(req));
            }
            List<CountryRes> ActualList = _country.GetAllCountries();
            foreach(CountryRes response in Aded_res)
            {
                Assert.Contains(response, ActualList);
            }
        }
        #endregion

        #region GetCountryById
        // empty parameter 
        [Fact]
        public void TestGetCountry_CheckNullPara()
        {
           CountryRes? res =  _country.GetCountryById(null);
           Assert.Null(res); 
        }
        // found
        [Fact]
        public void TestGetCountry_GetValidContry()
        {
            CountryAddReq req = new CountryAddReq() { CountryName = "India" };
            CountryRes res = _country.AddCountry(req);

            CountryRes Actual = _country.GetCountryById(res.id);

            Assert.Equal(res, Actual);
        }

        #endregion
    }
}
