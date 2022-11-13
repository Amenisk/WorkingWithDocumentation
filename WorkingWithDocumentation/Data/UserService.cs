﻿using DnsClient;
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
        public bool IsAuthorized()
        {
            return CurrentCustomer is not null 
                || CurrentDesigner is not null 
                || CurrentDeveloper is not null;
        }
        public void LogOut()
        {
            CurrentCustomer = null;
            CurrentDesigner = null;
            CurrentDeveloper = null;
            LoginOfCurrentUser = null;
        }

        public List<string> GetNamesOfDesignOrgs()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Designer>("Designers");
            List<string> names = collection.Find(x => x.NameOfDesignOrg != null).ToList().Select(x => x.NameOfDesignOrg).ToList<string>();

            return names;
        }

        public List<string> GetDeveloperNames()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Developer>("Developers");
            List<string> names = collection.Find(x => x.DeveloperName != null).ToList().Select(x => x.DeveloperName).ToList<string>();

            return names;
        }

        public Designer FindDesignerByName(string nameOfOrg)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Designer>("Designers");

            return collection.Find(x => x.NameOfDesignOrg == nameOfOrg).FirstOrDefault();
        }
        public Developer FindDeveloperByName(string developerName)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Developer>("Developers");

            return collection.Find(x => x.DeveloperName == developerName).FirstOrDefault();
        }

        public bool CheckLogin(string login)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection1 = database.GetCollection<Customer>("Customers");
            var collection2 = database.GetCollection<Designer>("Designers");
            var collection3 = database.GetCollection<Developer>("Developers");
            var customer = collection1.Find(x => x.Login == login).FirstOrDefault();
            var designer = collection2.Find(x => x.Login == login).FirstOrDefault();
            var developer = collection3.Find(x => x.Login == login).FirstOrDefault();

            return customer is not null || designer is not null || developer is not null;
        }
    }
}
