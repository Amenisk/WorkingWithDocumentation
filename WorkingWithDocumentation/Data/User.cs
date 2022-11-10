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
        public string Telephone { get; set; }

        public User(string login, string password, string role, string email)
        {
            Login = login;
            Password = password;
            Email = email;
        }
    }
}
