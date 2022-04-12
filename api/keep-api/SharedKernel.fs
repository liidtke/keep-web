module KeepApi.SharedKernel


open System

type ErrorType = NotFound | Validation | InvalidInput | InternalError

type ErrorResult = 
  { ErrorType: ErrorType; ErrorMessages: List<string> }
  member this.ErrorMessage = String.concat "\n" this.ErrorMessages  

type ServiceOutput<'TSuccess> = 
  | Success of 'TSuccess
  | Failure of ErrorResult

type EffectType = ToSave | ToUpdate | ToRemove
type ServiceEffect<'a> = { HasEffect: bool; EffectType: EffectType; Target: 'a }

let succeed a = Success a 

let fail a = Failure { ErrorType = ErrorType.InternalError; ErrorMessages = []}
let failWith err = Failure err
let validate a = Failure { ErrorType = ErrorType.InvalidInput; ErrorMessages = [a]}

let fromError errorType errorMessage  = 
   Failure { ErrorType = errorType; ErrorMessages = [errorMessage]}
  
let fromException (ex:Exception) =
   Failure { ErrorType = InternalError; ErrorMessages = [ex.Message]}

let validation message = fromError Validation message 
let notFound message = fromError NotFound message

let addMessage (message:string) (serviceOutput:ServiceOutput<'a>) = 
  match serviceOutput with 
  | Success res -> Failure { ErrorType = ErrorType.Validation; ErrorMessages = [message]}
  | Failure e -> Failure { ErrorType = e.ErrorType; ErrorMessages = e.ErrorMessages @ [message]}

//add one service output to another
let join (s1:ServiceOutput<'a>) (s2:ServiceOutput<'a>) =
  match s1, s2 with 
  | Success service1, Success service2 -> s2
  | Success service1, Failure e2 -> s2
  | Failure e1, Success service2 -> s1
  | Failure e1, Failure e2 -> Failure { ErrorType = e2.ErrorType; ErrorMessages = e1.ErrorMessages @ e2.ErrorMessages}


// apply either a success function or failure function
let successfully successFunc input =
  match input with
  | Success s -> successFunc s
  | Failure e -> failWith e

//my bind version
let may f =
  successfully f

//adapted from recipe
//https://fsharpforfunandprofit.com/posts/recipe-part3/

// apply either a success function or failure function
let either successFunc failureFunc input =
  match input with
  | Success s -> successFunc s
  | Failure e -> failureFunc e

// convert a switch function into a two-track function
let bind f =
  either f fail

// pipe a two-track value into a switch function
let (>>=) x f =
  bind f x

// compose two switches into another switch
let (>=>) s1 s2 =
  s1 >> bind s2

// convert a one-track function into a switch
let switch f =
  f >> succeed

// convert a one-track function into a two-track function
let map f =
   either (f >> succeed) fail

let tee f x =
  f x; x

// convert a one-track function into a switch with exception handling
let tryCatch f exnHandler x =
  try
    f x |> succeed
  with
    | ex -> exnHandler ex |> fail

// convert two one-track functions into a two-track function
let doubleMap successFunc failureFunc =
  either (successFunc >> succeed) (failureFunc >> fail)
  

//fsharp helpers
let isNullOrEmpty str =
  String.IsNullOrEmpty(str)
  
let parseGuid (str:string) =
  try Some (Guid.Parse str)
  with
  | ex -> None
