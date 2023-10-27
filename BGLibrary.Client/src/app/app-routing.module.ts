import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {BookListV1Component} from "./components/book-list-v1/book-list-v1.component";
import {JwtGuard} from "./guards/jwt.guard";

const routes: Routes = [
  {path:'',component: BookListV1Component},
  //{path:'books',component: BookListV1Component, canActivate: [JwtGuard]}
  {path:'books',component: BookListV1Component}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
