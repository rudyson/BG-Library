import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { PageEvent } from "@angular/material/paginator";
import { BookFullInfoDto } from "src/app/special/models/book.models";
import { GenericPaginationModel, ResponseWrapper } from "../../../../../special/models/request.models";
import { BooksService } from "../../../../../core/services/books/books.service";
import { environment } from "src/app/shared/environment";
import { HttpErrorResponse } from "@angular/common/http";

@Component({
    selector: "app-book-list-v1",
    templateUrl: "./book-list-v1.component.html",
    styleUrls: ["./book-list-v1.component.css"],
})
export class BookListV1Component implements OnInit {
    public books?: GenericPaginationModel<BookFullInfoDto>;
    public booksResponse?: ResponseWrapper<GenericPaginationModel<BookFullInfoDto>>;
    public pageSizeOptions!: number[] | readonly number[];
    public unableLoad: boolean = false;
    constructor(
        private booksService: BooksService,
        private router: Router,
        private route: ActivatedRoute,
    ) {}
    ngOnInit(): void {
        this.pageSizeOptions = environment.pageSizeOptions;
        this.handlePaginationEvent(undefined);
    }

    handlePaginationEvent($event?: PageEvent) {
        let take = $event?.pageSize === undefined ? 5 : $event?.pageSize;
        let skip = $event?.pageIndex === undefined ? 0 : $event?.pageIndex! * take;
        this.booksService.getAllBooks(skip, take).subscribe({
            next: (response) => {
                this.unableLoad = false;
                this.books = response.data;
                this.booksResponse = response;
                window.history.replaceState({}, "", `/books/${this.books?.page}`);
            },
            error: (error) => {
                if (error instanceof HttpErrorResponse) {
                    this.unableLoad = true;
                }
            },
        });
    }
    deleteBook(id: number) {
        if (confirm("Do you want to delete book with id " + id + "?")) {
            this.booksService.deleteBook(id).subscribe({
                next: () => {
                    this.handlePaginationEvent(undefined);
                },
                error: (response) => {
                    console.log("Deletion error");
                    console.log(response);
                },
            });
        }
    }
}
