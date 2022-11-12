using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics.Eventing.Reader;

namespace WorkingWithDocumentation.Data
{
    public class ProjectService
    {
        public int NumberOfCurrentProject { get; set; }
        public int GetLastNumberOfProject()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Project>("Projects");
            var project = collection.Find(x => x.NumberOfProject != 0).Sort("{NumberOfProject: -1}").FirstOrDefault<Project>();

            if(project is not null)
            {
                return project.NumberOfProject;
            }
            else
            {
                return 0;
            }
        }
        public int GetLastNumberOfDocument()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Document>("Documents");
            var document = collection.Find(x => x.NumberOfDocument != 0).Sort("{NumberOfDocument: -1}").FirstOrDefault<Document>();

            if (document is not null)
            {
                return document.NumberOfDocument;
            }
            else
            {
                return 0;
            }
        }
        public void SaveProjectTemplate(Project project)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Project>("ProjectTemplates");

            collection.InsertOne(project);
        }

        public void AddProjectToDB(Project project)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Project>("Projects");

            collection.InsertOne(project);
        }
        public void AddDocumentToDB(Document document)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Document>("Documents");

            collection.InsertOne(document);
        }
        public List<int> GetListOfNumberProjects(string departament)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Project>("Projects");
            var projects = collection.Find(x => x.Type == departament).ToList<Project>();
            List<int> numbersOfProjects = new List<int>();

            foreach (var pr in projects)
            {
                numbersOfProjects.Add(pr.NumberOfProject);
            }

            return numbersOfProjects;
        }

        public Project GetProjectFromDBByNumber(int number)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Project>("Projects");

            return collection.Find(x => x.NumberOfProject == number).FirstOrDefault();
        }

        public List<Document> GetTemplateFiles(string departament)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Project>("ProjectTemplates");

            return collection.Find(x => x.Type == departament).FirstOrDefault().Documents;
        }

        public void UpdateDocumentBoolByNumber(Document doc)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Document>("Documents");
            var filter = Builders<Document>.Filter.Eq("NumberOfDocument", doc.NumberOfDocument);
            var update1 = Builders<Document>.Update.Set("IsImportant", doc.IsImportant);
            var update2 = Builders<Document>.Update.Set("IsApproved", doc.IsApproved);

            collection.UpdateOne(filter, update1);
            collection.UpdateOne(filter, update2);
        }
    }
}
