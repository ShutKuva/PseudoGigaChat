import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { sha256 } from 'js-sha256';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: FormGroup = new FormGroup({
    name: new FormControl(''),
    password: new FormControl('')
  })

  constructor(private auth: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  login(){
    this.auth.authenticate({name: this.form.controls["name"].value, password: sha256(this.form.controls["password"].value)});

    this.back();
  }

  back(){
    this.router.navigate([""]);
  }
}
