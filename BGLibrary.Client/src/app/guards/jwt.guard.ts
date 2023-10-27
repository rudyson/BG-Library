import {Injectable} from "@angular/core";
import {CanActivate} from "@angular/router";
import {AuthorizationService} from "../services/authorization.service";

@Injectable()
export class JwtGuard implements CanActivate{
  constructor(private authorizationService: AuthorizationService) {
  }
  canActivate() {
    return this.authorizationService.isLoggedIn();
  }
}
