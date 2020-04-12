Imports MongoDB.Driver
Imports MongoDB.Bson
Module MyMod
    
    Public dbClient = New MongoClient("mongodb://localhost:27017/?readPreference=primary&ssl=false")
    Public db As IMongoDatabase = dbClient.GetDatabase("akademik")

    Public oMhs As MongoDB.Driver.IMongoCollection(Of MongoDB.Bson.BsonDocument)
    Public barang_baru As Boolean
    Public docs As System.Collections.Generic.List(Of BsonDocument)

End Module
