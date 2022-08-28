import { Injectable } from '@angular/core';
import * as signalr from '@microsoft/signalr'
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class SignalConnectionService {
  private connection! : signalr.HubConnection;

  private allMethods : string[] = [];

  private _conected: boolean = false;

  public get conected(){
    return this._conected;
  }

  constructor(private authService: AuthService) {  }

  public async connect() : Promise<boolean>{
    if(!this.authService.token){
      return false;
    }
    
    let success: boolean = false;

    this.connection = new signalr.HubConnectionBuilder()
      .withUrl(environment.api + environment.signalHub, {headers: {Authorization: `Bearer ${this.authService.token}`}})
      .build();

    const startConnection = this.connection.start();

    startConnection
      .then(() => success = true)
      .catch(err => console.log(err));
    
    await startConnection;

    return success;
  }

  public registerNewHandler(methodName: string, handler: (...args: any[]) => void){
    this.connection.on(methodName, handler);
    this.allMethods.push(methodName);
  }

  public shotdown(){
    this.allMethods.forEach(method => this.connection.off(method))
  }
}
