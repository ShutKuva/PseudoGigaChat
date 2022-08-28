import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MessageDTO } from 'src/app/models/messageDTO';

@Component({
  selector: 'app-client-message',
  templateUrl: './client-message.component.html',
  styleUrls: ['./client-message.component.css']
})
export class ClientMessageComponent implements OnInit {
  @Input() message: MessageDTO;

  @Output() edited: EventEmitter<string> = new EventEmitter<string>();
  @Output() deleted: EventEmitter<any> = new EventEmitter<any>();
  @Output() replied: EventEmitter<any> = new EventEmitter<any>();

  noEdit: boolean = true;

  editInput: FormControl = new FormControl();

  constructor() { }

  ngOnInit(): void {
  }

  edit(){
    this.edited.emit(this.editInput.value);
    this.editInput.patchValue("");
  }

  openEdit(){
    this.editInput.patchValue(this.message.text);
    this.noEdit = false;
  }

  closeEdit(){
    this.noEdit = true;
  }
}
