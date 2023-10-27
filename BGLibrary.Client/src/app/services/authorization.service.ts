import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {LoginDto, RegisterDto, UserInfoDto} from "../special/authorization.models";
import {Router} from "@angular/router";
import {JwtHelperService} from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  private authorizationApiUrl: string = environment.authorizationApiUrl;
  private baseRoute: string = '/auth/';

  constructor(private router: Router,private http: HttpClient, private jwtHelperService:JwtHelperService) { }
  register(model: RegisterDto) : void {
    this.http.post(this.authorizationApiUrl+this.baseRoute+'register',model);
  }
  login(model: LoginDto) : boolean {
    this.http.post(
      this.authorizationApiUrl+this.baseRoute+'login',
      model,
      { responseType: 'text'})
      .subscribe({
      next: (jwtToken) => {
        localStorage.setItem("jwt", jwtToken);
        this.router.navigate(["/"]).then(() => true);
        location.reload();
        return true;
      },
      error: (response) =>{
        console.log(response);
        return false;
      }
    })
    return false;
  }
  logout() : boolean{
    localStorage.removeItem("jwt");
    this.router.navigate(["/"]).then(() => true);
    location.reload();
    return true;
  }
  aboutMe() : Observable<UserInfoDto>{
    return this.http.get<UserInfoDto>(this.authorizationApiUrl+this.baseRoute+'info');
  }
  isLoggedIn() : boolean {
    const token: string | null = localStorage.getItem("jwt");
    if (token===null) return false;
    return !this.jwtHelperService.isTokenExpired(token);
  }
}
