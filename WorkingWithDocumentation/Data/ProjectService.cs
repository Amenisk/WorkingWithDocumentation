using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
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

        public List<int> GetListOfNumberProjectsByUser(string login, string role)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Project>("Projects");
            List<Project> projects;
            if (role == "designer")
                projects = collection.Find(x => x.RespDesigner.Login == login).ToList<Project>();
            else
                projects = collection.Find(x => x.RespDeveloper.Login == login).ToList<Project>();


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
        public Document GetDocumentFromDBByNumber(int number)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Document>("Documents");

            return collection.Find(x => x.NumberOfDocument == number).FirstOrDefault();
        }

        public List<Document> GetTemplateFiles(string departament)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Project>("ProjectTemplates");

            return collection.Find(x => x.Type == departament).FirstOrDefault().Documents;
        }

        public void UpdateDocumentBoolByNumber(Document doc, int numberOfProject)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection1 = database.GetCollection<Document>("Documents");
            var collection2 = database.GetCollection<Project>("Projects");
            var filter = Builders<Document>.Filter.Eq("NumberOfDocument", doc.NumberOfDocument);
            var update1 = Builders<Document>.Update.Set("IsImportant", doc.IsImportant);
            var update2 = Builders<Document>.Update.Set("IsApproved", doc.IsApproved);
            var project = collection2.Find(x => x.NumberOfProject == numberOfProject).FirstOrDefault();

            collection1.UpdateOne(filter, update1);
            collection1.UpdateOne(filter, update2);
            project.Documents.Find(x => x.NumberOfDocument == doc.NumberOfDocument).IsImportant = doc.IsImportant;
            project.Documents.Find(x => x.NumberOfDocument == doc.NumberOfDocument).IsApproved = doc.IsApproved;

            collection2.ReplaceOne(x => x.NumberOfProject == numberOfProject, project);
        }

        public async Task UploadDocumentToDbAsync(Stream fs, string name)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var gridFS = new GridFSBucket(database);

            await gridFS.UploadFromStreamAsync(name, fs);
        }

        public bool CheckLoadDocumentByNumber(string name)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<GridFSFileInfo>("fs.files");

            var info = collection.Find(x => x.Filename == name).FirstOrDefault();

            if (info is null)
                return false;
            else
                return true;
        }

        public void DownloadToLocal(int number)
        {            
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Document>("Documents");
            var doc = collection.Find(x => x.NumberOfDocument == number).FirstOrDefault();
            var gridFS = new GridFSBucket(database);
            using (FileStream fs = new FileStream($"{Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/wwwroot/Docs/")}{doc.FileName}", FileMode.CreateNew))
            {
                gridFS.DownloadToStreamByName(doc.FileName, fs);
            }
        }

        public void UpdateFileName(int numberOfDocument, string fileName)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Document>("Documents");
            var doc = collection.Find(x => x.NumberOfDocument == numberOfDocument).FirstOrDefault();
            var filter = Builders<Document>.Filter.Eq("NumberOfDocument", doc.NumberOfDocument);
            var update = Builders<Document>.Update.Set("FileName", fileName);

            collection.UpdateOne(filter, update);
        }

        public void SaveWaterForm(WaterSupplyForm form, int numberOfProject)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Project>("Projects");
            var filter = Builders<Project>.Filter.Eq("NumberOfProject", numberOfProject);
            var update = Builders<Project>.Update.Set("WaterSupply", form);

            collection.UpdateOne(filter, update);
        }

        public void SaveGasForm(GasSupplyForm form, int numberOfProject)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection = database.GetCollection<Project>("Projects");
            var filter = Builders<Project>.Filter.Eq("NumberOfProject", numberOfProject);
            var update = Builders<Project>.Update.Set("GasSupply", form);

            collection.UpdateOne(filter, update);
        }

        public void DeleteDocument(int numberOfDocument)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DocumentFlow");
            var collection1 = database.GetCollection<GridFSFileInfo>("fs.files");
            var collection2 = database.GetCollection<Document>("Documents");
            var doc = collection2.Find(x => x.NumberOfDocument == numberOfDocument).FirstOrDefault();
            doc.FileName = null;

            collection1.DeleteOne(x => x.Filename == doc.FileName);
            collection2.ReplaceOne(x => x.NumberOfDocument == doc.NumberOfDocument, doc);
        }
    }
}
