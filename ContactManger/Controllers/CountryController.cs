using Microsoft.AspNetCore.Mvc;
using ServiceContract;
using ServiceContract.DTO.Countries;
using System;
using System.Threading.Tasks;

namespace ContactManger.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService con)
        {
            _countryService = con;
        }

        [Route("AllCountry")]
        public async Task<IActionResult> Index()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return View(countries); // ✅ Now passing List<CountryRes>
        }

        [Route("Country/Delete/{guid:guid}")]
        public async Task<IActionResult> DeleteCountry(Guid guid)
        {
            await _countryService.DeleteCountryAsync(guid);
            return RedirectToAction("Index");
        }

        [HttpPost] // ✅ Good practice for add
        [Route("Country/Add")]
        public async Task<IActionResult> AddCountry(CountryAddReq countryAddReq)
        {
            if (!ModelState.IsValid)
            {
                return View(countryAddReq); // return form with validation errors
            }

            try
            {
                await _countryService.AddCountryAsync(countryAddReq);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(countryAddReq);
            }

            return RedirectToAction("Index");
        }
    }
}
