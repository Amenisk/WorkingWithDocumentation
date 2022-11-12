using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkingWithDocumentation.Data
{
    public class Project
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        [BsonIgnoreIfDefault]
        public int NumberOfProject { get; set; }
        public string Type { get; set; }
        [BsonIgnoreIfDefault]
        public Customer RespCustomer { get; set; }
        [BsonIgnoreIfDefault]
        public Designer  RespDesigner { get; set; }
        [BsonIgnoreIfDefault]
        public Developer RespDeveloper { get; set; }
        [BsonIgnoreIfDefault]
        public List<Document> Documents { get; set; } = new List<Document>();
        [BsonIgnoreIfDefault]
        public WaterSupplyForm WaterSupply { get; set; }
        [BsonIgnoreIfDefault]
        public GasSupplyForm GasSupply { get; set; }

        public Project(string type, Customer respCustomer, Designer respDesigner, Developer respDeveloper, 
            int lastNumber, List<Document> documents)
        {
            Type = type;
            RespCustomer = respCustomer;
            RespDesigner = respDesigner;
            RespDeveloper = respDeveloper;
            NumberOfProject = lastNumber + 1;
            Documents = documents;
        }

        public void AddDocument(Document document)
        {
            Documents.Add(document);
        }
    }
}
