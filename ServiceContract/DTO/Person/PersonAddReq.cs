using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.DTO.Person
{
    public class PersonAddReq
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string? LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Select Country")]
        public Guid CountryId { get; set; }
        public Entity.Person ToPerson()
        {
            return new Entity.Person
            {
                Id = Guid.NewGuid(),       // generate new ID for new person
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                CountryId = this.CountryId
            };
        }
    }
}
