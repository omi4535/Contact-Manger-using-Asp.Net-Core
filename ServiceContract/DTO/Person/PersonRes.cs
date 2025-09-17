using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.DTO.Person
{
    public class PersonRes
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public String? CountryName { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj is not PersonRes objres)
                return false;

            return objres.Id == this.Id &&
                   objres.FirstName == this.FirstName &&
                   objres.LastName == this.LastName &&
                   objres.Email == this.Email &&
                   objres.PhoneNumber == this.PhoneNumber &&
                   objres.CountryName == this.CountryName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, Email, PhoneNumber, CountryName);
        }



    }

    public static class PersonExtension
    {
        public static PersonRes ToPersonRes(this Entity.Person person,ICountryService _country)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            return new PersonRes
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                PhoneNumber = person.country?.CountryCode +" "+ person.PhoneNumber,
                CountryName = person.country?.CountryName
            };
        }
    }
}
