using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.DTO.Person
{
    public class PersonEditReq
    {
        public Guid Id { get; set; }             // needed for update
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid CountryId { get; set; }

        public Entity.Person ToPerson(Entity.Person existingPerson)
        {
            if (existingPerson == null)
                throw new ArgumentNullException(nameof(existingPerson));

            // update fields
            existingPerson.FirstName = this.FirstName;
            existingPerson.LastName = this.LastName;
            existingPerson.Email = this.Email;
            existingPerson.PhoneNumber = this.PhoneNumber;
            existingPerson.CountryId = this.CountryId;

            return existingPerson;
        }
    }
}
