import {Component, Input} from '@angular/core';
import {BookNewDto} from "../../special/entities";
import {Router} from "@angular/router";
import {BooksService} from "../../services/books/books.service";
import {NgForm} from "@angular/forms";
import {AuthorizationService} from "../../services/authorization/authorization.service";

@Component({
  selector: 'app-book-page',
  templateUrl: './book-page.component.html',
  styleUrls: ['./book-page.component.css']
})
export class BookPageComponent {
  @Input() model: BookNewDto | undefined = undefined;
  @Input() id: number | undefined = undefined;

  constructor(private router: Router, private booksService: BooksService, public authorizationService: AuthorizationService) {
  }
  pageHasNoModel(): boolean{
    return (this.model === undefined || this.id ===undefined);
  }
  pageInUpdateMode(): boolean {
    let condition : boolean = (this.model === undefined || this.id ===undefined);
    condition = !condition;
    return condition;
  };
  submit(form: NgForm) {
    console.log(form)
    const bookModel : BookNewDto = {
      title: form.value.title,
      genre: form.value.genre,
      authorId: (form.value.authorId==0) ? null : form.value.authorId,
      publishYear: form.value.publishYear
    }
    if (this.pageHasNoModel()){
      this.booksService.createBook(bookModel);
    }
    else {
      if (this.id != undefined) {
        this.booksService.updateBook(this.id, bookModel)
      }
    }
  }
}
