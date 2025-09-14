    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Entity
    {
        public class Person
        {
            [Key]
            public Guid Id { get; set; } = Guid.NewGuid();

            [Required, MaxLength(50)]
            public string FirstName { get; set; } = string.Empty;

            [MaxLength(50)]
            public string? LastName { get; set; }

            [EmailAddress]
            public string? Email { get; set; }

            [Phone]
            public string? PhoneNumber { get; set; }
        
            public Guid CountryId { get; set; }
        }
    }
