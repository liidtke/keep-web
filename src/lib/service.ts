import { Result } from '$lib/result'
import { localities } from './store';

export class Service {

  private api: string;
  private headers: any;
  private token: any;

  constructor() {

    this.api = "http://localhost:5000/"
    this.token = "";
    this.headers = {
      'Content-Type': 'application/json',
      // 'Authorization': `bearer ${this.token}`
    }
  };


  async getStudents() {
    return [{
      Id: "123",
      Name: "Carlos Silva",
      Number: "123",
      Registration: "AFG-32",
      Radius: null,
      Cell: "012",
      Pav: "12",
      Xad: "001",
      Referer: "A",
      Observations: "",
      Locality: null,
      LocalityId: null,
      AdmissionDate: Date,
      CreationDate: Date,
      CreatedBy: "Aldo"
    },
    {
      Id: "123",
      Name: "Felipe Nogueira",
      Number: "4511",
      Registration: "AFG-2311",
      Radius: null,
      Cell: "012",
      Pav: "12",
      Xad: "001",
      Referer: "A",
      Observations: "Criado agora a pouco",
      Locality: null,
      LocalityId: null,
      AdmissionDate: Date,
      CreationDate: Date,
      CreatedBy: "Aldo"
    }
  ]
  }

  async saveStudent() {
    return Result.Succeed();
  }

  async getLocalities() {
    let request = await fetch(this.api + `localities`, {
      headers: this.headers,
    });
    let response = await request.json();
    return response;
  }

  async saveLocality(locality) {
    let res: Response;

    try {
      console.log('trying', locality)
      if (locality.id) {
        console.log('PUT')
        res = await fetch(this.api + 'localities', {
          method: 'PUT',
          body: JSON.stringify(locality),
          headers: this.headers,
        });
      }
      else {
        console.log('posting', this.api)
        res = await fetch(this.api + 'localities', {
          method: 'POST',
          headers: this.headers,
          body: JSON.stringify(locality)
        })
      }
    }
    catch (e) {
      console.log(e);
      return Result.Error("Não foi possível realizar a operação")
    }

    if (res.ok) {
      let data = await res.json();
      return Result.Ok(data);
    }
    else {
      let message = await res.text();
      return Result.Error(message);
    }
  }

}