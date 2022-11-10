namespace WorkingWithDocumentation.Data
{
    public class Customer : User
    {
        public string? Departament { get; set; }
        public string? FullName { get; set; }

        public Customer(string login, string password, string role, string email) 
            : base(login, password, role, email)
        {
        }
    }
}
