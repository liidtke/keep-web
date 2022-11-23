export enum AsideType {
  Student,
  Course,
  Locality,
  Questions
}

export interface ILocality {
  Id:string;
  Name:string;
  Number:string;
  Address:string;
  ZipCode:string;
  Box:string;
  City:string;
  State:string;
  Address2:string;
}

export interface IStudent {
  Id:string;
  Name:string;
  Number:string;
  Registration:string;
  Radius:string;
  Cell:string;
  Pav:string;
  Xad:string;
  Referer:string;
  Observation:string;
  Locality?:ILocality
  LocalityId:any;
  AdmissionDate:Date;
  CreationDate:Date;
  CreatedBy?:string;
  Answers?:IAnswer[];
}

export interface ICourse {
    Id: string,
    Name:string,
    Lessons:number,
    IsActive:boolean,
}

export interface IProgress {
  Lessons: number[];
  Sent: Date;
  Returned?: Date;
  Comments?: string;
  _edit?:boolean;
}

export interface IRegistration{
  StudentId:string
  Course:ICourse;
  Progress:IProgress[];
  IsCompleted:boolean;
  StartDate:Date;
  Id?:string
}

export interface IAnswer{
  Question:IQuestion;
  Text:string;
}

export interface IQuestion{
  Id?:string;
  Text:string;
}

export interface IUser{
  Id?: string;
  Name: string;
  Email: string;
  Password: string;
  IsVerified: boolean;
  _edit?:boolean;
}

export interface IDelay{
  Id?: string;
  StudentId:string;
  StudentName:string;
  CourseName:string;

}
