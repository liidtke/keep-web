export enum AsideType {
  Student,
  Course,
  Locality
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
  LocalityId:string;
  AdmissionDate:any;
  CreationDate:any;
  CreatedBy?:string;
}

export interface ICourse {
    Id: string,
    Name:string,
    Lessons:number,
    IsActive:boolean,
}