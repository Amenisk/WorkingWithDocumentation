namespace WorkingWithDocumentation.Data
{
    public class Designer : User
    {
        public string? NameOfDesignOrg { get; set; }
        public string? PSRN { get; set; }
        public string? ITN{ get; set; }
        public string? KPP { get; set; }
        public string? Address { get; set; }
        public string? DirectorFN { get; set; }
        public string? ChiefProjectEngineerFN { get; set; }
        public Designer(string login, string password, string role, string email) 
            : base(login, password, role, email)
        {
        }
    }
}
