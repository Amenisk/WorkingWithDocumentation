namespace WorkingWithDocumentation.Data
{
    public class Developer : User
    {
        public string? PSRN { get; set; }
        public string? ITN { get; set; }
        public string? KPP { get; set; }
        public string? Address { get; set; }
        public string? HeadOfExecutiveCommitteeRTFN { get; set; }
        public Developer(string login, string password, string role, string email) 
            : base(login, password, role, email)
        {
        }
    }
}
