module KeepApi.Queries

open System
open KeepApi.Domain
open KeepApi.DataAccess
open System.Linq
open KeepApi.Effects
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
    type StudentFilters =  {Filter: string; Order:string }
    
    let getOne (ctx: IMongoContext) (id:Guid) =
         let collection = ctx.Query<Student>("Student")
         collection |> Seq.tryFind (fun x -> x.Id = id)
    
    let sortMe (order:string) (query:IQueryable<Student>)  =
        if order = "latest" then query.OrderByDescending(fun x -> x.CreationDate)
        else query.OrderBy(fun x -> x.Name)
    
    let getAll (ctx: IMongoContext) (order:string) =
        let collection = ctx.Query<Student>("Student")
        collection.Take(500) |> sortMe order |> Seq.toList
    
    let filter (ctx: IMongoContext) (filters:StudentFilters) =
        if String.IsNullOrEmpty(filters.Filter) then getAll ctx filters.Order
        else
            let search = filters.Filter.ToUpper()
            let collection = ctx.Query<Student>("Student")
            let q = query {
                for student in collection do
                where (student.Name.Contains(search) || student.Number = search || student.Registration = search )
                select student
                take 50
            }
            q |> sortMe filters.Order |> Seq.toList
        
    let search (ctx: IMongoContext) (text:string) =
        let collection = ctx.Query<Student>("Student")
        collection
        |> Seq.filter (fun x -> x.Name.Contains(text) || x.Number = text || x.Registration = text)
        |> Seq.toList
        
    let registrations (ctx: IMongoContext) studentId =
        let collection = ctx.Query<Registration>("Registration")
        collection |> Seq.filter (fun x -> x.StudentId = studentId)

module Delay =

  let getAll (ctx: IMongoContext) =
        let collection = ctx.Query<Delay>("Delay")
        toList collection