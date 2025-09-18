using System.ComponentModel.DataAnnotations;

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
        
    }
}
