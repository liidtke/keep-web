import { Result } from '$lib/result'
import { localities, questions } from './store';
import type { ICourse, IQuestion, IRegistration, IStudent, IUser } from './types';
import { API_URL } from '$lib/Env';
import dateConverter from "$lib/date-converter";

export class Service {

  private api: string;
  private headers: any;
  private token: any;

  constructor(token) {
    this.api = API_URL as string;
    this.token = token;
    this.headers = {
      'Content-Type': 'application/json',
      'Authorization': `bearer ${this.token}`
    }
  };

  private async getLocalities() {
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
      if (locality.Id) {
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

  async getCourses(): Promise<ICourse[]> {
    let request = await fetch(this.api + `courses`, {
      headers: this.headers,
    });
    let response = await request.json();
    return response;
  }

  async saveCourse(course: ICourse) {
    let res: Response;
    let n = Number(course.Lessons);
    if (isNaN(n)) {
      return Result.Error("Valor Inválido");
    }
    course.Lessons = n;

    try {
      console.log('trying', course)
      if (course.Id) {
        console.log('PUT')
        res = await fetch(this.api + 'courses', {
          method: 'PUT',
          body: JSON.stringify(course),
          headers: this.headers,
        });
      }
      else {
        console.log('posting', this.api)
        res = await fetch(this.api + 'courses', {
          method: 'POST',
          headers: this.headers,
          body: JSON.stringify(course)
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
      console.log(message);
      return Result.Error(message);
    }
  }

  async getStudents(search = null, order = null) {
    let params = new URLSearchParams({filter:search ?? '', order:order ?? ''});

    let request = await fetch(this.api + `students?` + params, {
      headers: this.headers,
    });
    try {
      let students = await request.json();

      return students;
    }
    catch {
      return [];
    }
  }

  async getStudent(id) {
    let request = await fetch(this.api + `students/${id}`, {
      headers: this.headers,
    });
    try {
      let student = await request.json();

      return student;
    }
    catch {
      return null;
    }
  }

  async saveStudent(student: IStudent) {
    let val = this.validateStudent(student);
    if (val.isError) {
      return val;
    }

    let res: Response;

    try {
      console.log('trying', student)

      if (student.Id) {

        res = await fetch(this.api + 'students', {
          method: 'PUT',
          body: JSON.stringify(student),
          headers: this.headers,
        });
      }
      else {
        console.log('posting', this.api)
        res = await fetch(this.api + 'students', {
          method: 'POST',
          headers: this.headers,
          body: JSON.stringify(student)
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
      console.log(message);
      return Result.Error(message);
    }
  }

  validateStudent(student: IStudent) {
    if (!student.LocalityId) {
      return Result.Error("Localidade Obrigatória");
    }
    if (student.Name == null) {
      return Result.Error("Nome Obrigatório")
    }
    if (student.Number == null) {
      return Result.Error("Número Obrigatório")
    }
    if (student.Registration == null) {
      return Result.Error("Matrícula Obrigatória")
    }
    if (student.AdmissionDate == null) {
      return Result.Error("Data de Admissão Obrigatória")
    }

    return Result.Succeed();
  }

  async saveRegistration(registration: IRegistration) {

    if (!registration.StudentId) {
      return Result.Error("Aluno não encontrado")
    }

    let res: Response;

    try {
      console.log('trying', registration)
      let url = `${this.api}students/${registration.StudentId}/registrations`;

      if (registration.Id) {
        res = await fetch(url, {
          method: 'PUT',
          body: JSON.stringify(registration),
          headers: this.headers,
        });
      }
      else {
        res = await fetch(url, {
          method: 'POST',
          headers: this.headers,
          body: JSON.stringify(registration)
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
      console.log(message);
      return Result.Error(message);
    }
  }

  async deleteRegistration(registration) {
    let res: Response;

    try {
      let url = `${this.api}students/${registration.StudentId}/registrations/${registration.Id}`;
      res = await fetch(url, {
        method: 'DELETE',
        headers: this.headers,
      });
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
      console.log(message);
      return Result.Error(message);
    }
  }

  async getRegistrations(studentId: string) {
    let url = `${this.api}students/${studentId}/registrations`;
    let request = await fetch(url, {
      headers: this.headers,
    });
    try {
      let items = await request.json();

      return items;
    }
    catch {
      return [];
    }
  }

  private locLoaded: boolean = false;
  private locPromise: any;

  async loadLocalities(reload = false) {
    if (this.locPromise) {
      await this.locPromise;
    }

    if (!this.locLoaded || reload) {
      this.locPromise = this.getLocalities();
      let locs = await this.locPromise;
      localities.set(locs);
      this.locLoaded = true;
    }
  }

  private questionsLoaded: boolean = false;
  private questionPromise: any;

  async loadQuestions(reload = false) {
    if (this.questionPromise) {
      await this.questionPromise;
    }

    if (!this.questionPromise || reload) {
      this.questionPromise = this.getQuestions();
      let items = await this.questionPromise;
      questions.set(items);
      this.questionsLoaded = true;
    }
  }

  async getQuestions(): Promise<IQuestion[]> {
    let request = await fetch(this.api + `questions`, {
      headers: this.headers,
    });
    let response = await request.json();
    return response;
  }

  async saveQuestion(question: IQuestion) {
    let res: Response;

    try {
      let url = `${this.api}questions`;

      if (question.Id) {
        res = await fetch(url, {
          method: 'PUT',
          body: JSON.stringify(question),
          headers: this.headers,
        });
      }
      else {
        res = await fetch(url, {
          method: 'POST',
          headers: this.headers,
          body: JSON.stringify(question)
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
      console.log(message);
      return Result.Error(message);
    }
  }


  //USER

  async getUsers(): Promise<IUser[]> {
    let request = await fetch(this.api + `users`, {
      headers: this.headers,
    });
    let response = await request.json();
    return response;
  }

  async getUser(id) {
    let request = await fetch(this.api + `users/${id}`, {
      headers: this.headers,
    });
    try {
      let student = await request.json();

      return student;
    }
    catch {
      return null;
    }
  }

  async saveUser(user: IUser) {
    //todo front-end validations

    let res: Response;

    try {
      if (user.Id) {

        res = await fetch(this.api + 'users/' + user.Id , {
          method: 'PUT',
          body: JSON.stringify(user),
          headers: this.headers,
        });
      }
      else {
        res = await fetch(this.api + 'users', {
          method: 'POST',
          headers: this.headers,
          body: JSON.stringify(user)
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