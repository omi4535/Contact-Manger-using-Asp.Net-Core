using EntityFrameworkMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

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
            var options = new DbContextOptionsBuilder<ContactMangerDBContext>()
      .UseInMemoryDatabase(databaseName: "TestDb")
      .Options;
            var dbContext = new ContactMangerDBContext(options);
            _country = new CountryService(dbContext);
        }
        #region AddCountry


        // null parameter
        [Fact]
        public async Task TestNullPara()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
               await _country.AddCountryAsync(null);
            });

        }
        // null name
        [Fact]
        public void TestNullName()
        {
            CountryAddReq countryReq = new CountryAddReq() { CountryName = null };
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
               await _country.AddCountryAsync(countryReq);
            });

        }
        // duplicate name
        [Fact]
        public async Task TestDuplicate()
        {
            CountryAddReq countryReq = new CountryAddReq() { CountryName = "USA" };
            CountryAddReq countryReq1 = new CountryAddReq() { CountryName = "USA" };
            await Assert.ThrowsAsync<ArgumentException>(async () => 
            {
                await _country.AddCountryAsync(countryReq);
                await _country.AddCountryAsync(countryReq1);
            });

        }
        // proper object
        [Fact]
        public async Task TestAddition()
        {
            //   CountryAddReq countryReq = new CountryAddReq() { CountryName = "USA" };
            CountryAddReq countryReq1 = new CountryAddReq() { CountryName = "India" };

            // _country.AddCountry(countryReq);
            CountryRes res = await _country.AddCountryAsync(countryReq1);
            List<CountryRes> listres =await _country.GetAllCountriesAsync();

            Assert.True(res.id != Guid.Empty);
            Assert.Contains(res, listres);

        }
        #endregion

        #region GetAllCountries
        [Fact]
        public async Task TestGetAllCountries_Empty()
        {
            Assert.Empty(await _country.GetAllCountriesAsync());
        }

        [Fact]
        public async Task TestGetAllCountries_AddFew()
        {
            List<CountryAddReq> countryAddReqs = new List<CountryAddReq>()
            {
                new CountryAddReq(){CountryName= "India",CountryCode = "+91"},
                new CountryAddReq(){CountryName= "Japan",CountryCode = "+1"},

            };
            List<CountryRes> Aded_res = new List<CountryRes>();
            foreach(CountryAddReq req in countryAddReqs)
            {
                 Aded_res.Add(await _country.AddCountryAsync(req));
            }
            List<CountryRes> ActualList = await _country.GetAllCountriesAsync();
            foreach(CountryRes response in Aded_res)
            {
                Assert.Contains(response, ActualList);
            }
        }
        #endregion

        #region GetCountryById
        // empty parameter 
        [Fact]
        public async Task TestGetCountry_CheckNullPara()
        {
          
         
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                CountryRes? res = await _country.GetCountryByIdAsync(null);
            });
        }
        // found
        [Fact]
        public async Task TestGetCountry_GetValidContry()
        {
            CountryAddReq req = new CountryAddReq() { CountryName = "India" };
            CountryRes res = await _country.AddCountryAsync(req);

            CountryRes Actual =await _country.GetCountryByIdAsync(res.id);

            Assert.Equal(res, Actual);
        }

        #endregion
    }
}
