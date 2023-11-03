import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {NgForm} from "@angular/forms";
import {AuthorizationService} from "../../services/authorization/authorization.service";
import {LoginDto} from "../../special/models/authorization.models";
import {Subject} from "rxjs";

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit{
  invalidLogin: boolean = false;
  isPasswordVisible: boolean = false;
  validationErrors: { [key: string]: string } = {};
 constructor(private router: Router,private http: HttpClient, private authorizationService: AuthorizationService) {
 }

 login(form: NgForm) {
   const credentials : LoginDto = {
     username: form.value.username,
     password: form.value.password
   }
   this.authorizationService.loginRequest(credentials)
     .subscribe(
       {
         next: (jwtToken) => {
           localStorage.setItem("jwt", jwtToken.token);
           location.reload();
           this.invalidLogin = false;
         },
         error: (response) =>{
           if (response.status == 422){
             this.validationErrors = response.error
           }
           if(response.name == 'NetworkError'){
             console.log("API not run")
           }
           console.log(response);
           this.invalidLogin = true;
         }
       });
   console.log(this.invalidLogin)
 }

  ngOnInit(): void {
    if(this.authorizationService.isLoggedIn()){
      this.router.navigate(["/"]);
    }
  }
}
