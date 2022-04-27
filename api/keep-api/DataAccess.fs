module KeepApi.DataAccess

open System.Linq
open MongoDB
open MongoDB.Driver

type DatabaseSettings =
    { ConnectionString: string
      DatabaseName: string }

type IMongoContext =
    abstract member Collection<'a> : string -> IMongoCollection<'a>
    abstract member Query<'a> : string -> IQueryable<'a>

type MongoContext(settings: DatabaseSettings) =
    let client: MongoClient = MongoClient(settings.ConnectionString)
    let database: IMongoDatabase = client.GetDatabase(settings.DatabaseName) 

    interface IMongoContext with
        member this.Collection<'a> name = database.GetCollection<'a>(name)
        member this.Query<'a> name = database.GetCollection<'a>(name).AsQueryable() :> IQueryable<'a>
