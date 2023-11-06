import {Component, Input, OnInit} from '@angular/core';
import {Router, ActivatedRoute} from "@angular/router";
import { AuthorCreateRequest, AuthorUpdateRequest } from 'src/app/special/models/author.models';
import {NgForm} from "@angular/forms";
import {AuthorsService} from "../../../../../core/services/authors/authors.service";
import {AuthorizationService} from "../../../../../core/services/authorization/authorization.service";

@Component({
  selector: 'app-author-page',
  templateUrl: './author-page.component.html',
  styleUrls: ['./author-page.component.css']
})
export class AuthorPageComponent implements OnInit{
  public id: number | undefined = undefined;
  public model: AuthorUpdateRequest | undefined = undefined;
  public hasValidationError: boolean = false;
  //public validationErrors: Array<string>

  constructor(private router: Router, private route: ActivatedRoute, private authorsService: AuthorsService, public authorizationService: AuthorizationService) {
  }
  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id') ?? undefined);
    if (this.id===undefined){
      return;
    }
    this.authorsService.getAuthor(this.id).subscribe({
      next: (response) => {
        let tempModel: AuthorUpdateRequest = {
          name: response.data?.name,
          surname: response.data?.surname,
          birthday: response.data?.birthday
        }
        this.model = tempModel;
      },
      error: (response) =>{
        this.model = undefined;
        this.id = undefined;
        this.router.navigate(["/author"]);
      }
    }
    )
  }
  pageHasNoModel(): boolean{
    return (this.id ===undefined);
  }

  submit(form: NgForm) {
    if (this.pageHasNoModel()){
      const authorModel : AuthorCreateRequest = {
        name: form.value.name == "" ? undefined: form.value.name,
        surname: form.value.surname == "" ? undefined: form.value.surname,
        birthday: form.value.birthday == undefined ? undefined: form.value.birthday,
      }
      this.authorsService.createAuthor(authorModel).subscribe(
        {
          next: (author) => {
            alert("Author created")
            console.log(author)
          },
          error: (response) =>{
            console.log("Create author");
            console.log(response)
          }
        }
      );
    }
    else {
      if (this.id != undefined) {
        const authorModel : AuthorUpdateRequest = {
          name: form.value.name == "" ? undefined: form.value.name,
          surname: form.value.surname == "" ? undefined: form.value.surname,
          birthday: form.value.birthday == undefined ? undefined: form.value.birthday,
        }
        this.authorsService.updateAuthor(this.id, authorModel).subscribe({
          next: (author) => {
            alert("Author updated")
          },
          error: (response) =>{
            console.log("Update author");
            console.log(response)
          }
        })
      }
      else{
        console.log("Return condition");
      }
    }
  }
}
