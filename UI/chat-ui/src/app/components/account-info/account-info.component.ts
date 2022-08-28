import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { SignalConnectionService } from 'src/app/services/signalr-connection.service';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {
  public authenticated: boolean = false;

  constructor(private authService: AuthService, private signalrService: SignalConnectionService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.authService.tokenChanged.push({callback: () => {
      this.authenticated = this.authService.token != null;
    }}) 
    this.tryToAuthenticate();
  }

  async tryToAuthenticate(){
    this.authenticated = await this.authService.authenticate();
    if(this.authenticated){
      await this.signalrService.connect();
    }
  }

  logout(){
    this.authService.logout();
    this.authenticated = false;
    this.router.navigate(["login"], {relativeTo: this.route});
  }

  login(){
    this.router.navigate(["login"], {relativeTo: this.route});
  }
}
