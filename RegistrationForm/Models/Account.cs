namespace RegistrationForm.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Login { get; set; }
        public bool AgreeToWorkForFood { get; set; }
        public int ProvinceId { get; set; }
    }
}