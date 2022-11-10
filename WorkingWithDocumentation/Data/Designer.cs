using MongoDB.Bson.Serialization.Attributes;

namespace WorkingWithDocumentation.Data
{
    public class Designer : User
    {
        [BsonIgnoreIfDefault]
        public string? NameOfDesignOrg { get; set; }
        [BsonIgnoreIfDefault]
        public string? PSRN { get; set; }
        [BsonIgnoreIfDefault]
        public string? ITN{ get; set; }
        [BsonIgnoreIfDefault]
        public string? KPP { get; set; }
        [BsonIgnoreIfDefault]
        public string? Address { get; set; }
        [BsonIgnoreIfDefault]
        public string? DirectorFN { get; set; }
        [BsonIgnoreIfDefault]
        public string? ChiefProjectEngineerFN { get; set; }

        public Designer(string login, string password, string role, string email) 
            : base(login, password, role, email)
        {
        }
    }
}
