using Entity;
using Microsoft.EntityFrameworkCore;
using ServiceContract;
using ServiceContract.DTO.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace services
{
    public class PersonService : IPerson
    {
        private readonly ContactMangerDBContext _Db;
        private readonly ICountryService _country;

        public PersonService(ContactMangerDBContext contactMangerDbContext, ICountryService con, bool init = true)
        {
            _Db = contactMangerDbContext;
            _country = con;
        }

        public async Task<PersonRes> AddPersonAsync(PersonAddReq req)
        {
            if (req == null)
                throw new ArgumentNullException(nameof(req));

            // Run attribute-based validation
            var context = new ValidationContext(req, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(req, context, results, validateAllProperties: true);

            if (!isValid)
            {
                string errors = string.Join("; ", results.Select(r => r.ErrorMessage));
                throw new ValidationException($"Invalid person data: {errors}");
            }

            Person p = req.ToPerson();
            await _Db.people.AddAsync(p);
            await _Db.SaveChangesAsync();
            return p.ToPersonRes(_country);
        }

        public async Task<bool> DeletePersonAsync(Guid guid)
        {
            var existingPerson = await _Db.people.FirstOrDefaultAsync(p => p.Id == guid);

            if (existingPerson == null)
                return false;

            _Db.people.Remove(existingPerson);
            await _Db.SaveChangesAsync();
            return true;
        }

        public async Task<PersonRes> EditPersonAsync(PersonEditReq req)
        {
            if (req == null)
                throw new ArgumentNullException(nameof(req));

            var existingPerson = await _Db.people.FirstOrDefaultAsync(p => p.Id == req.Id);

            if (existingPerson == null)
                throw new KeyNotFoundException("Person not found");

            existingPerson.FirstName = req.FirstName;
            existingPerson.LastName = req.LastName;
            existingPerson.Email = req.Email;
            existingPerson.PhoneNumber = req.PhoneNumber;
            existingPerson.CountryId = req.CountryId;

            await _Db.SaveChangesAsync();

            return existingPerson.ToPersonRes(_country);
        }

        public async Task<List<PersonRes>> GetAllPersonAsync()
        {
            var peopleList = await _Db.people.ToListAsync();

            List<PersonRes> reslist = new List<PersonRes>();
            foreach (Person person in peopleList)
            {
                reslist.Add(person.ToPersonRes(_country));
            }

            return reslist;
        }

        public async Task<PersonRes?> GetPersonByIdAsync(Guid? guid)
        {
            if (guid == null)
                throw new ArgumentNullException(nameof(guid));

            var person = await _Db.people.FirstOrDefaultAsync(p => p.Id == guid.Value);
            return person?.ToPersonRes(_country);
        }

        public async Task GetAllPersonWithSpAsync()
        {
            // Assuming you have a stored procedure mapped in your DbContext
            List<Person> people = await _Db.GetAllPerson();
            // You could map them to PersonRes if needed
        }
    }
}
