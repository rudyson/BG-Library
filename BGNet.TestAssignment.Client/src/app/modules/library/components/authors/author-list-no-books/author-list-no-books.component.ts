import { Component, OnInit } from "@angular/core";
import { PageEvent } from "@angular/material/paginator";
import { Router, ActivatedRoute } from "@angular/router";
import { AuthorsService } from "src/app/core/services/authors/authors.service";
import { AuthorFullInfoDto } from "src/app/special/models/author.models";
import { GenericPaginationModel, ResponseWrapper } from "src/app/special/models/request.models";
import { environment } from "src/app/shared/environment";
import { HttpErrorResponse } from "@angular/common/http";

@Component({
    selector: "app-author-list-no-books",
    templateUrl: "./author-list-no-books.component.html",
    styleUrls: ["./author-list-no-books.component.css"],
})
export class AuthorListNoBooksComponent implements OnInit {
    public authorsResponse?: ResponseWrapper<GenericPaginationModel<AuthorFullInfoDto>>;
    public pageSizeOptions!: number[] | readonly number[];
    public currentPage: number;
    public pageSize: number;
    public unableLoad: boolean = false;
    constructor(
        private authorsService: AuthorsService,
        private router: Router,
        private route: ActivatedRoute,
    ) {
        this.currentPage = Number(this.route.snapshot.paramMap.get("id") ?? 1);
        this.pageSizeOptions = environment.pageSizeOptions;
        this.pageSize = environment.pageSizeOptions[0];
    }
    ngOnInit(): void {
        this.handlePaginationEvent(undefined);
    }
    handlePaginationEvent($event?: PageEvent) {
        let take = $event?.pageSize === undefined ? 5 : $event?.pageSize;
        let skip = $event?.pageIndex === undefined ? 0 : $event?.pageIndex! * take;
        this.authorsService.getAllAuthors(skip, take).subscribe({
            next: (response) => {
                this.authorsResponse = response;
                window.history.replaceState({}, "", `/authors/${this.currentPage}`);
            },
            error: (response) => {
                if (response instanceof HttpErrorResponse) {
                    this.unableLoad = true;
                }
            },
        });
    }
    deleteAuthor(id: number) {
        if (confirm("Do you want to delete author with id " + id + "?")) {
            this.authorsService.deleteAuthor(id).subscribe({
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

