import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MessageDTO } from 'src/app/models/messageDTO';

@Component({
  selector: 'app-someone-message',
  templateUrl: './someone-message.component.html',
  styleUrls: ['./someone-message.component.css']
})
export class SomeoneMessageComponent implements OnInit {
  @Input() message: MessageDTO;

  @Output() replied: EventEmitter<any> = new EventEmitter<any>();
  @Output() repliedAndRedirect: EventEmitter<number> = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

}
