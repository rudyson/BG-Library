import {HTTP_INTERCEPTORS, HttpClientModule, provideHttpClient, withInterceptorsFromDi} from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BookTableV1Component } from './components/book-table-v1/book-table-v1.component';
import { BookListV1Component } from './components/book-list-v1/book-list-v1.component';
import { BookListitemV1Component } from './components/book-listitem-v1/book-listitem-v1.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {NavbarTopComponent} from "./components/navbar-top/navbar-top.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from "@angular/material/table";
import { BookTableV2Component } from './components/book-table-v2/book-table-v2.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {MatListModule} from "@angular/material/list";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MAT_FORM_FIELD_DEFAULT_OPTIONS} from "@angular/material/form-field";
import { RegistrationFormComponent } from './components/registration-form/registration-form.component';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { UserinfoPageComponent } from './components/userinfo-page/userinfo-page.component';
import {MatTabsModule} from "@angular/material/tabs";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {MatInputModule} from "@angular/material/input";
import { BookNewFormComponent } from './components/book-new-form/book-new-form.component';
import {MatCardModule} from "@angular/material/card";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {JwtModule} from "@auth0/angular-jwt";
import {config} from "rxjs";
import {JwtTokenInterceptorInterceptor} from "./interceptors/jwt/jwt-token.interceptor.interceptor";
import { NotfoundPageComponent } from './components/notfound-page/notfound-page.component';
import { FooterComponent } from './components/footer/footer.component';
import { AuthorListV1Component } from './components/author-list-v1/author-list-v1.component';
import { BookPageComponent } from './components/book-page/book-page.component';
import {MatTooltipModule } from '@angular/material/tooltip';

@NgModule({
    declarations: [
        AppComponent,
        BookTableV1Component,
        BookListV1Component,
        BookListitemV1Component,
        NavbarTopComponent,
        BookTableV2Component,
        RegistrationFormComponent,
        LoginFormComponent,
        UserinfoPageComponent,
        BookNewFormComponent,
        NotfoundPageComponent,
        FooterComponent,
        AuthorListV1Component,
        BookPageComponent
    ],
  imports: [
    BrowserModule, AppRoutingModule, HttpClientModule, NgbModule, BrowserAnimationsModule, MatTableModule, MatPaginatorModule, MatSortModule, MatProgressSpinnerModule, MatListModule, MatIconModule, MatButtonModule, MatToolbarModule, MatTabsModule, MatProgressBarModule, MatInputModule, MatCardModule, ReactiveFormsModule,MatTooltipModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () =>{
          return localStorage.getItem("jwt")
        },
        allowedDomains: ["localhost:44302","localhost:44304"],
        disallowedRoutes: [],
      },
    }), FormsModule,
  ],
  providers: [
    {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}},
    {provide: HTTP_INTERCEPTORS, useClass: JwtTokenInterceptorInterceptor, multi: true},
    provideHttpClient(withInterceptorsFromDi())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
