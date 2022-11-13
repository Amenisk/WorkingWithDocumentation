﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkingWithDocumentation.Data
{
    public class Document
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        [BsonIgnoreIfDefault]
        public int NumberOfDocument { get; set; }
        [BsonIgnoreIfDefault]
        public string FileName { get; set; }
        public string DocumentRole { get; set; }
        public string NameOfDocument { get; set; }
        public bool IsImportant { get; set; }
        public bool IsApproved { get; set; }

        public Document(string nameOfDocument, string documentRole, int lastNumber)
        {
            NameOfDocument = nameOfDocument;
            DocumentRole = documentRole;
            NumberOfDocument = lastNumber + 1;
        }
    }
}
