module KeepApi.UseCases

open System.Net.Mail
open Domain
open KeepApi.Domain
open SharedKernel
open System


module Course =
    let validateCourse (course: Course) =
        let validateName =
            if isNullOrEmpty course.Name then
                validation "Nome Inválido"
            else
                succeed course

        let validateNumber =
            if course.Lessons < 0 then
                validation "Número Inválido"
            else
                succeed course

        validateName |> join validateNumber

    let create db =
        let workflow saveEffect = validateCourse >> may saveEffect

        workflow <| Effects.Course.save db

module Student =
    let checkOptions (student: Student) =
        let radius =
            if Object.ReferenceEquals(student.Radius, null) then
                { student with Radius = option.None }
            else
                student

        radius

    let validateStudent (student: Student) =
        let validateName =
            if student.Name = String.Empty then
                validation "Nome Inválido"
            else
                succeed student

        let validateLocality =
            if student.LocalityId = Guid.Empty then
                validation "Localidade Inválida"
            else
                succeed student

        let validateRegistration =
            if isNullOrEmpty student.Registration then
                validation "Matrícula inválida"
            else
                succeed student

        let validateNumber =
            if isNullOrEmpty student.Number then
                validation "Número inválido"
            else
                succeed student

        validateName
        |> join validateLocality
        |> join validateRegistration
        |> join validateNumber

    let locationExists (effect: Student -> bool) (student: Student) =
        if effect student then
            succeed student
        else
            validation "Localidade não encontrada"

    let prepare (student: Student) =
        { student with
              CreatedBy = "system"
              CreationDate = DateTime.UtcNow }

    let create db =
        let workflow locationEffect saveEffect =
            prepare
            >> validateStudent
            >> may (locationExists locationEffect)
            >> may saveEffect

        workflow
        <| Effects.Student.getLocality db
        <| Effects.Student.save db

module User =

    let validateUser (user: User) =
        let validateName =
            if user.Name = "" then
                validation "Nome Inválido"
            else
                succeed user

        let validateEmail =
            try
                MailAddress(user.Email) |> ignore
                succeed user
            with
            | _ -> validation "Email Inválido"

        let validatePassword =
            if user.Id = Guid.Empty && (user.Password = "" || user.Password.Length < 6) then
                validation "Senha Inválida"
            else
                succeed user

        validateName
        |> join validateEmail
        |> join validatePassword

    let validateCreation (user: User) =
        if user.Id = Guid.Empty then
            succeed user
        else
            validation "Id"

    let validateDuplicates (effect: User -> int64) (user: User) =
        let max = if user.Id = Guid.Empty then 0 else 1

        if effect user > max then
            validation "Usuário já existe"
        else
            succeed user

    let validateDuplicatesChange (effect: User -> int64) (emailChanged: bool) (user: User) =
        let max = if emailChanged then 0 else 1

        if effect user > max then
            validation "Usuário já existe"
        else
            succeed user

    let passwordToHash (hashEffect: string -> string) (user: User) =
        succeed
            { user with
                  Password = hashEffect user.Password }

    let create db =
        let workflow saveEffect valEffect hashEffect =
            validateUser
            >> may validateCreation
            >> may (validateDuplicates valEffect)
            >> may (passwordToHash hashEffect)
            >> may saveEffect

        workflow
        <| Effects.User.save db
        <| Effects.User.countDuplicates db

    let updateUser original modified =
        { original with
              Email = modified.Email
              Name = modified.Name
              IsVerified = modified.IsVerified }

    let update db (user: User) =
        let workflow originalEffect saveEffect valEffect =
            let output =
                user |> validateUser |> join
                <| originalEffect user

            match output with
            | Failure _ -> output
            | Success original ->
                updateUser original user
                |> validateDuplicatesChange valEffect (original.Email <> user.Email)
                |> may saveEffect

        workflow
        <| Effects.User.getById db
        <| Effects.User.save db
        <| Effects.User.countDuplicates db


module Locality =
    let validateLocality (locality: Locality) =
        let validateName =
            if locality.Name = String.Empty then
                validation "Nome Inválido"
            else
                succeed locality

        let validateAddress =
            if locality.Address = String.Empty then
                validation "Endereço Inválido"
            else
                succeed locality

        let validateCity =
            if locality.City = null || locality.City = "" then
                validation "Cidade Inválida"
            else
                succeed locality

        let validateState =
            if locality.State = null || locality.State = "" then
                validation "Estado Inválido"
            else
                succeed locality

        validateCity
        |> join validateName
        |> join validateAddress
        |> join validateState

    let create db =
        let workflow saveEffect = validateLocality >> may saveEffect

        workflow <| Effects.Locality.save db

module Registration =
    let validate (reg: Registration) =
        let validateStudent =
            if reg.StudentId = Guid.Empty then
                validation "Id inválido"
            else
                succeed reg

        let validateCourse =
            if reg.Course.Id = Guid.Empty || reg.Course.Name = "" then
                validation "Curso Inválido"
            else
                succeed reg

        //        let validateRegs =
//            courses.Registrations
//            |> List.map vRegCourse
//            |> List.fold (fun acc elem -> join acc elem) (succeed courses)

        validateStudent |> join validateCourse

    let create db =
        let workflow saveEffect = validate >> may saveEffect
        workflow <| Effects.Registration.save db

    let delete db = Effects.Registration.delete db

module Question =
    let validate (q: Question) =
        if q.Text = String.Empty || q.Text.Length > 500 then
            validation "Texto inválido"
        else
            succeed q

    let create db =
        let workflow saveEffect = validate >> may saveEffect
        workflow <| Effects.Question.save db
