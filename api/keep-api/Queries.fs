module KeepApi.Queries

open System
open KeepApi.Domain
open KeepApi.DataAccess
open System.Linq
open Microsoft.FSharp.Linq
open FSharp.Linq
open System.Linq.Expressions;

let toList (collection:IQueryable<'T>) =
    collection.ToList()

module Course =
    let getAll (ctx: IMongoContext) =
        let collection = ctx.Query<Course>("Course")
        toList collection

module Question =
    let getAll (ctx: IMongoContext) =
        let collection = ctx.Query<Question>("Question")
        toList collection

module User =
    let getAll (ctx: IMongoContext) =
        let collection = ctx.Query<User>("User")
        toList collection
        
    let filter (ctx:IMongoContext) (id) =
         let collection = ctx.Query<User>("User")
         
         let q = query {
             for u in collection do
             where (u.Id = id)
             select u
         }
         toList q

module Locality =
    let getAll (ctx: IMongoContext) =
        let collection = ctx.Query<Locality>("Locality")
        toList collection

module Student =
    let getAll (ctx: IMongoContext) =
        let collection = ctx.Query<Student>("Student")
        collection
            .Take(50)
            .ToList()
    let getOne (ctx: IMongoContext) (id:Guid) =
         let collection = ctx.Query<Student>("Student")
         collection |> Seq.tryFind (fun x -> x.Id = id)

    let search (ctx: IMongoContext) (text:string) =
        let collection = ctx.Query<Student>("Student")
        collection |> Seq.filter (fun x -> x.Name.Contains(text) || x.Number = text || x.Registration = text) |> Seq.toList
         
    let filter (ctx: IMongoContext) (filter:string) =
        let search = filter.ToUpper()
        let collection = ctx.Query<Student>("Student")
        let q = query {
            for student in collection do
            where (student.Name.Contains(search) || student.Number = search || student.Registration = search )
            select student
            take 50
        }
        toList q
        
    let registrations (ctx: IMongoContext) studentId =
        let collection = ctx.Query<Registration>("Registration")
        collection |> Seq.filter (fun x -> x.StudentId = studentId)