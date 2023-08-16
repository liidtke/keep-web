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
    let message =
        sprintf "Invalid JSON: %s" error

    Response.withStatusCode 400
    >> Response.ofPlainText message

let handleInvalidAuth: HttpHandler =
    Response.withStatusCode 403
    >> Response.ofPlainText "Forbidden"

let secureHandler (ok: HttpHandler) : HttpHandler =
    Request.ifAuthenticated ok handleInvalidAuth

let verySecureHandler (ok: HttpHandler) : HttpHandler =
    Request.ifAuthenticatedInRole [ "User" ] ok handleInvalidAuth

module Home =
    let handler =
        get "/" (Response.ofPlainText "WELCOME HOME")

module User =
    module Create =
        let workflow (user: User) (ctx: HttpContext) =

            let context =
                ctx.GetService<IMongoContext>()

            let settings =
                ctx.GetService<Security.SecuritySettings>()

            Output.from
                (User.create
                 <| context
                 <| Security.hashPassword settings
                 <| user)
                ctx

        let jsonHandler: HttpHandler =
            Request.bindJson workflow handleError

        let handler = post "/users" (jsonHandler)

    module Update =
        let workflow user = Service.run UseCases.User.update user

        let jsonHandler: HttpHandler =
            verySecureHandler
            <| Request.bindJson workflow handleError

        let handler =
            put "/users/{id:Guid}" jsonHandler

    module Get =
        let workflow (ctx: HttpContext) =
            let data =
                Queries.User.getAll (getContext ctx)

            (data |> Response.ofJson) ctx

        let handler =
            get "/users" (verySecureHandler workflow)

module Student =
    open Queries.Student

    module Get =

        let workflow (filters: StudentFilters) (ctx: HttpContext) =
            (filter <| getContext ctx <| filters
             |> Response.ofJson)
                ctx

        let paramsHandler: HttpHandler =
            let queryMap (query: QueryCollectionReader) =
                let filter = query.GetString "filter" ""
                let order = query.GetString "order" ""
                let loc = query.GetGuid "localityId" Guid.Empty
                { Filter = filter; Order = order; LocalityId = loc }

            Request.mapQuery queryMap workflow

        let handler =
            get "/students" (verySecureHandler paramsHandler)

    module GetOne =
        let workflow id (ctx: HttpContext) =
            Response.ofJson (Queries.Student.getOne <| getContext ctx <| id) ctx

        let paramsHandler: HttpHandler =
            let routeMap (route: RouteCollectionReader) =
                let id = route.GetGuid "id" Guid.Empty
                id

            Request.mapRoute routeMap workflow

        let handler =
            get "/students/{id:Guid}" (verySecureHandler paramsHandler)

    module Create =
        let workflow (student: Student) = Service.run Student.create student

        let jsonHandler: HttpHandler =
            let handleOk (person: Student) : HttpHandler = workflow person

            Request.bindJson handleOk handleError

        let handler =
            all
                "/students"
                [ POST, verySecureHandler jsonHandler
                  PUT, verySecureHandler jsonHandler ]

    module Delete =
        let workflow (id: Guid) = Service.run Student.delete id

        let paramsHandler: HttpHandler =
            let routeMap (route: RouteCollectionReader) =
                let id = route.GetGuid "id" Guid.Empty
                id

            Request.mapRoute routeMap workflow

        let handler =
            delete "/students/{id:Guid}" (verySecureHandler paramsHandler)


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

            verySecureHandler
            <| Request.mapRoute routeMap handleJson

        let handler =
            all "/students/{id:Guid}/registrations" [ POST, handle; PUT, handle ]

    module Get =
        let workflow id (ctx: HttpContext) =
            Response.ofJson
                (Queries.Student.registrations
                 <| getContext ctx
                 <| id)
                ctx

        let handle: HttpHandler =
            Request.mapRoute routeMap workflow

        let handler =
            get "/students/{id:Guid}/registrations" (verySecureHandler handle)

    module Delete =
        let workflow id = Service.run Registration.delete id

        let idMap (route: RouteCollectionReader) = route.GetGuid "nId" Guid.Empty

        let handle: HttpHandler =
            Request.mapRoute idMap workflow

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

        let jsonHandler: HttpHandler =
            secureHandler
            <| Request.bindJson workflow handleError

        let handler =
            all "/localities" [ POST, jsonHandler; PUT, jsonHandler ]

module Course =
    module Get =
        let workflow (ctx: HttpContext) =
            (getContext ctx
             |> Queries.Course.getAll
             |> Response.ofJson)
                ctx

        let handler =
            get "/courses" (secureHandler workflow)

    module Create =
        let workflow (course: Course) = Service.run Course.create course

        let jsonHandler: HttpHandler =
            Request.bindJson workflow handleError

        let handler =
            all
                "/courses"
                [ POST, verySecureHandler jsonHandler
                  PUT, verySecureHandler jsonHandler ]

module Question =
    module Get =
        let workflow (ctx: HttpContext) =
            (Queries.Question.getAll (getContext ctx)
             |> Response.ofJson)
                ctx

        let handler =
            get "/questions" (secureHandler workflow)

    module Create =
        let workflow (question: Question) = Service.run Question.create question

        let jsonHandler: HttpHandler =
            Request.bindJson workflow handleError

        let handler =
            all
                "/questions"
                [ POST, verySecureHandler jsonHandler
                  PUT, verySecureHandler jsonHandler ]


module Delay =
    module Get =
        let getAll (ctx:HttpContext) = (Queries.Delay.getAll <| getContext ctx |> Response.ofJson) ctx
        let handler = get "/delays" (secureHandler getAll)

    let handler = post "/delays" (Service.run Delay.handle ())

module Login =
    open Security

    let workflow (loginRequest: LoginRequest) (ctx: HttpContext) =
        let context =
            ctx.GetService<IMongoContext>()

        let settings =
            ctx.GetService<SecuritySettings>()

        Output.from (login context settings loginRequest) ctx

    let jsonHandler: HttpHandler =
        Request.bindJson workflow handleError

    let handler = post "/login" jsonHandler


let all =
    [ Home.handler
      User.Create.handler
      User.Get.handler
      User.Update.handler
      Student.Create.handler
      Student.Get.handler
      Student.Delete.handler
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
      Delay.Get.handler
      Delay.handler
      Login.handler ]
