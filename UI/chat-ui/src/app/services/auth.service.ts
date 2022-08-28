import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UserDTO } from '../models/userDTO';
import { first, firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _token: string | null = null;

  public get token(): string | null{
    return this._token;
  }

  public tokenChanged: {callback: () => void}[] = [];

  constructor(private httpClient: HttpClient){  }

  public async authenticate(user?: UserDTO) : Promise<boolean>{
    if(user){
      let result: boolean = true;

      let response = await firstValueFrom(this.httpClient.post(environment.api + environment.auth, user, {observe: "response"})
        .pipe(first())).catch(err => console.log(err));
        
      if(response?.ok){
        this._token = (<{token: string}>response.body!).token;
        localStorage.setItem("token", this._token);
        this.tokenChanged.forEach(entity => entity.callback());
      }
      else{
        result = false;
        this.logout();
      }

      return result;
    }
    else{
      const tokenFromLocal = localStorage.getItem("token");

      if(tokenFromLocal){
        this._token = tokenFromLocal

        if(await this.validate()){
          this.tokenChanged.forEach(entity => entity.callback());
          return true;
        }

        this.logout();
      }

      return false;
    }
  }

  public logout(){
    localStorage.removeItem("token");
    this._token = null;
  }

  public async validate() : Promise<boolean>{
    let result: boolean = false;

    let response = await firstValueFrom(this.httpClient.get(environment.api + environment.validateToken, {headers: {Authorization: `Bearer ${this.token}`}, observe: "response"})
      .pipe(first())).catch(err => console.log(err));

    if(response?.ok){
      result = true;
    }

    return result;
  }
}
