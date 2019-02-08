namespace RegistrationForm.DAL.src.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public bool AgreeToWorkForFood { get; set; }

        public int ProvinceId { get; set; }
        public Province Province { get; set; }
    }
}