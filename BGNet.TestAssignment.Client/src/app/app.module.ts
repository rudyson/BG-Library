import {HTTP_INTERCEPTORS, HttpClientModule, provideHttpClient, withInterceptorsFromDi} from '@angular/common/http';
import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from "@angular/material/table";
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {MatListModule} from "@angular/material/list";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MAT_FORM_FIELD_DEFAULT_OPTIONS} from "@angular/material/form-field";

import { RegistrationFormComponent } from './modules/unauthorized/components/registration-form/registration-form.component'
import { LoginFormComponent } from './modules/unauthorized/components/login-form/login-form.component';
import { UserinfoPageComponent } from './modules/unauthorized/pages/userinfo-page/userinfo-page.component';
import { BookListV1Component } from './modules/library/components/books/book-list-v1/book-list-v1.component';
import {NavbarTopComponent} from "./core/nav/header/navbar-top.component";
import { JwtInterceptor } from "./core/interceptors/jwt/jwt.interceptor";
import { NotfoundPageComponent } from './shared/pages/notfound-page/notfound-page.component';
import { FooterComponent } from "./core/nav/footer/footer.component";
import { AuthorListV1Component } from './modules/library/components/authors/author-list-v1/author-list-v1.component';
import { BookPageComponent } from './modules/library/components/books/book-page/book-page.component';
import { AuthorPageComponent } from './modules/library/components/authors/author-page/author-page.component';
import { ErrorHandlerService } from './core/services/error-handler/error-handler.service';

import {MatTabsModule} from "@angular/material/tabs";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {MatInputModule} from "@angular/material/input";

import {MatCardModule} from "@angular/material/card";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {JwtModule} from "@auth0/angular-jwt";
import {config} from "rxjs";

import {MatTooltipModule } from '@angular/material/tooltip';

import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';
import {MatSnackBar, MatSnackBarConfig, MatSnackBarModule} from '@angular/material/snack-bar';
import { AuthorListNoBooksComponent } from './modules/library/components/authors/author-list-no-books/author-list-no-books.component';

@NgModule({
    declarations: [
        AppComponent,
        BookListV1Component,
        NavbarTopComponent,
        RegistrationFormComponent,
        LoginFormComponent,
        UserinfoPageComponent,
        NotfoundPageComponent,
        FooterComponent,
        AuthorListV1Component,
        BookPageComponent,
        AuthorPageComponent,
        AuthorListNoBooksComponent
    ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatListModule,
    MatIconModule,
    MatButtonModule,
    MatToolbarModule,
    MatTabsModule,
    MatProgressBarModule,
    MatInputModule,
    MatCardModule,
    ReactiveFormsModule,
    MatTooltipModule,
    MatAutocompleteModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () =>{
          return localStorage.getItem("jwt")
        },
        allowedDomains: ["localhost:44443","localhost:44080"],
        disallowedRoutes: [],
      },
    }), FormsModule,
  ],
  providers: [
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: {appearance: 'outline'}
    },
    {
      provide: MAT_SNACK_BAR_DEFAULT_OPTIONS,
      useValue: {duration: 2500}
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    {
      provide: ErrorHandler,
      useClass: ErrorHandlerService
    },
    provideHttpClient(withInterceptorsFromDi())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
