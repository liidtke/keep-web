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

    Response.withStatusCode 400
    >> Response.ofPlainText message

module Home =
    let handler =
        get "/" (Response.ofPlainText "Hello world")

module User =
    module Create =
        let workflow =
            let user: User =
                { Id = Guid.Empty
                  Name = "Teste34441"
                  Email = "A@aa.com" }
            //todo get user from json
            Service.run User.create user

        let handler = post "/users" (workflow)

    module Get =
        let workflow (ctx: HttpContext) =
            let data = Queries.User.getAll (getContext ctx)
            (data |> Response.ofJson) ctx

        let handler = get "/users" (workflow)

module Student =
    module Get =
        let allWorkflow (ctx: HttpContext) =
            (Queries.Student.getAll (getContext ctx)
             |> Response.ofJson)
                ctx

        let handler = get "/students" allWorkflow

    module GetOne =
        let workflow id (ctx: HttpContext) =
            Response.ofJson (Queries.Student.getOne <| getContext ctx <| id) ctx

        let paramsHandler: HttpHandler =
            let routeMap (route: RouteCollectionReader) =
                let id = route.GetGuid "id" Guid.Empty
                id

            Request.mapRoute routeMap workflow

        let handler = get "/students/{id:Guid}" paramsHandler

    module Create =
        let workflow (student: Student) =
            Service.run Student.create student

        let jsonHandler: HttpHandler =
            let handleOk (person: Student) : HttpHandler = workflow person

            Request.bindJson handleOk handleError

        let handler =
            all "/students" [ POST, jsonHandler; PUT, jsonHandler ]

module Registration =
    let routeMap (route: RouteCollectionReader) =
        let id = route.GetGuid "id" Guid.Empty
        id

    module Create =
        let workflow (reg: Registration) =
            Service.run Registration.create reg

        let handle: HttpHandler =
            let handleOk id (reg: Registration) : HttpHandler = workflow reg

            let handleJson id =
                Request.bindJson (handleOk id) handleError

            Request.mapRoute routeMap handleJson

        let handler =
            all "/students/{id:Guid}/registrations" [ POST, handle; PUT, handle ]

    module Get =
        let workflow id (ctx: HttpContext) =
            Response.ofJson
                (Queries.Student.registrations
                 <| getContext ctx
                 <| id)
                ctx

        let handle: HttpHandler = Request.mapRoute routeMap workflow

        let handler =
            get "/students/{id:Guid}/registrations" handle

    module Delete =
        let workflow id =
            Service.run Registration.delete id

        let idMap (route: RouteCollectionReader) =
            route.GetGuid "nId" Guid.Empty
        
        let handle: HttpHandler = Request.mapRoute idMap workflow

        let handler =
            delete "/students/{id:Guid}/registrations/{nId:Guid}" handle

module Locality =
    module Get =
        let workflow (ctx: HttpContext) =
            (Queries.Locality.getAll (getContext ctx)
             |> Response.ofJson)
                ctx

        let handler = get "/localities" workflow

    module Create =
        let workflow (locality: Locality) =
            Service.run Locality.create locality

        let jsonHandler: HttpHandler = Request.bindJson workflow handleError

        let handler =
            all "/localities" [ POST, jsonHandler; PUT, jsonHandler ]

module Course =
    module Get =
        let workflow (ctx: HttpContext) =
            (getContext ctx
             |> Queries.Course.getAll
             |> Response.ofJson)
                ctx

        let handler = get "/courses" workflow

    module Create =
        let workflow (course: Course) =
            Service.run Course.create course

        let jsonHandler: HttpHandler = Request.bindJson workflow handleError

        let handler = all "/courses" [POST, jsonHandler; PUT, jsonHandler]

//module Create =
//    let workflow (ctx: HttpContext) =
//        let result = Effects.Person.create ()
//        (Response.ofPlainText "ok") ctx
//
//    let handler = post "/test-postgres" (workflow)

let all =
    [ Home.handler
      User.Create.handler
      User.Get.handler
      Student.Create.handler
      Student.Get.handler
      Student.GetOne.handler
      Locality.Create.handler
      Locality.Get.handler
      Course.Get.handler
      Course.Create.handler
      Registration.Get.handler
      Registration.Create.handler
      Registration.Delete.handler ]
