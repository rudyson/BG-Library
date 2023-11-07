import { Component, Input, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { NgForm } from "@angular/forms";
import { BookCreateRequest, BookFullInfoDto, BookShortInfoDto, BookUpdateRequest } from "src/app/special/models/book.models";
import { AuthorAutocompleteDto } from "src/app/special/models/author.models";
import { Observable } from "rxjs";
import { MatAutocomplete } from "@angular/material/autocomplete";
import { BooksService } from "../../../../../core/services/books/books.service";
import { AuthorsService } from "../../../../../core/services/authors/authors.service";
import { AuthorizationService } from "../../../../../core/services/authorization/authorization.service";
import { ResponseWrapper } from "src/app/special/models/request.models";

@Component({
    selector: "app-book-page",
    templateUrl: "./book-page.component.html",
    styleUrls: ["./book-page.component.css"],
})
export class BookPageComponent implements OnInit {
    public id: number | undefined = undefined;
    public bookCreateUpdateResponse: ResponseWrapper<BookShortInfoDto> | undefined;
    public model: BookUpdateRequest | undefined = undefined;
    public authorsAutocompleteResponse: ResponseWrapper<AuthorAutocompleteDto[]> | undefined;
    public authors: AuthorAutocompleteDto[] | undefined = [{ id: 1, name: "Test", surname: "Author" }];
    public filteredOptions: Observable<AuthorAutocompleteDto[]> | undefined;
    //myControl: any;
    //dashboardService: any;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private booksService: BooksService,
        private authorsService: AuthorsService,
        public authorizationService: AuthorizationService,
    ) {}

    displayAuthor(author: AuthorAutocompleteDto): string {
        return author && author.name && author.surname ? `${author.surname} ${author.name}` : "";
    }

    loadAuthors(q: string): boolean {
        this.authorsService.searchAuthor(q).subscribe({
            next: (response) => {
                this.authorsAutocompleteResponse = response;
                console.log(response);
                this.authors = response.data;
                return true;
            },
            error: (err) => {
                console.log(err);
                this.authors = undefined;
                return false;
            },
        });
        return false;
    }

    ngOnInit(): void {
        if (this.route.snapshot.paramMap.get("id") == null) {
            this.router.navigate(["/book"]);
        } else {
            this.id = Number(this.route.snapshot.paramMap.get("id") ?? undefined);
        }
        if (this.id === undefined) {
            return;
        }
        this.booksService.getBook(this.id).subscribe({
            next: (response) => {
                let tempModel: BookUpdateRequest = {
                    authorId: response.data?.author?.id,
                    title: response.data?.title,
                    genre: response.data?.genre,
                    publishYear: response.data?.publishYear,
                };
                this.model = tempModel;
            },
            error: (response) => {
                this.model = undefined;
                this.id = undefined;
                this.router.navigate(["/book"]);
            },
        });
    }
    /*
    this.authorsService.searchAuthor(q).subscribe({
      next: (author) =>{

        console.log(author)
        this.authors = author;
        return true;
      },
      error: (err) =>{
        console.log(err)
        this.authors = undefined;
        return false;
      }
    });*/
    /*
    this.myControl.valueChanges
      .subscribe((value: string) => {
        if(value.length >= 1){
          this.authorsService.searchAuthor(value).subscribe({
            next: (response) =>{
              this.authors = response.data;
              return true;
            },
            error: (err) =>{
              console.log(err)
              this.authors = undefined;
              return false;
            }
          })
        }})
      }*/
    /*
  pageHasNoModel(): boolean{
    return (this.model === undefined || this.id ===undefined);
  }
  pageInUpdateMode(): boolean {
    let condition : boolean = (this.model === undefined || this.id ===undefined);
    condition = !condition;
    return condition;
  };*/
    pageHasNoModel(): boolean {
        return this.id === undefined;
    }

    submit(form: NgForm) {
        console.log(form);
        const bookModel: BookCreateRequest = {
            title: form.value.title === "" ? undefined : form.value.title,
            genre: form.value.genre === "" ? undefined : form.value.genre,
            authorId: Number(form.value.authorId),
            publishYear: Number(form.value.authorId),
        };
        console.log(bookModel);
        if (this.pageHasNoModel()) {
            this.booksService.createBook(bookModel).subscribe({
                next: (response) => {
                    if (response.status == 200) {
                        form.reset();
                        this.router.navigate(["/books"]);
                    }
                    this.bookCreateUpdateResponse = response;
                },
                error: (response) => {
                    //console.log("Create book");
                    console.log(response);
                },
            });
        } else {
            if (this.id != undefined) {
                //console.log("Update condition");
                this.booksService.updateBook(this.id, bookModel).subscribe({
                    next: (response) => {
                        if (response.status == 200) {
                            this.router.navigate(["/books"]);
                        }
                        this.bookCreateUpdateResponse = response;
                    },
                    error: (response) => {
                        console.log("Update book");
                        console.log(response);
                    },
                });
            } else {
                console.log("Return condition");
            }
        }
    }
}
