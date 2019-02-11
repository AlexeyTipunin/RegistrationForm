using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationForm.Models
{
    public class Country
    {
        [Required]
        public int CountryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
