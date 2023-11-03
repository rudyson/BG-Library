import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpErrorResponse
} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {AuthorizationService} from "../../services/authorization/authorization.service";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class JwtTokenInterceptorInterceptor implements HttpInterceptor {

  constructor(private authorizationService: AuthorizationService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.authorizationService.token()}`
      }
    }));
  }
/*
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.authorizationService.token();
    if (token!=null){
      request = request.clone({
        setHeaders: {
          Authorization: 'Bearer '+token
        },
        //withCredentials: true
      })
    }
    return next.handle(request).pipe(
      catchError((err: any)=> {
        if (err instanceof HttpErrorResponse){
          if(err.status === 401){
            console.log("Warning. Token is expired or authorization failed, please login again");
            this.router.navigate(['login']);
          }
        }
        console.log("JWT")
        console.log(err)
        console.log(err.message)
        return throwError(() => new Error("[JwtTokenInterceptorInterceptor] Unhandled error occured"));
      })
    );
    }*/
}
