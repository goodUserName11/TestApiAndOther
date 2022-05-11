namespace TestApi.Entity
{
    public class UserForRequest
    {
        public UserForRequest()
        {

        }

        public UserForRequest(string password, string? name, string? surname, string? patronimic,
            string email, string phone, Guid? role, Guid? registratedBy, string companyInn, string companyName, 
            string companyAdress)
        {
            Password = password;
            Name = name;
            Surname = surname;
            Patronimic = patronimic;
            Email = email;
            Phone = phone;
            Role = role;
            RegistratedBy = registratedBy;
            CompanyInn = companyInn;
            CompanyName = companyName;
            CompanyAddress = companyAdress;
        }

        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronimic { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public Guid? Role { get; set; }
        public Guid? RegistratedBy { get; set; }
        public string? CompanyInn { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }
    }
}
