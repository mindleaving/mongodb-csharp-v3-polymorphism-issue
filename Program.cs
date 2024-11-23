using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDbDriverV3PolymorphismBug.Models;

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
var mongoDbSettings = new MongoClientSettings
{
    Credential = MongoCredential.CreateCredential("admin", "dev", File.ReadAllText(@"C:\temp\mongodb-dev-credential.txt"))
};
var mongoClient = new MongoClient(mongoDbSettings);
var database = mongoClient.GetDatabase("MongoDbPolymorphismTest");
var loginCollection = database.GetCollection<Login>(nameof(Login));

var localLogin = new LocalLogin
{
    Id = Guid.NewGuid(),
    Username = "test"
};
await loginCollection.InsertOneAsync(localLogin);
var searchResult = await loginCollection
    .OfType<LocalLogin>() // <--- Causes NotSupportedException "OfType is not supported with the configured discriminator convention"
    .Find(x => x.Username == localLogin.Username)
    .FirstOrDefaultAsync();

if(searchResult != null)
    Console.WriteLine("Successful roundtrip");
else
    Console.WriteLine("FAILED");

