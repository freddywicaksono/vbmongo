Imports MongoDB.Driver
Imports MongoDB.Bson
Module MyMod
    'Public dbClient = New MongoClient("mongodb+srv://Freddy:K6G5A7X6AM@clusterumc-gg1zy.mongodb.net/akademik?authSource=admin&replicaSet=ClusterUMC-shard-0&connectTimeoutMS=360000&socketTimeoutMS=360000&readPreference=primary&ssl=true")
    Public dbClient = New MongoClient("mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false")
    Public db As IMongoDatabase = dbClient.GetDatabase("akademik")

    Public oMhs As MongoDB.Driver.IMongoCollection(Of MongoDB.Bson.BsonDocument)
    Public barang_baru As Boolean
    Public docs As System.Collections.Generic.List(Of BsonDocument)

End Module
