using MongoDB.Bson.Serialization.Attributes;

namespace WorkingWithDocumentation.Data
{
    public class Customer : User
    {
        [BsonIgnoreIfDefault]
        public string? Departament { get; set; }
        [BsonIgnoreIfDefault]
        public string? FullName { get; set; }

        public Customer(string login, string password, string role, string email) 
            : base(login, password, role, email)
        {
        }
    }
}
