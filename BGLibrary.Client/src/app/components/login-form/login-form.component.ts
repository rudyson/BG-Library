import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {NgForm} from "@angular/forms";
import {AuthorizationService} from "../../services/authorization.service";
import {LoginDto} from "../../special/authorization.models";
import {Subject} from "rxjs";

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent {
  invalidLogin: boolean = false;
  isPasswordVisible: boolean = false;
 constructor(private router: Router,private http: HttpClient, private authorizationService: AuthorizationService) {
 }

 login(form: NgForm) {
   const credentials : LoginDto = {
     username: form.value.username,
     password: form.value.password
   }

   let invalidLogin;
   if (this.authorizationService.login(credentials)) {
     invalidLogin = false;
     this.router.navigate(["/"]);
   }
   else {
     invalidLogin = true;
   }
 }
 showPassword() {
   this.isPasswordVisible = !this.isPasswordVisible;
 }
}
