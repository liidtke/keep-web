export enum AsideType {
  Student,
  Course,
  Locality
}

export interface ILocality {
  id:number;
  name:string;
  number:string;
  address:string;
  zipCode:string;
  box:string;
  city:string;
  state:string;
  address2:string;
}