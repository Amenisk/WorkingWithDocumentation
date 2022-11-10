using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace WorkingWithDocumentation.Data
{
    public class UserService
    {
        public User CurrentUser { get; set; }
        public void AddToDatabase(User user)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<User>("Users");
            var us = collection.Find(x => x.Login == user.Login).FirstOrDefault<User>();
            if (us == null)
            {
                collection.InsertOne(user);
            }
        }

        public User CheckUser(string login, string password)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Users");
            var collection = database.GetCollection<User>("Characters");
            return collection.Find(x => x.Login == login && x.Password == password).FirstOrDefault<User>();
        }
    }
}
