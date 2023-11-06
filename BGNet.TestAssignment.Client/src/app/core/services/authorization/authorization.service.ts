import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Router} from "@angular/router";
import {JwtHelperService} from "@auth0/angular-jwt";
import { ResponseWrapper } from 'src/app/special/models/request.models';
import {environment} from "../../../shared/environment";
import {JwtToken, JwtTokenDto, LoginDto, RegisterDto, UserInfoDto} from "../../../special/models/authorization.models";

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  private authorizationApiUrl: string = environment.authorizationApiUrl;

  constructor(private router: Router,private http: HttpClient, private jwtHelperService:JwtHelperService) { }
  register(model: RegisterDto) : boolean {
    this.registerRequest(model)
      .subscribe({
        next: (response) => {
          if (response.hasData&&!response.hasErrors){
            location.reload();
            return true;
          }
          else {
            console.log("Error due registration:")
            for (let index = 0; index < response.errors!.length; index++) {
              console.error(response.errors![index])
            }

            return false;
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

  registerRequest(model: RegisterDto) : Observable<ResponseWrapper<UserInfoDto>> {
    return this.http.post<ResponseWrapper<UserInfoDto>>(this.authorizationApiUrl+'register',model);
  }
  loginRequest(model: LoginDto) : Observable<ResponseWrapper<JwtTokenDto>> {
    return this.http.post<ResponseWrapper<JwtTokenDto>>(this.authorizationApiUrl+'login',model);
  }
  aboutMe() : Observable<ResponseWrapper<UserInfoDto>>{
    return this.http.get<ResponseWrapper<UserInfoDto>>(this.authorizationApiUrl+'info');
  }

  login(model: LoginDto) : boolean {
    this.loginRequest(model)
      .subscribe(
        {
          next: (jwtTokenRequest) => {
            localStorage.setItem("jwt", jwtTokenRequest.data?.token ?? "");
            location.reload();
            return true;
          },
          error: (response) =>{
            if(response.name == 'NetworkError'){
              alert("API not run")
            }
            alert("error: "+response)
            console.log(response);
            return false;
          }
        });
        return false;
  }

  logout() : boolean{
    localStorage.removeItem("jwt");
    location.reload();
    return true;
  }

  userName() : string | undefined{
    const token: string | null = localStorage.getItem("jwt");
    if (token===null) return undefined;
    let claims: JwtToken = JSON.parse(window.atob(token.split('.')[1]));
    return claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
  }
  isLoggedIn() : boolean {
    return !(this.token()===null||this.jwtHelperService.isTokenExpired(this.token()))
  }
  guardCheck() : boolean{
    if (!this.isLoggedIn()){
      this.router.navigate(["/login"])
      return false;
    }
    let authorized = this.isLoggedIn();
    if (!authorized) this.router.navigate(["/login"]);
    return authorized;
  }
  token() : string | null{
    const token: string | null = localStorage.getItem("jwt");
    if (token===null) return null;
    if (!this.jwtHelperService.isTokenExpired(token)){
      return token;
    }
    else {
      localStorage.removeItem("jwt");
      return null;
    }
  }
}
