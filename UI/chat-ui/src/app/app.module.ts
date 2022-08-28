import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AccountInfoComponent } from './components/account-info/account-info.component';
import { LeftSideComponent } from './components/left-side/left-side.component';
import { RightSideComponent } from './components/right-side/right-side.component';
import { EntityInfoComponent } from './components/entity-info/entity-info.component';
import { ClientMessageComponent } from './components/client-message/client-message.component';
import { SomeoneMessageComponent } from './components/someone-message/someone-message.component';
import { MessageFormComponent } from './components/message-form/message-form.component';

import { HttpClientModule } from '@angular/common/http'
import { HttpClient } from '@microsoft/signalr';
import { AuthService } from './services/auth.service';
import { ChatComponent } from './components/chat/chat.component';
import { RoutingModule } from './modules/routing/routing.module';
import { LoginComponent } from './components/login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    AccountInfoComponent,
    LeftSideComponent,
    RightSideComponent,
    EntityInfoComponent,
    ClientMessageComponent,
    SomeoneMessageComponent,
    MessageFormComponent,
    ChatComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RoutingModule,
    ReactiveFormsModule,
    CommonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
