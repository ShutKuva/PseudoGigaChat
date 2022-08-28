import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  groupId: number = 0;

  constructor() { }

  ngOnInit(): void {
  }

  getGroup(id: number){
    this.groupId = id;
  }

}
