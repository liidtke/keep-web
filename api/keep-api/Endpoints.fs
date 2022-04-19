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

let handleInvalidAuth : HttpHandler =
        Response.withStatusCode 403 
        >> Response.ofPlainText "Forbidden"

let secureHandler (ok: HttpHandler) : HttpHandler  = Request.ifAuthenticated ok handleInvalidAuth
let verySecureHandler (ok: HttpHandler) : HttpHandler  = Request.ifAuthenticatedInRole ["User"] ok handleInvalidAuth

module Home =
    let handler =
        get "/" (Response.ofPlainText "WELCOME HOME")

module User =
    module Create =
        let workflow (user: User) (ctx: HttpContext) =

            let context = ctx.GetService<IMongoContext>()

            let settings =
                ctx.GetService<Security.SecuritySettings>()

            Output.from
                (User.create
                 <| context
                 <| Security.hashPassword settings
                 <| user)
                ctx

        let jsonHandler: HttpHandler = Request.bindJson workflow handleError

        let handler = post "/users" (jsonHandler)

    module Get =
        let workflow (ctx: HttpContext) =
            let data = Queries.User.getAll (getContext ctx)
            (data |> Response.ofJson) ctx

        let handler = get "/users" (verySecureHandler workflow)

module Student =
    module Get =
        let allWorkflow (ctx: HttpContext) =
            (Queries.Student.getAll <| getContext ctx
             |> Response.ofJson)
                ctx

        let someWorkflow (search:string) (ctx: HttpContext)  =
            (Queries.Student.filter <| getContext ctx <| search |> Response.ofJson) ctx
        
        let workflow (search:string) (ctx: HttpContext) =
            if search = String.Empty then allWorkflow ctx
            else someWorkflow search ctx
        
        let paramsHandler : HttpHandler =
            let queryMap (query: QueryCollectionReader) =
                query.GetString "filter" ""
            
            Request.mapQuery queryMap workflow
        
        let handler = get "/students" (verySecureHandler allWorkflow)

    module GetOne =
        let workflow id (ctx: HttpContext) =
            Response.ofJson (Queries.Student.getOne <| getContext ctx <| id) ctx

        let paramsHandler: HttpHandler =
            let routeMap (route: RouteCollectionReader) =
                let id = route.GetGuid "id" Guid.Empty
                id

            Request.mapRoute routeMap workflow

        let handler = get "/students/{id:Guid}" (verySecureHandler paramsHandler)

    module Create =
        let workflow (student: Student) = Service.run Student.create student

        let jsonHandler: HttpHandler =
            let handleOk (person: Student) : HttpHandler = workflow person

            Request.bindJson handleOk handleError

        let handler =
            all "/students" [ POST, verySecureHandler jsonHandler; PUT, verySecureHandler jsonHandler ]

module Registration =
    let routeMap (route: RouteCollectionReader) =
        let id = route.GetGuid "id" Guid.Empty
        id

    module Create =
        let workflow (reg: Registration) = Service.run Registration.create reg

        let handle: HttpHandler =
            let handleOk id (reg: Registration) : HttpHandler = workflow reg

            let handleJson id =
                Request.bindJson (handleOk id) handleError

            Request.mapRoute routeMap handleJson

        let handler =
            all "/students/{id:Guid}/registrations" [ POST, verySecureHandler handle; PUT, verySecureHandler handle ]

    module Get =
        let workflow id (ctx: HttpContext) =
            Response.ofJson
                (Queries.Student.registrations
                 <| getContext ctx
                 <| id)
                ctx

        let handle: HttpHandler = Request.mapRoute routeMap workflow

        let handler =
            get "/students/{id:Guid}/registrations" (verySecureHandler handle)

    module Delete =
        let workflow id = Service.run Registration.delete id

        let idMap (route: RouteCollectionReader) = route.GetGuid "nId" Guid.Empty

        let handle: HttpHandler = Request.mapRoute idMap workflow

        let handler =
            delete "/students/{id:Guid}/registrations/{nId:Guid}" (verySecureHandler handle)

module Locality =
    module Get =
        let workflow (ctx: HttpContext) =
            (Queries.Locality.getAll (getContext ctx)
             |> Response.ofJson)
                ctx

        let handler = get "/localities" workflow

    module Create =
        let workflow (locality: Locality) = Service.run Locality.create locality

        let jsonHandler: HttpHandler = Request.bindJson workflow handleError

        let handler =
            all "/localities" [ POST, secureHandler jsonHandler; PUT, secureHandler jsonHandler ]

module Course =
    module Get =
        let workflow (ctx: HttpContext) =
            (getContext ctx
             |> Queries.Course.getAll
             |> Response.ofJson)
                ctx

        let handler = get "/courses" (secureHandler workflow)

    module Create =
        let workflow (course: Course) = Service.run Course.create course

        let jsonHandler: HttpHandler = Request.bindJson workflow handleError
        
        let handler =
            all "/courses" [ POST, verySecureHandler jsonHandler; PUT, verySecureHandler jsonHandler ]

module Question =
    module Get =
        let workflow (ctx: HttpContext) =
            (Queries.Question.getAll (getContext ctx)
             |> Response.ofJson)
                ctx

        let handler = get "/questions" (secureHandler workflow)
        
    module Create =
        let workflow (question:Question) = Service.run Question.create question
        let jsonHandler: HttpHandler = Request.bindJson workflow handleError
        let handler = all "/questions" [ POST, verySecureHandler jsonHandler; PUT, verySecureHandler jsonHandler ]


module Login =
    open Security

    let workflow (loginRequest: LoginRequest) (ctx: HttpContext) =
        let context = ctx.GetService<IMongoContext>()
        let settings = ctx.GetService<SecuritySettings>()
        Output.from (login context settings loginRequest) ctx

    let jsonHandler: HttpHandler = Request.bindJson workflow handleError

    let handler = post "/login" jsonHandler


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
      Registration.Delete.handler
      Question.Get.handler
      Question.Create.handler
      Login.handler
      ]
