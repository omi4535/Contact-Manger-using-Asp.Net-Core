using Microsoft.AspNetCore.Mvc;
using ServiceContract;
using ServiceContract.DTO.Countries;
using services;

namespace ContactManger.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _CountryService;
        
        public CountryController(ICountryService con)
        {
            _CountryService = con;
        }

        [Route("AllCountry")]
        public IActionResult Index()
        {
            return View(_CountryService.GetAllCountries());
        }

        [Route("Country/Delete/{guid:guid}")]
        public IActionResult DeleteCountry(Guid guid)
        {
            _CountryService.DeleteCountry(guid);

            return RedirectToAction("Index");
        }
        [Route("Country/Add")]
        public IActionResult AddCountry(CountryAddReq countryAddReq)
        {
            try
            {
               _CountryService.AddCountry(countryAddReq);
              
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
