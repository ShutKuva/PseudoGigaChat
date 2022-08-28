import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { HttpConnectionService } from 'src/app/services/http-connection.service';
import { UserDTOFromServer } from 'src/app/models/userDTO-from-server'
import { AuthService } from 'src/app/services/auth.service';
import { MessageDTO } from 'src/app/models/messageDTO';
import { first } from 'rxjs';
import { SignalConnectionService } from 'src/app/services/signalr-connection.service';
import { FormControl } from '@angular/forms';
import { MiniMessageDTO } from 'src/app/models/mini-messageDTO';

@Component({
  selector: 'app-right-side',
  templateUrl: './right-side.component.html',
  styleUrls: ['./right-side.component.css']
})
export class RightSideComponent implements OnChanges {
  user: UserDTOFromServer | null;
  messages: MessageDTO[] = [];

  handlerAdded: boolean = false;

  @Input() groupId: number = 0;

  currentPage: number = 1;
  maxPage: number = 0;

  canBeMorePages: boolean = false;
  canBeLessPages: boolean = false;

  currentlyReplying: MessageDTO | null = null;

  messageInput: FormControl = new FormControl('');

  constructor(private httpService: HttpConnectionService, private signalrService: SignalConnectionService) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.groupId = changes["groupId"].currentValue as number;
    if(this.groupId != 0){
      if(!this.handlerAdded){
        this.signalrService.registerNewHandler('UpdateMessages', (messages, groupId, pages) => {
          if(this.groupId == groupId){
            this.maxPage = pages;
            this.currentPage = pages;
            this.messages = messages;
            this.canBeLessPages = this.currentPage > 1;
            this.canBeMorePages = this.currentPage < this.maxPage;
          }
        });
        this.handlerAdded = true;
      }
      this.getMessagesAndUser();
    }
  }

  getMessagesAndUser(){
    this.httpService.get<UserDTOFromServer>("/api/Source/me")?.pipe(first()).subscribe(response => {
      if(response.ok){
        this.user = response.body;
      }
    });

    this.httpService.get<number>(`/api/Source/message/pages/${this.groupId}`)!.pipe(first()).subscribe(response => {
      if(response.ok){
        this.currentPage = response.body!;
        this.maxPage = response.body!;

        this.canBeLessPages = this.currentPage > 1;
        this.canBeMorePages = false;

        this.getMessages();
      }
    });
  }

  nextPage(){
    if(this.canBeMorePages){
      this.currentPage++;
      this.canBeLessPages = this.currentPage > 1;
      this.canBeMorePages = this.currentPage < this.maxPage;
      this.getMessages();
    }
  }

  previousPage(){
    if(this.canBeLessPages){
      this.currentPage--;
      this.canBeLessPages = this.currentPage > 1;
      this.canBeMorePages = this.currentPage < this.maxPage;
      this.getMessages();
    }
  }

  sendMessage(){
    this.httpService.post<MessageDTO>(`/api/Source/send/${this.groupId}`, {
      id: 0,
      groupId: this.groupId,
      text: this.messageInput.value,
      user: this.user!,
      replied: this.currentlyReplying ?? undefined,
      created: new Date()
    })?.pipe(first()).subscribe();

    this.messageInput.patchValue("");
    this.currentlyReplying = null;
  }

  editMessage(id:number, text: string){
    this.httpService.post<MiniMessageDTO>(`/api/Source/edit/${this.groupId}`, {
      id: id,
      text: text
    })?.pipe(first()).subscribe();
  }

  deleteMessage(id: number){
    this.httpService.get<any>(`/api/Source/delete/${this.groupId}/${id}`)?.pipe(first()).subscribe();
  }

  changeReply(message: MessageDTO){
    this.currentlyReplying = message;
    console.log(message);
  }

  changeReplyAndRedirect(message: MessageDTO, id: number){
    this.changeReply(message);
    this.httpService.get<number>(`/api/Source/private/${id}`)?.pipe(first()).subscribe(response => {
      if(response.ok){
        this.groupId = response.body!;

        this.getMessagesAndUser();
      }
    });
  }

  clearReplying(){
    this.currentlyReplying = null;
  }

  private getMessages(){
    this.httpService.get<MessageDTO[]>(`/api/Source/messages/${this.groupId}/${this.currentPage}`)?.pipe(first()).subscribe(response => {
      if(response.ok){
        this.messages = response.body!;
      }
    });
  }
}