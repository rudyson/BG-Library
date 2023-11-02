import { AuthorDtoUpdate } from 'src/app/special/entities';
import {Component, Input, OnInit} from '@angular/core';
import {BookNewDto} from "../../special/entities";
import {Router, ActivatedRoute} from "@angular/router";
import {BooksService} from "../../services/books/books.service";
import {NgForm} from "@angular/forms";
import {AuthorizationService} from "../../services/authorization/authorization.service";
import { AuthorsService } from 'src/app/services/authors/authors.service';

@Component({
  selector: 'app-author-page',
  templateUrl: './author-page.component.html',
  styleUrls: ['./author-page.component.css']
})
export class AuthorPageComponent implements OnInit{
  public id: number | undefined = undefined;
  public model: AuthorDtoUpdate | undefined = undefined;
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
      next: (author) => {
        let tempModel: AuthorDtoUpdate = {
          name: author.name,
          surname: author.surname,
          birthday: author.birthday
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
    const authorModel : AuthorDtoUpdate = {
      name: form.value.name == "" ? undefined: form.value.name,
      surname: form.value.surname == "" ? undefined: form.value.surname,
      birthday: form.value.birthday == undefined ? undefined: form.value.birthday,
    }
    if (this.pageHasNoModel()){
      console.log("Create condition");
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
        console.log("Update condition");
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
