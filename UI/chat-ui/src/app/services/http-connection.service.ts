import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { HttpResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpConnectionService {

  constructor(private authservice: AuthService, private httpClient: HttpClient) { }

  public get<T>(relativePath: string): Observable<HttpResponse<T>> | null{
    if(!this.authservice.token){
      if(!this.authservice.authenticate()){
        return null;
      }
    }

    return this.httpClient.get<T>(environment.api + relativePath, {headers: {Authorization: `Bearer ${this.authservice.token}`}, observe: "response"});
  }

  public post<T>(relativePath: string, obj : T): Observable<HttpResponse<T>> | null{
    if(!this.authservice.token){
      if(!this.authservice.authenticate()){
        return null;
      }
    }

    return this.httpClient.post<T>(environment.api + relativePath, obj, {headers: {Authorization: `Bearer ${this.authservice.token}`}, observe: "response"});
  }
}
