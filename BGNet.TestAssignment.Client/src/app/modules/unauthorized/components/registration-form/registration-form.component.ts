import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { NgForm } from "@angular/forms";
import { AuthorizationService } from "../../../../core/services/authorization/authorization.service";
import { RegisterDto, UserInfoDto } from "../../../../special/models/authorization.models";
import { ResponseWrapper } from "src/app/special/models/request.models";

@Component({
    selector: "app-registration-form",
    templateUrl: "./registration-form.component.html",
    styleUrls: ["./registration-form.component.css"],
})
export class RegistrationFormComponent implements OnInit {
    isPasswordVisible: boolean = false;
    registrationResponse: ResponseWrapper<UserInfoDto> | undefined;
    constructor(
        private router: Router,
        private http: HttpClient,
        private authorizationService: AuthorizationService,
    ) {}

    register(form: NgForm) {
        const credentials: RegisterDto = {
            username: form.value.username,
            password: form.value.password,
            name: form.value.name,
            surname: form.value.surname,
            birthday: form.value.birthday,
            address: form.value.address,
        };
        this.authorizationService.registerRequest(credentials).subscribe({
            next: (response) => {
                this.registrationResponse = response;
                if (response.status == 200) {
                    this.router.navigate(["/login"]);
                }
            },
            error: (response) => {
                if (response instanceof HttpErrorResponse) {
                    alert("Network error. Backend is not running");
                }
            },
        });
    }

    ngOnInit(): void {
        if (this.authorizationService.isLoggedIn()) {
            this.router.navigate(["/"]);
        }
    }
}
