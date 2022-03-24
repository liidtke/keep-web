module KeepApi.Endpoints

open Falco
open Falco.Routing
open Microsoft.AspNetCore.Http
open DataAccess
open UseCases
open System
open Domain
open SharedKernel

let getContext (ctx: HttpContext) = ctx.GetService<IMongoContext>()

let handleError error : HttpHandler = 
            let message = sprintf "Invalid JSON: %s" error
            Response.withStatusCode 400 >> Response.ofPlainText message

module Home =
    let handler = get "/" (Response.ofPlainText "Hello world")

module Test =
    let handler = get "/test" (validation "testingtdtdtdt" |> Output.from)

module User =
  module Create =
      let workflow (ctx: HttpContext)  =
        let user:User = { Id = Guid.Empty; Name = "Teste34441"; Email = "A@aa.com"; }
        Output.from (User.create <| getContext ctx <| user) ctx
      let handler = post "/users" (workflow)
      
  module Get =
        let workflow (ctx: HttpContext) =
            let data = Queries.User.getAll (getContext ctx) 
            (data |> Response.ofJson) ctx
        let handler = get "/users" (workflow)

module Person =
  module Create =
      let workflow (person:Person) (ctx: HttpContext)  =
        let data = Queries.User.getAll (getContext ctx)
        (data |> Response.ofJson) ctx
        
      let jsonHandler : HttpHandler =
        let handleOk (person:Person) : HttpHandler =
            let message = sprintf "hello %s" person.Name
            workflow person
            
        Request.bindJson handleOk handleError
      
      let handler = post "/person" (jsonHandler)

module Locality =
  module Get =
      let workflow (ctx:HttpContext) =
          (Queries.Locality.getAll (getContext ctx) |> Response.ofJson) ctx
          
      let handler = get "/localities" workflow
          
  module Create =
      let workflow (locality:Locality) (ctx: HttpContext) =
          Output.from (Locality.create <| getContext ctx <| locality) ctx
      let jsonHandler : HttpHandler =
          Request.bindJson workflow handleError
          
      let handler = post  "/localities" jsonHandler
      let handlerPut = put  "/localities" jsonHandler
      

module Course =
    module Get =
        let workflow (ctx:HttpContext) =
            (getContext ctx |> Queries.Course.getAll |> Response.ofJson) ctx
        let handler = get "/courses" workflow

    module Create =
        let workflow (course:Course) (ctx: HttpContext) =
            Output.from (Course.create <| getContext ctx <| course) ctx
        let jsonHandler : HttpHandler =
            Request.bindJson workflow handleError
            
        let handler = post "/courses" jsonHandler
        let handlerPut = put "/courses" jsonHandler
            
//module Create =
//    let workflow (ctx: HttpContext) =
//        let result = Effects.Person.create ()
//        (Response.ofPlainText "ok") ctx
//        
//    let handler = post "/test-postgres" (workflow)

let all = [ Home.handler
            Test.handler
            User.Create.handler
            User.Get.handler
            Person.Create.handler
            Locality.Create.handler
            Locality.Create.handlerPut
            Locality.Get.handler
            Course.Get.handler
            Course.Create.handler
            Course.Create.handlerPut
            ]
