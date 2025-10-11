using ContactManger.Filters;
using ContactManger.Filters.ActionFilters;
using ContactManger.Filters.AuthFilter;
using ContactManger.Filters.exceptionFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using ServiceContract;
using ServiceContract.DTO.Person;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManger.Controllers
{
    [TypeFilter(typeof(PersonExceptionFilter))]
    public class PersonController : Controller
    {
        private readonly IPerson _person;
        private readonly ICountryService _country;

        public PersonController(IPerson person, ICountryService con)
        {
            _person = person;
            _country = con;
        }

        [Route("person")]
        [TypeFilter(typeof(personActionFilter), Arguments =  new Object[]{"TryArg"}, Order = -1)]
        [SkipFilter]
        public async Task<IActionResult> Index()
        {
            // Load dropdown countries
            ViewBag.CountryDD = (await _country.GetAllCountriesAsync())
                .Select(temp => new SelectListItem(temp.CountryName, temp.id.ToString()))
                .ToList();

            // Call stored procedure (if needed)
            await _person.GetAllPersonWithSpAsync();

            // Pass actual list of persons to the view
            var people = await _person.GetAllPersonAsync();
            ViewData["XYZ"] = "XYZ";
            HttpContext.Response.Cookies.Append("Auth-key", "Auth001", new CookieOptions
            {
                HttpOnly = true,   // Not accessible by JS
                SameSite = SameSiteMode.Strict, // CSRF protection
                Expires = DateTimeOffset.UtcNow.AddMinutes(30) // Expiry time
            });
            return View(people);
        }

        [HttpPost]
        [Route("Person/Add")]
        [TypeFilter(typeof(personActionFilter),Arguments= new object[] {"str"})]
        [TypeFilter(typeof(AddPersonAuthFilter))]
        public async Task<IActionResult> Add(PersonAddReq addReq)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("AddPersonPartial", addReq);
            }

            await _person.AddPersonAsync(addReq);
            return RedirectToAction("Index");
        }

        [Route("Person/Delete/{guid:guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await _person.DeletePersonAsync(guid);
            return RedirectToAction("Index");
        }

        [Route("Person/GetPersonById/{id:guid}")]
        public async Task<IActionResult> GetPersonById(Guid id)
        {
            var person = await _person.GetPersonByIdAsync(id);
            if (person == null)
                return NotFound();

            return Json(person);
        }
        [Route("Person/PDF")]
        public async Task<IActionResult> PersonToPdf()
        {
            var people = await _person.GetAllPersonAsync();

            return new ViewAsPdf("PersonToPdf", people.ToList())
            {
                FileName = "Persons.pdf"
            };
        }

        [Route("Person/CSV")]
        public async Task<IActionResult> PersonToCsv()
        {
            var memoryStream = await _person.GetPersonCSV();
            return File(memoryStream, "text/csv", "People.csv");
        }
    }
}
