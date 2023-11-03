import {Injectable} from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {JwtToken, JwtTokenDto, LoginDto, RegisterDto, UserInfoDto} from "../../special/models/authorization.models";
import {Router} from "@angular/router";
import {JwtHelperService} from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  private authorizationApiUrl: string = environment.authorizationApiUrl;
  private baseRoute: string = '/auth/';

  constructor(private router: Router,private http: HttpClient, private jwtHelperService:JwtHelperService) { }
  register(model: RegisterDto) : boolean {
    this.http.post(this.authorizationApiUrl+this.baseRoute+'register',model)
      .subscribe({
        next: () => {
          location.reload();
          return true;
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

  registerRequest(model: RegisterDto) : Observable<boolean> {
    return this.http.post<boolean>(this.authorizationApiUrl+this.baseRoute+'register',model);
  }

  login(model: LoginDto) : boolean {
    this.http.post<JwtTokenDto>(
      this.authorizationApiUrl+this.baseRoute+'login',
      model)
      .subscribe({
      next: (jwtToken) => {
        localStorage.setItem("jwt", jwtToken.token);
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
    })
    return false;
  }

  loginRequest(model: LoginDto) : Observable<boolean> {
    return this.http.post<boolean>(this.authorizationApiUrl+this.baseRoute+'login',model);
  }

  logout() : boolean{
    localStorage.removeItem("jwt");
    location.reload();
    return true;
  }
  aboutMe() : Observable<UserInfoDto>{
    return this.http.get<UserInfoDto>(this.authorizationApiUrl+this.baseRoute+'info');
  }
  userName() : string | undefined{
    const token: string | null = localStorage.getItem("jwt");
    if (token===null) return undefined;
    let claims: JwtToken = JSON.parse(window.atob(token.split('.')[1]));
    return claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
  }
  isLoggedIn() : boolean {
    return !(this.token()===null)
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
