using DnsClient;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace WorkingWithDocumentation.Data
{
    public class UserService
    {
        public string? LoginOfCurrentUser { get; set; }
        public Customer? CurrentCustomer { get; private set; }
        public Designer? CurrentDesigner { get; private set; }
        public Developer? CurrentDeveloper { get; private set; }
        public void AddCustomerToDB(Customer customer)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Customer>("Customers");

            collection.InsertOne(customer);
        }

        public void AddDesignerToDB(Designer designer)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Designer>("Designers");

            collection.InsertOne(designer);
        }

        public void AddDeveloperToDB(Developer developer)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Developer>("Developers");

            collection.InsertOne(developer);
        }

        public void AuthorizeCustomer(string login, string password)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Customer>("Customers");

            ChooseCustomer(collection.Find(x => x.Login == login && x.Password == password).FirstOrDefault<Customer>());
        }
        public void AuthorizeDesigner(string login, string password)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Designer>("Designers");

            ChooseDesigner(collection.Find(x => x.Login == login && x.Password == password).FirstOrDefault<Designer>());
        }
        public void AuthorizeDeveloper(string login, string password)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Developer>("Developers");

            ChooseDeveloper(collection.Find(x => x.Login == login && x.Password == password).FirstOrDefault<Developer>());
        }

        private void ChooseCustomer(Customer customer)
        {
            if(customer is not null)
            {
                LoginOfCurrentUser = customer.Login;
                CurrentCustomer = customer;
                CurrentDesigner = null;
                CurrentDeveloper = null;
            }
        }
        private void ChooseDesigner(Designer designer)
        {
            if(designer is not null)
            {
                LoginOfCurrentUser = designer.Login;
                CurrentCustomer = null;
                CurrentDesigner = designer;
                CurrentDeveloper = null;
            }  
        }
        public void ReplaceCustomer(Customer customer)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Customer>("Customers");
            CurrentCustomer = customer;

            collection.ReplaceOne(x => x.Login == customer.Login, customer);
        }
        public void ReplaceDesigner(Designer designer)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Designer>("Designers");
            CurrentDesigner = designer;

            collection.ReplaceOne(x => x.Login == designer.Login, designer);
        }
        public void ReplaceDeveloper(Developer developer)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Developer>("Developers");
            CurrentDeveloper = developer;

            collection.ReplaceOne(x => x.Login == developer.Login, developer);
        }
        private void ChooseDeveloper(Developer developer)
        {
            if(developer is not null)
            {
                LoginOfCurrentUser = developer.Login;
                CurrentCustomer = null;
                CurrentDesigner = null;
                CurrentDeveloper = developer;
            }    
        }
    }
}
