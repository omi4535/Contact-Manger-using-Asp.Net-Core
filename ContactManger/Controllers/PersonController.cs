using Microsoft.AspNetCore.Mvc;
using ServiceContract;
using ServiceContract.DTO.Person;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContactManger.Controllers
{
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
            return View(people);
        }

        [HttpPost]
        [Route("Person/Add")]
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
    }
}
