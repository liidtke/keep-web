import { Result } from '$lib/result'

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

  getAllLocalities() {
    return [{
      id: 1,
      name: "Teste 01",
      address: "Rua Teste 001",
      number: "123",
      zipCode: "13184-233",
      box: null,
      city: "Hortolândia",
      state: "SP",
    },
    {
      id: 2,
      name: "Hortolândia II",
      address: "Rua Teste 003",
      zipCode: "0301123-122",
      box: null,
      city: "Hortolândia",
      state: "SP",
    }
    ];
  }


}