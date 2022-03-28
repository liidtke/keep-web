module KeepApi.Domain

open System

type Id = Guid
type Number = int32

[<Interface>]
type IEntity =
    abstract Id : Guid

[<CLIMutable>]
type Locality =
    { Id: Id
      Name: string
      Address: string
      City: string
      State: string
      Number: string option
      ZipCode: string option
      Box: string option
      Address2: string option }

[<CLIMutable>]
type User =
    { Id: Guid
      Name: string
      Email: string }
    interface IEntity with
        member this.Id = this.Id

[<CLIMutable>]
type Student =
    { Id: Guid
      Name: string
      Number: string
      Registration: string
      LocalityId: Guid
      Radius: string option
      Cell: string option
      Pav: string option
      Xad: string option
      Referer: string option
      AdmissionDate: DateTime
      CreationDate: DateTime
      CreatedBy: string
      Observation: string option
      }

[<CLIMutable>]
type Course = {
    Id: Guid
    Name:string
    Lessons:Number
    IsActive:bool
}

type Progress = {
    LessonNumbers: int32 list
    Sent: DateTime
    Returned: DateTime option
    Comments: string option
}

type Registration = {
    Course: Course
    Progress: Progress
    IsCompleted: bool
    StartDate: DateTime
}

[<CLIMutable>]
type StudentProgress = {
    Id: Guid
    Registrations: Registration list
}

[<CLIMutable>]
type Postage = {
    Id:Guid
    CreatedBy:string
    Occurred:DateTime
    Total:Number
    Cost:decimal
}

[<CLIMutable>]
type Letter = {
    Id:Guid
    Name:string
    Text:string
}