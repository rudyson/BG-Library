import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {BookListV1Component} from "./components/book-list-v1/book-list-v1.component";
import {JwtGuard} from "./guards/jwt.guard";
import {LoginFormComponent} from "./components/login-form/login-form.component";
import {UserinfoPageComponent} from "./components/userinfo-page/userinfo-page.component";
import {NotfoundPageComponent} from "./components/notfound-page/notfound-page.component";
import {RegistrationFormComponent} from "./components/registration-form/registration-form.component";
import {AuthorsService} from "./services/authors/authors.service";
import {AuthorListV1Component} from "./components/author-list-v1/author-list-v1.component";
import {BookPageComponent} from "./components/book-page/book-page.component";
import { AuthorPageComponent } from './components/author-page/author-page.component';

const routes: Routes = [
  {path:'',redirectTo: '/books', pathMatch: "full"},
  //{path:'books',component: BookListV1Component, canActivate: [JwtGuard]}
  {path:'books',component: BookListV1Component, title:'Books', canActivate: [JwtGuard]},
  {path:'books/:id',component: BookListV1Component, title:'Books', canActivate: [JwtGuard]},
  {path:'book/:id',component: BookPageComponent, title:'Book', canActivate: [JwtGuard]},
  {path:'book',component: BookPageComponent, title:'Book', canActivate: [JwtGuard]},
  {path:'authors',component: AuthorListV1Component, title:'Authors', canActivate: [JwtGuard]},
  {path:'authors/:id',component: AuthorListV1Component, title:'Authors', canActivate: [JwtGuard]},
  {path:'author',component: AuthorPageComponent, title:'Author', canActivate: [JwtGuard]},
  {path:'author/:id',component: AuthorPageComponent, title:'Author', canActivate: [JwtGuard]},
  {path:'login',component: LoginFormComponent, title:'Login'},
  {path:'register',component: RegistrationFormComponent, title:'Registration'},
  {path:'me',component: UserinfoPageComponent, title:'me', canActivate: [JwtGuard]},
  { path: '**', component: NotfoundPageComponent, title:'404 Not found' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
