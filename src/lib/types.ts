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

export interface IPerson {
  Id:string;
  Name:string;
  Number:string;
  Registration:string;
  Radius:string;
  Cell:string;
  Pav:string;
  Xad:string;
  Referer:string;
  Observations:string;
  Locality?:ILocality
  LocalityId:string;
  AdmissionDate:Date;
  CreationDate:Date;
  CreatedBy?:string;

}