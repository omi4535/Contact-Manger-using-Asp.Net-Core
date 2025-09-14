using Entity;
using ServiceContract;
using ServiceContract.DTO.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services
{
    public class PersonService : IPerson
    {
        private readonly List<Entity.Person> _people;
        private readonly ICountryService _country;
        public PersonService( ICountryService con ,bool init=true)
        {
            
            _people = new List<Entity.Person>();

            _country = con;
        }
        public PersonRes AddPerson(PersonAddReq req)
        {
            if (req == null)
                throw new ArgumentNullException(nameof(req));

            // Run attribute-based validation
            var context = new ValidationContext(req, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(req, context, results, validateAllProperties: true);

            if (!isValid)
            {
                // You can throw with all error messages combined
                string errors = string.Join("; ", results.Select(r => r.ErrorMessage));
                throw new ValidationException($"Invalid person data: {errors}");
            }

            Person p = req.ToPerson();
            _people.Add(p);
            return p.ToPersonRes(_country);
        }
        public bool DeletePerson(Guid guid)
        {
            if (guid == null)
                throw new ArgumentNullException(nameof(guid));

            var existingPerson = _people.FirstOrDefault(p => p.Id == guid);

            if (existingPerson == null)
                return false;

            _people.Remove(existingPerson);
            return true;
        }


        public PersonRes EditPerson(PersonEditReq req)
        {
            if (req == null)
                throw new ArgumentNullException(nameof(req));

            // Find the existing person
            var existingPerson = _people.FirstOrDefault(p => p.Id == req.Id);

            if (existingPerson == null)
                throw new KeyNotFoundException("Person not found");

            // Update fields
            existingPerson.FirstName = req.FirstName;
            existingPerson.LastName = req.LastName;
            existingPerson.Email = req.Email;
            existingPerson.PhoneNumber = req.PhoneNumber;
            existingPerson.CountryId = req.CountryId;

            // Return updated PersonRes
            return existingPerson.ToPersonRes(_country);
        }


        public List<PersonRes> GetAllPerson()
        {
            List<PersonRes> reslist = new List<PersonRes>();
            foreach (Person person in _people)
            {
                reslist.Add(person.ToPersonRes(_country));
            }
            return reslist;
        }

        public PersonRes? GetPersonById(Guid? guid)
        {
            if (guid == null)
                throw new ArgumentNullException(nameof(guid));

            var person = _people.FirstOrDefault(p => p.Id == guid.Value);

            return person?.ToPersonRes(_country); // null if not found
        }

    }
}
