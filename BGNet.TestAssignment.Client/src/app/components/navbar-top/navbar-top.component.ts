import { Component } from '@angular/core';
import {AuthorizationService} from "../../services/authorization/authorization.service";

@Component({
  selector: 'app-navbar-top',
  templateUrl: './navbar-top.component.html',
  styleUrls: ['./navbar-top.component.css']
})
export class NavbarTopComponent {
  constructor(public authorizationService : AuthorizationService) {
  }
  //authorized: boolean = this.authorizationService.isLoggedIn();
  logout(){
    this.authorizationService.logout();
    this.authorizationService.isLoggedIn();
  }

  protected readonly document = document;
}
