module KeepApi.Output

open Falco
open SharedKernel

let errorStatusCode (serviceError:ErrorResult) =
  match serviceError.ErrorType with
  | NotFound -> Response.withStatusCode 404
  | Validation -> Response.withStatusCode 409
  | InvalidInput -> Response.withStatusCode 400
  | InternalError -> Response.withStatusCode 500
  | Forbidden -> Response.withStatusCode 403

let errorHandler (serviceError:ErrorResult) : HttpHandler =
   errorStatusCode serviceError >> Response.ofPlainText serviceError.ErrorMessage
   
let from serviceOutput =
   match serviceOutput with 
   | Success result -> result |> Response.ofJson
   | Failure e -> (errorHandler e)


let fromModel toModel serviceOutput =
   match serviceOutput with 
   | Success result -> result |> toModel |> Response.ofJson
   | Failure e -> errorHandler e
   
let badRequest text =
   Response.withStatusCode 400 >> Response.ofPlainText text