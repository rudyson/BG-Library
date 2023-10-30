import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {AuthorizationService} from "../../services/authorization/authorization.service";
import {NgForm} from "@angular/forms";
import {LoginDto, RegisterDto} from "../../special/authorization.models";

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
    if (this.authorizationService.register(credentials)){
      this.router.navigate(["/login"]);
    }
    else console.log("Registration error");
  }

  ngOnInit(): void {
    if(this.authorizationService.isLoggedIn()){
      this.router.navigate(["/"]);
    }
  }
}
