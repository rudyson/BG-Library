import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { BookListV1Component } from "./modules/library/components/books/book-list-v1/book-list-v1.component";
import { JwtGuard } from "./core/guards/jwt/jwt.guard";
import { BookPageComponent } from "./modules/library/components/books/book-page/book-page.component";
import { AuthorListV1Component } from "./modules/library/components/authors/author-list-v1/author-list-v1.component";
import { AuthorPageComponent } from "./modules/library/components/authors/author-page/author-page.component";
import { LoginFormComponent } from "./modules/unauthorized/components/login-form/login-form.component";
import { RegistrationFormComponent } from "./modules/unauthorized/components/registration-form/registration-form.component";
import { UserinfoPageComponent } from "./modules/unauthorized/pages/userinfo-page/userinfo-page.component";
import { NotfoundPageComponent } from "./shared/pages/notfound-page/notfound-page.component";
import { AuthorListNoBooksComponent } from "./modules/library/components/authors/author-list-no-books/author-list-no-books.component";

const routes: Routes = [
    { path: "", redirectTo: "/books", pathMatch: "full" },
    { path: "books", component: BookListV1Component, title: "Books", canActivate: [JwtGuard] },
    { path: "books/:id", component: BookListV1Component, title: "Books", canActivate: [JwtGuard] },
    { path: "book/:id", component: BookPageComponent, title: "Book", canActivate: [JwtGuard] },
    { path: "book", component: BookPageComponent, title: "Book", canActivate: [JwtGuard] },
    { path: "authors", component: AuthorListNoBooksComponent, title: "Authors", canActivate: [JwtGuard] },
    { path: "authors/:id", component: AuthorListNoBooksComponent, title: "Authors", canActivate: [JwtGuard] },
    { path: "author", component: AuthorPageComponent, title: "Author", canActivate: [JwtGuard] },
    { path: "author/:id", component: AuthorPageComponent, title: "Author", canActivate: [JwtGuard] },
    { path: "login", component: LoginFormComponent, title: "Login" },
    { path: "register", component: RegistrationFormComponent, title: "Registration" },
    { path: "me", component: UserinfoPageComponent, title: "About me", canActivate: [JwtGuard] },
    { path: "**", component: NotfoundPageComponent, title: "404 Not found" },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
