namespace MongoAndCSharp
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Driver;

    class Program
    {
        static void Main(string[] args)
        {
            // CRUD OPERATIONS
            // CREATE
            CreateMongoWithBsonDocument();
            CreateMongoWithClass();
            CreateMongoManyWithBsonDocument();
            CreateMongoManyWithClass();

            // READ
            UseOfFind();
            FilterWithBsonDocument();
            FilterWithString();
            FilterByIdWithBsonDocument();
            FilterByIdWithString();
            FilterUsinEQWithBsonDocument();
            FilterUsinEQWithString();
            FilterUsinEQWithFilterDifinitionBuilder();
            FilterUsinEQWithLambdaExpression();

            // UPDATE
            UpdateWithBsonDocument();

            // DELETE
            DeleteWithBsonDocument();

            Console.WriteLine("The program has ended, press any key");
            Console.ReadKey();
        }

        #region Create
        static void CreateMongoWithBsonDocument()
        {
            var collection = new MongoDb<BsonDocument>().GetCollection("student");

            //Save Data In Mongo using a BsonDocument object
            var document = new BsonDocument
            {
                { "name", "Winston Churchill" },
                { "undergrad", true },
                { "units", "3" },
                { "classes", new BsonArray {
                    "English",
                    "Math",
                    "Spanish"
                }},
            };

            collection.InsertOne(document);

            Console.WriteLine("The record has been successfully created (scenario 1)");
        }

        static void CreateMongoWithClass()
        {
            var collection = new MongoDb<Student>().GetCollection("student");

            //Save Data In Mongo using a BsonDocument object
            var document = new Student
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Keanu Reeves",
                Undergrad = false,
                Units = 3,
                Classes = new List<string>
                {
                    "History",
                    "Math",
                    "Chemistry"
                }
            };

            collection.InsertOne(document);

            Console.WriteLine("The record has been successfully created (scenario 2)");
        }

        static void CreateMongoManyWithBsonDocument()
        {
            var collection = new MongoDb<BsonDocument>().GetCollection("student");

            var documents = new List<BsonDocument>
            {
                new BsonDocument
                {
                    { "name", "Winston Churchill" },
                    { "undergrad", true },
                    { "units", "3" },
                    { "classes", new BsonArray {
                        "English",
                        "Math",
                        "Spanish"
                    }}
                },
                new BsonDocument
                {
                    { "name", "Winston Churchill" },
                    { "undergrad", true },
                    { "units", "3" },
                    { "classes", new BsonArray {
                        "English",
                        "Math",
                        "Spanish"
                    }}
                },
                new BsonDocument
                {
                    { "name", "Winston Churchill" },
                    { "undergrad", true },
                    { "units", "3" },
                    { "classes", new BsonArray {
                        "English",
                        "Math",
                        "Spanish"
                    }}
                },
            };

            collection.InsertMany(documents);

            Console.WriteLine("The records has been successfully created (scenario 3)");
        }

        static void CreateMongoManyWithClass()
        {
            var collection = new MongoDb<Student>().GetCollection("student");

            var documents = new List<Student>
            {
                new Student
                {
                    Id = ObjectId.GenerateNewId(),
                    Name = "Keanu Reeves",
                    Undergrad = false,
                    Units = 3,
                    Classes = new List<string>
                    {
                        "History",
                        "Math",
                        "Chemistry"
                    }
                },
                new Student
                {
                    Id = ObjectId.GenerateNewId(),
                    Name = "Keanu Reeves",
                    Undergrad = false,
                    Units = 3,
                    Classes = new List<string>
                    {
                        "History",
                        "Math",
                        "Chemistry"
                    }
                },
                new Student
                {
                    Id = ObjectId.GenerateNewId(),
                    Name = "Keanu Reeves",
                    Undergrad = false,
                    Units = 3,
                    Classes = new List<string>
                    {
                        "History",
                        "Math",
                        "Chemistry"
                    }
                }
            };

            collection.InsertMany(documents);

            Console.WriteLine("The records has been successfully created (scenario 4)");
        }
        #endregion

        #region Read
        static void UseOfFind()
        {
            var collection = new MongoDb<BsonDocument>().GetCollection("student");

            collection.Find(FilterDefinition<BsonDocument>.Empty).ForEachAsync(doc => Console.WriteLine(doc));

        }

        static void FilterWithBsonDocument()
        {
            var collection = new MongoDb<BsonDocument>().GetCollection("student");

            var filter = new BsonDocument("Name", "Keanu Reeves");

            collection.Find(filter).ForEachAsync(doc => Console.WriteLine(doc));
        }

        static void FilterWithString()
        {
            var collection = new MongoDb<BsonDocument>().GetCollection("student");

            var filter = "{Name: 'Keanu Reeves'}";

            collection.Find(filter).ForEachAsync(doc => Console.WriteLine(doc));
        }

        static void FilterByIdWithBsonDocument()
        {
            var collection = new MongoDb<BsonDocument>().GetCollection("student");

            var filter = new BsonDocument("_id", new ObjectId("5ea37c1edc6da61880bff131"));

            var doc = collection.Find(filter).FirstOrDefault();

            Console.WriteLine(doc);
        }

        static void FilterByIdWithString()
        {
            var collection = new MongoDb<BsonDocument>().GetCollection("student");

            var filter = "{_id: ObjectId('5ea37c1edc6da61880bff131')}";

            var doc = collection.Find(filter).FirstOrDefault();

            Console.WriteLine(doc);
        }

        static void FilterUsinEQWithBsonDocument()
        {
            var collection = new MongoDb<BsonDocument>().GetCollection("student");

            var filter = new BsonDocument("undergrad", new BsonDocument("$eq", false));

            collection.Find(filter).ForEachAsync(doc => Console.WriteLine(doc));
        }

        static void FilterUsinEQWithString()
        {
            var collection = new MongoDb<BsonDocument>().GetCollection("student");

            var filter = "{undergrad: {'$eq': false}}";

            collection.Find(filter).ForEachAsync(doc => Console.WriteLine(doc));
        }

        static void FilterUsinEQWithFilterDifinitionBuilder()
        {
            var collection = new MongoDb<BsonDocument>().GetCollection("student");

            var filter = new FilterDefinitionBuilder<BsonDocument>().Eq("undergrad", false);

            collection.Find(filter).ForEachAsync(doc => Console.WriteLine(doc));
        }

        static void FilterUsinEQWithLambdaExpression()
        {
            var collection = new MongoDb<Student>().GetCollection("student");

            collection.Find(x => x.Undergrad == false).ForEachAsync(doc => Console.WriteLine($"{doc.Id} - {doc.Name}"));
        }
        #endregion

        #region Update
        static void UpdateWithBsonDocument()
        {
            var collection = new MongoDb<BsonDocument>().GetCollection("student");

            var filter = new BsonDocument("_id", new ObjectId("5ea24b49dc6da638a88216d7"));
            var updateValues = new BsonDocument( new List<BsonElement>
            {
                new BsonElement("name", "Aurora Boreal"),
                new BsonElement("undergrad", true),
                new BsonElement("units", 10),
                new BsonElement("classes", new BsonArray { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", })
            });

            collection.ReplaceOne(filter, updateValues);

            Console.WriteLine("The record has been succcessfully updated");
        }
        #endregion

        #region Delete
        static void DeleteWithBsonDocument()
        {
            var collection = new MongoDb<BsonDocument>().GetCollection("student");

            var filter = new BsonDocument("_id", new ObjectId("5ea24b49dc6da638a88216d7"));

            collection.DeleteOne(filter);

            Console.WriteLine("The record has been succcessfully deleted");
        }
        #endregion

    }

    class MongoDb<T> where T : class
    {
        private readonly string dbServer = "mongodb://localhost:27017";
        private readonly string dbName = "myfirstdb";
        private readonly IMongoDatabase _db;

        public MongoDb()
        {
            MongoClient client = new MongoClient(dbServer);
            _db = client.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection(string collectionName)
        {
            return _db.GetCollection<T>(collectionName);
        }
    }

    class Student
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public bool Undergrad { get; set; }
        public int Units { get; set; }
        public List<string> Classes { get; set; }
    }
}
