import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { first } from 'rxjs';
import { GroupDTO } from 'src/app/models/groupDTO';
import { AuthService } from 'src/app/services/auth.service';
import { HttpConnectionService } from 'src/app/services/http-connection.service';
import { SignalConnectionService } from 'src/app/services/signalr-connection.service';

@Component({
  selector: 'app-left-side',
  templateUrl: './left-side.component.html',
  styleUrls: ['./left-side.component.css']
})
export class LeftSideComponent implements OnInit {
  groups: GroupDTO[] = [];

  @Output() groupChosen: EventEmitter<number> = new EventEmitter<number>();

  handleRegistered: boolean = false;

  constructor(private httpService: HttpConnectionService, private authService: AuthService, private signalrService: SignalConnectionService) { }

  ngOnInit(): void {
    this.authService.tokenChanged.push({callback: () => {
      this.tryRegisterHandle();
      this.getGroups();
    }});

    if(this.authService.token){
      this.tryRegisterHandle();
      this.getGroups();
    }
  }

  private getGroups(){
    const subscription = this.httpService.get<GroupDTO[]>("/api/Source/groups");
      if(subscription){
        subscription.pipe(first())
          .subscribe(response => {
            if(response.ok){
              this.groups = response.body!;
            }
          })
      }
      else{
        this.groups = [];
      }
  }

  groupSelected(id: number){
    this.groupChosen.emit(id);
  }

  private tryRegisterHandle(){
    if(!this.handleRegistered && this.signalrService.conected){
      this.signalrService.registerNewHandler("GroupsUpdated", (firstId: number, secondId: number) =>{
        this.getGroups();
      });

      this.handleRegistered = true;
    }
  }
}
