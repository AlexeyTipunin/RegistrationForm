using System.Collections.Generic;

namespace RegistrationForm.DAL.src.Entities
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }

        public ICollection<Province> Provinces { get; set; }
    }
}
