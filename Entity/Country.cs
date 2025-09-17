using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Entity
{
    public class Country
    {
        [Key]
        public Guid id { get; set; }
        [StringLength(30)]
        public string CountryName { get; set; }
        [StringLength(5)]
        public string? CountryCode { get; set; }
        public ICollection<Person> PeopleFromCountry { get; set; }
    }
}
