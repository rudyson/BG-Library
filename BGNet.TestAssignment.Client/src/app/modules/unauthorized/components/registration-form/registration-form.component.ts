import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {NgForm} from "@angular/forms";
import {AuthorizationService} from "../../../../core/services/authorization/authorization.service";
import {RegisterDto} from "../../../../special/models/authorization.models";

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent implements OnInit{
  isPasswordVisible: boolean = false;
  constructor(private router: Router,private http: HttpClient, private authorizationService: AuthorizationService) {
  }

  register(form: NgForm) {
    const credentials : RegisterDto = {
      username: form.value.username,
      password: form.value.password,
      name: form.value.name,
      surname: form.value.surname,
      birthday: form.value.birthday,
      address: form.value.address
    }
    this.authorizationService.registerRequest(credentials)
      .subscribe({
        next: (response) => {
          if (response.hasData&&!response.hasErrors){
            this.router.navigate(["/login"]);
          }
          else {
            alert("Provided data is not valid (Password/Username)")
            for (let index = 0; index < response.errors!.length; index++) {
              console.error(response.errors![index])
            }
          }
        },
        error: (response) =>{
          if(response.name == 'NetworkError'){
            alert("API not run")
          }
          console.log(response);
          return false;
        }});
    return false;
  }

  ngOnInit(): void {
    if(this.authorizationService.isLoggedIn()){
      this.router.navigate(["/"]);
    }
  }
}
