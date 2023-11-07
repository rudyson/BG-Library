import { Component, Input, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { AuthorCreateRequest, AuthorFullInfoDto, AuthorShortInfoDto, AuthorUpdateRequest } from "src/app/special/models/author.models";
import { NgForm } from "@angular/forms";
import { AuthorsService } from "../../../../../core/services/authors/authors.service";
import { AuthorizationService } from "../../../../../core/services/authorization/authorization.service";
import { ResponseWrapper } from "src/app/special/models/request.models";

@Component({
    selector: "app-author-page",
    templateUrl: "./author-page.component.html",
    styleUrls: ["./author-page.component.css"],
})
export class AuthorPageComponent implements OnInit {
    public id: number | undefined = undefined;
    //public model: AuthorUpdateRequest | undefined = undefined;
    public authorInfo: ResponseWrapper<AuthorFullInfoDto> | undefined;
    public authorShortInfoResponse: ResponseWrapper<AuthorShortInfoDto> | undefined;
    public hasValidationError: boolean = false;
    //public validationErrors: Array<string>

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private authorsService: AuthorsService,
        public authorizationService: AuthorizationService,
    ) {}
    ngOnInit(): void {
        if (this.route.snapshot.paramMap.get("id") == null) {
            this.router.navigate(["/author"]);
        } else {
            this.id = Number(this.route.snapshot.paramMap.get("id") ?? undefined);
        }
        if (this.id === undefined) {
            return;
        }
        this.authorsService.getAuthor(this.id).subscribe({
            next: (response) => {
                this.authorInfo = response;
                let tempModel: AuthorUpdateRequest = {
                    name: response.data?.name,
                    surname: response.data?.surname,
                    birthday: response.data?.birthday,
                };
                //this.model = tempModel;
            },
            error: (response) => {
                //this.model = undefined;
                this.id = undefined;
                this.router.navigate(["/author"]);
            },
        });
    }

    submit(form: NgForm) {
        if (!this.id) {
            const authorModel: AuthorCreateRequest = {
                name: form.value.name == "" ? undefined : form.value.name,
                surname: form.value.surname == "" ? undefined : form.value.surname,
                birthday: form.value.birthday == undefined ? undefined : form.value.birthday,
            };
            this.authorsService.createAuthor(authorModel).subscribe({
                next: (response) => {
                    if (response.status == 200) {
                        form.reset();
                        this.router.navigate(["/books"]);
                    }
                    this.authorShortInfoResponse = response;
                },
                error: (response) => {
                    console.log("Create author");
                    console.log(response);
                },
            });
        } else {
            if (this.id != undefined) {
                const authorModel: AuthorUpdateRequest = {
                    name: form.value.name == "" ? undefined : form.value.name,
                    surname: form.value.surname == "" ? undefined : form.value.surname,
                    birthday: form.value.birthday == undefined ? undefined : form.value.birthday,
                };
                this.authorsService.updateAuthor(this.id, authorModel).subscribe({
                    next: (response) => {
                        this.authorShortInfoResponse = response;
                        if (response.status == 200) {
                            this.router.navigate(["/books"]);
                        }
                    },
                    error: (response) => {
                        console.log("Update author");
                        console.log(response);
                    },
                });
            } else {
                console.log("Return condition");
            }
        }
    }
}
