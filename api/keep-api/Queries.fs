module KeepApi.Queries

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
    
    let filter (ctx: IMongoContext) (search:string) =
        let collection = ctx.Query<Student>("Student")
        let q = query {
            for p in collection do
            where (p.Name.Contains(search))
            select p
            take 50
        }
        toList q