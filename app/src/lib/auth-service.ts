import { API_URL } from '$lib/Env';
import { Result } from '$lib/result'
import { isAuthenticated, service } from '$lib/store';
import { Service } from '$lib/service';
import Cookies from 'js-cookie'

export class AuthService {

  api:string;
  constructor(){
    this.api = API_URL as any ?? 'http://localhost:5000';
  }

  headers = {
    'Content-Type': 'application/json',
  }

  public async login(user:string, pwd:string){
    let body = {Email:user, Password:pwd, "grantType": "password"};
    let request = await fetch(this.api + '/login', {
      method:'POST',
      body:JSON.stringify(body),
      headers:this.headers
    });

    if(request.ok){
      this.setup(await request.json())
      return Result.Succeed();
    }
    else{
      return Result.Error(await request.text());
    }
  }

  private setup(data){
    isAuthenticated.set(true);
    this.saveToken(data);
    service.set(new Service(data.token));
  }

  private saveToken(security){
    document.cookie = `Keep-JWT=${security.Token}; path=/; secure;  SameSite=Strict`;
    document.cookie = `Keep-expiration=${security.Expiration}; path=/; secure;  SameSite=Strict `;
    document.cookie = `Keep-refresh=${security.RefreshToken}; path=/; secure;  SameSite=Strict`;
  }

  public logout(){
    isAuthenticated.set(false);
    document.cookie = `Keep-JWT=; path=/; secure;  SameSite=Strict`;
    document.cookie = `Keep-expiration=; path=/; secure;  SameSite=Strict `;
    document.cookie = `Keep-refresh=; path=/; secure;  SameSite=Strict`;
  }

  public reload(){
    if(!this.isValidExpiration()){
      //this.refreshToken();
      return false;
    }

    let token = Cookies.get('Keep-JWT');
    if(token){
      isAuthenticated.set(true);
      service.set(new Service(token));
      return true;
    }
    else{
      return false;
    }
  }

  private refreshToken(){

  }

  private getExpiration(){
    let ex = Cookies.get('Keep-expiration');
    if(ex){
      return new Date(ex);
    }
    else return null;
  }

  private isValidExpiration(){
    let exDate = this.getExpiration();
    return exDate && new Date() < exDate;
  }
}