import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { NgForm } from "@angular/forms";
import { Subject } from "rxjs";
import { JwtTokenDto, LoginDto } from "../../../../special/models/authorization.models";
import { AuthorizationService } from "../../../../core/services/authorization/authorization.service";
import { ResponseWrapper } from "src/app/special/models/request.models";

@Component({
    selector: "app-login-form",
    templateUrl: "./login-form.component.html",
    styleUrls: ["./login-form.component.css"],
})
export class LoginFormComponent implements OnInit {
    loginResponse: ResponseWrapper<JwtTokenDto> | undefined;
    isPasswordVisible: boolean = false;
    constructor(
        private router: Router,
        private http: HttpClient,
        private authorizationService: AuthorizationService,
    ) {}

    login(form: NgForm) {
        const credentials: LoginDto = {
            username: form.value.username,
            password: form.value.password,
        };
        this.authorizationService.loginRequest(credentials).subscribe({
            next: (response) => {
                console.log(response);
                this.loginResponse = response;
                if (this.loginResponse.data?.token) {
                    localStorage.setItem("jwt", response.data?.token ?? "");
                    location.reload();
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
