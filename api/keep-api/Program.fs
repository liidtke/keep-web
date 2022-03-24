module KeepApi.Program

open Falco
open Falco.Routing
open Falco.HostBuilder

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Builder
open DataAccess

// ------------
// Exception Handler
// ------------
let exceptionHandler: HttpHandler =
    Response.withStatusCode 500
    >> Response.ofPlainText "Server error"

let configureServices (context: IMongoContext) (services: IServiceCollection) =
    services
        .AddSingleton<IMongoContext>(context)
        .AddCors()
        .AddFalco()
    |> ignore

let configureCors (corsBuilder: CorsPolicyBuilder) : unit =
    corsBuilder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
    |> ignore

let configureApp: HttpEndpoint list -> IApplicationBuilder -> unit =
    fun endpoints app ->
        app.UseCors(fun options -> configureCors options)
        |> ignore

        app.UseFalco(endpoints) |> ignore

let configureWebHost (endpoints: HttpEndpoint list) (webHost: IWebHostBuilder) =
    let appConfiguration: IConfiguration =

        ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
        :> IConfiguration

    let section =
        appConfiguration.GetSection("DatabaseSettings")

    let settings =
        { ConnectionString = section.GetValue("ConnectionString")
          DatabaseName = section.GetValue("DatabaseName") }

    let mongoContext = MongoContext(settings) :> IMongoContext

    webHost
        .UseConfiguration(appConfiguration)
        .ConfigureServices(configureServices mongoContext)
        .Configure(configureApp endpoints)

[<EntryPoint>]
let main args =
    webHost args {
        use_if FalcoExtensions.IsDevelopment DeveloperExceptionPageExtensions.UseDeveloperExceptionPage
        use_ifnot FalcoExtensions.IsDevelopment (FalcoExtensions.UseFalcoExceptionHandler exceptionHandler)
        configure configureWebHost

        endpoints Endpoints.all
    }

    0
