using System.Collections.Generic;

namespace RegistrationForm.DAL.src.Entities
{
    public class Province
    {
        public int ProvinceId { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}