import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {NgForm} from "@angular/forms";
import {Subject} from "rxjs";
import {LoginDto} from "../../../../special/models/authorization.models";
import {AuthorizationService} from "../../../../core/services/authorization/authorization.service";

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
         next: (response) => {
          if (response.hasData&&!response.hasErrors){
            localStorage.setItem("jwt", response.data?.token ?? "");
            location.reload();
            this.invalidLogin = false;
          }
          else{
            for (let index = 0; index < response.errors!.length; index++) {
              console.error(response.errors![index]);
            }
          }
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
 }

  ngOnInit(): void {
    if(this.authorizationService.isLoggedIn()){
      this.router.navigate(["/"]);
    }
  }
}
