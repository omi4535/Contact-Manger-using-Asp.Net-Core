using Microsoft.AspNetCore.Mvc;
using ServiceContract;
using ServiceContract.DTO.Person;
using System.Reflection;

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
        public IActionResult Index()
        {
            ViewBag.CountryDD = _country.GetAllCountries().Select(temp =>
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(
            temp.CountryName,temp.id.ToString()
            ));
            return View(_person.GetAllPerson());
        }

        [Route("Person/Add")]
        public IActionResult Add(PersonAddReq addReq)
        {
            if (ModelState.IsValid)
            {
                _person.AddPerson(addReq);
            }
            else
            {
                return PartialView("AddPersonPartial", addReq);
            }

                return RedirectToAction("Index");
        }
        [Route("Person/Delete/{guid:guid}")]
        public IActionResult Delete(Guid guid)
        {
            _person.DeletePerson(guid);
            return RedirectToAction("Index");
        }

        [Route("Person/GetPersonById/{id:guid}")]
        public IActionResult GetPersonByid(Guid id)
        {
            return Json(_person.GetPersonById(id));
        }
    }
}
