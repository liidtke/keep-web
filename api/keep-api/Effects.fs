module KeepApi.Effects

open DataAccess
open Domain
open SharedKernel
open System
open MongoDB.Driver
open Microsoft.FSharp.Linq
open System.Linq

let output id (result: DeleteResult) =
    if result.DeletedCount > 0 then
        succeed id
    else
        validation "Erro ao deletar item"

module User =
    let collectionName = "User"

    let getOne (ctx: IMongoContext) (email: string) =
        ctx.Query<User>(collectionName)
        |> Seq.tryFind (fun x -> x.Email = email)

    /// Searches for the original user in the database
    let getById (ctx: IMongoContext) (user: User) =
        let u =
            ctx.Query<User>(collectionName)
            |> Seq.tryFind (fun x -> x.Id = user.Id)

        match u with
        | Some u -> succeed u
        | None _ -> validation "Usuário não encontrado"

    let countDuplicates (ctx: IMongoContext) (user: User) =
        let db = ctx.Collection(collectionName)

        let filter = Builders.Filter.Eq((fun (x: User) -> x.Email), user.Email)

        db.Find(filter).CountDocuments()

    let save (ctx: IMongoContext) (user: User) =
        let db = ctx.Collection<User>(collectionName)

        let filter = Builders.Filter.Eq((fun (x: User) -> x.Id), user.Id)

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

        let filter = Builders.Filter.Eq((fun (x: Locality) -> x.Id), locality.Id)

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

        let filter = Builders.Filter.Eq((fun (x: Course) -> x.Id), entity.Id)

        try
            if entity.Id = Guid.Empty then
                db.InsertOne(entity)
                succeed entity
            else
                db.ReplaceOne(filter, entity) |> ignore
                succeed entity
        with
        | ex -> fromException ex


module Student =
    let collectionName = "Student"

    let getLocality (ctx: IMongoContext) (entity: Student) = true

    let save (ctx: IMongoContext) (entity: Student) =
        let db = ctx.Collection<Student>(collectionName)

        let filter = Builders.Filter.Eq((fun (x: Student) -> x.Id), entity.Id)

        try
            if entity.Id = Guid.Empty then
                db.InsertOne(entity)
                succeed entity
            else
                db.ReplaceOne(filter, entity) |> ignore
                succeed entity
        with
        | ex -> fromException ex

    let delete (ctx: IMongoContext) (studentId: Guid) =
        let db = ctx.Collection<Student>(collectionName)
        let filter = Builders.Filter.Eq((fun (x: Student) -> x.Id), studentId)

        try
            db.DeleteOne(filter) |> ignore
            succeed studentId
        with
        | ex -> fromException ex



module Registration =
    let collectionName = "Registration"

    let save (ctx: IMongoContext) (entity: Registration) =
        let db = ctx.Collection<Registration>(collectionName)

        let filter = Builders.Filter.Eq((fun (x: Registration) -> x.Id), entity.Id)

        try
            if entity.Id = Guid.Empty then
                db.InsertOne(entity)
                succeed entity
            else
                db.ReplaceOne(filter, entity) |> ignore
                succeed entity
        with
        | ex -> fromException ex

    let delete (ctx: IMongoContext) id =
        let db = ctx.Collection<Registration>(collectionName)

        let filter = Builders.Filter.Eq((fun (x: Registration) -> x.Id), id)

        db.DeleteOne(filter) |> output id


module Question =
    let collectionName = "Question"

    let save (ctx: IMongoContext) (entity: Question) =
        let db = ctx.Collection<Question>(collectionName)

        let filter = Builders.Filter.Eq((fun (x: Question) -> x.Id), entity.Id)

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
