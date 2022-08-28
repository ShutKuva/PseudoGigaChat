import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { first } from 'rxjs';
import { GroupDTO } from 'src/app/models/groupDTO';
import { AuthService } from 'src/app/services/auth.service';
import { HttpConnectionService } from 'src/app/services/http-connection.service';

@Component({
  selector: 'app-left-side',
  templateUrl: './left-side.component.html',
  styleUrls: ['./left-side.component.css']
})
export class LeftSideComponent implements OnInit {
  groups: GroupDTO[] = [];

  @Output() groupChosen: EventEmitter<number> = new EventEmitter<number>();

  constructor(private httpService: HttpConnectionService, private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.tokenChanged.push({callback: this.getGroups.bind(this)});

    if(this.authService.token){
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
}
