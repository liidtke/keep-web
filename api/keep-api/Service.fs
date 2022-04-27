module KeepApi.Service

open Falco.Extensions
open Falco.Core
open KeepApi.DataAccess
open KeepApi.SharedKernel
open Microsoft.AspNetCore.Http

type ServiceHandler<'input, 'output> = 'input -> ServiceOutput<'output>

let run
    (serviceHandler: IMongoContext -> ServiceHandler<'input, 'output>)
    (input: 'input) : HttpHandler =
    fun (ctx:HttpContext) ->
        let context = ctx.GetService<IMongoContext>()
        Output.from (serviceHandler <| context <| input) ctx