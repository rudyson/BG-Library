import {Injectable} from "@angular/core";
import {CanActivate, Router} from "@angular/router";
import {AuthorizationService} from "../../services/authorization/authorization.service";
@Injectable({
  providedIn: 'root'
})
export class JwtGuard implements CanActivate{
  constructor(private authorizationService: AuthorizationService, private router: Router) {
  }
  canActivate() {
    return this.authorizationService.guardCheck();
  }
}
