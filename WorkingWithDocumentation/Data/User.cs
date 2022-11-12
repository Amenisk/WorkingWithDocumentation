using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkingWithDocumentation.Data
{
    public abstract class User
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string Login { get; private set; }
        public string Password { get; set; }
        public string Email { get ; set; }
        [BsonIgnoreIfDefault]
        public string Telephone { get; set; }
        public string Role { get; set; }

        public User(string login, string password, string role, string email)
        {
            Login = login;
            Password = password;
            Role = role;
            Email = email;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
