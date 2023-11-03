import {Component, OnInit} from '@angular/core';
import {UserInfoDto} from "../../special/models/authorization.models";
import {AuthorizationService} from "../../services/authorization/authorization.service";

@Component({
  selector: 'app-userinfo-page',
  templateUrl: './userinfo-page.component.html',
  styleUrls: ['./userinfo-page.component.css']
})
export class UserinfoPageComponent implements OnInit{
  constructor(public authorizationService: AuthorizationService) {
  }
  public userInfo?: UserInfoDto = undefined;
  public authorized: boolean = false;
  ngOnInit(): void {
    this.loadUserInformation();
  }
  loadUserInformation(){
    if (this.authorizationService.isLoggedIn()){
      this.authorized=true;
      this.userInfo = undefined;

      this.authorizationService.aboutMe()
        .subscribe({
          next: (response) => {
            this.userInfo = response.data;
          },
          error: (response) =>
            console.log(response)
        })
    }
    else this.authorized=false;
  }

  protected readonly undefined = undefined;
}
