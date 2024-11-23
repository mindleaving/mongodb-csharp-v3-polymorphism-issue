# Demonstration of issue with polymorphism in MongoDB C# Driver version 3.0.0

Trying to query a derived type using .OfType<> causes NotSupportedException: "OfType is not supported with the configured discriminator convention"
