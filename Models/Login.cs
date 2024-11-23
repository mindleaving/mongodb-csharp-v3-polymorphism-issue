using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbDriverV3PolymorphismBug.Models;

[BsonKnownTypes(typeof(LocalLogin), typeof(ExternalLogin))]
public abstract class Login
{
    public Guid Id { get; set; }
}

public class LocalLogin : Login
{
    public string Username { get; set; }
}

public class ExternalLogin : Login
{
    public string ExternalAccountId { get; set; }
}