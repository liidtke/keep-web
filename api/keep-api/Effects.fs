module KeepApi.Effects

open DataAccess
open Domain
open SharedKernel
open System
open MongoDB.Driver
open Microsoft.FSharp.Linq
open System.Linq

module User =
    let collectionName = "User"

    let countDuplicates (ctx: IMongoContext) (user: User) =
        let db = ctx.Collection(collectionName)

        let filter =
            Builders.Filter.Eq((fun (x: User) -> x.Email), user.Email)

        db.Find(filter).CountDocuments()

    let save (ctx: IMongoContext) (user: User) =
        let db = ctx.Collection<User>(collectionName)

        let filter =
            Builders.Filter.Eq((fun (x: User) -> x.Id), user.Id)

        try
            if user.Id = Guid.Empty then
                db.InsertOne(user)
                succeed user
            else
                db.ReplaceOne(filter, user) |> ignore
                succeed user
        with
        | ex -> fromException ex


//        let saveOrUpdate =
//            if user.Id = Guid.Empty then
//                let id = Guid.NewGuid()
//                db.InsertOne({ user with Id = id })
//                id
//            else
//                db.ReplaceOne(filter, user) |> ignore
//                user.Id

module Locality =
    let collectionName = "Locality"

    let save (ctx: IMongoContext) (locality: Locality) =
        let db = ctx.Collection<Locality>(collectionName)

        let filter =
            Builders.Filter.Eq((fun (x: Locality) -> x.Id), locality.Id)

        try
            if locality.Id = Guid.Empty then
                db.InsertOne(locality)
                succeed locality
            else
                db.ReplaceOne(filter, locality) |> ignore
                succeed locality
        with
        | ex -> fromException ex


module Course =
    let collectionName = "Course"
    
    let save (ctx: IMongoContext) (entity: Course) =
        let db = ctx.Collection<Course>(collectionName)

        let filter =
            Builders.Filter.Eq((fun (x: Course) -> x.Id), entity.Id)

        try
            if entity.Id = Guid.Empty then
                db.InsertOne(entity)
                succeed entity
            else
                db.ReplaceOne(filter, entity) |> ignore
                succeed entity
        with
        | ex -> fromException ex


module Person =
    let collectionName = "Person"
    
    let save (ctx: IMongoContext) (entity: Person) =
        let db = ctx.Collection<Person>(collectionName)

        let filter =
            Builders.Filter.Eq((fun (x: Person) -> x.Id), entity.Id)

        try
            if entity.Id = Guid.Empty then
                db.InsertOne(entity)
                succeed entity
            else
                db.ReplaceOne(filter, entity) |> ignore
                succeed entity
        with
        | ex -> fromException ex

//testing postgres
//module Person =
//    open Dapper.FSharp
//    open Npgsql
//    open Dapper.FSharp.PostgreSQL
//
//
//    let create () =
//        let connectionString = "Server=127.0.0.1;Port=5432;Database=Keep;User Id=postgres;Password=mysecretpassword;"
//        let conn = new NpgsqlConnection(connectionString);
//        conn.Open();
//
//        let newPerson:Person =
//            { Id = Guid.NewGuid()
//              Name = "Roman"
//              Cell = Some("1")
//              LocationId = Guid.Empty }
//
//        let personTable = table<Person>
//
//        insert {
//           for p in personTable do
//           value newPerson
//        }
//        |> conn.InsertAsync |> Async.AwaitTask |> Async.RunSynchronously
