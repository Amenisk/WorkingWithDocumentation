using MongoDB.Bson.Serialization.Attributes;

namespace WorkingWithDocumentation.Data
{
    public class Developer : User
    {
        [BsonIgnoreIfDefault]
        public string? DeveloperName { get; set; }
        [BsonIgnoreIfDefault]
        public string? PSRN { get; set; }
        [BsonIgnoreIfDefault]
        public string? ITN { get; set; }
        [BsonIgnoreIfDefault]
        public string? KPP { get; set; }
        [BsonIgnoreIfDefault]
        public string? Address { get; set; }
        [BsonIgnoreIfDefault]
        public string? HeadOfExecutiveCommitteeRTFN { get; set; }

        public Developer(string login, string password, string role, string email) 
            : base(login, password, role, email)
        {
        }
    }
}
