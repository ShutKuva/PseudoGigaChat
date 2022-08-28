import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatComponent } from 'src/app/components/chat/chat.component';
import { LoginComponent } from 'src/app/components/login/login.component';

const routes: Routes = [
  {path: "login", component: LoginComponent},
  {path: "**", component: ChatComponent}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports:[
    RouterModule
  ]
})
export class RoutingModule { }
