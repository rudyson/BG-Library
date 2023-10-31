import {Component, Input, OnInit} from '@angular/core';
import {BookNewDto} from "../../special/entities";
import {Router, ActivatedRoute} from "@angular/router";
import {BooksService} from "../../services/books/books.service";
import {NgForm} from "@angular/forms";
import {AuthorizationService} from "../../services/authorization/authorization.service";

@Component({
  selector: 'app-book-page',
  templateUrl: './book-page.component.html',
  styleUrls: ['./book-page.component.css']
})
export class BookPageComponent implements OnInit{
  //@Input() model: BookNewDto | undefined = undefined;
  //@Input() id: number | undefined = undefined;
  public id: number | undefined = undefined;
  public model: BookNewDto | undefined = undefined;

  constructor(private router: Router, private route: ActivatedRoute, private booksService: BooksService, public authorizationService: AuthorizationService) {
  }
  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id') ?? undefined);
    if (this.id===undefined){
      return;
    }
    this.booksService.getBook(this.id).subscribe({
      next: (book) => {
        let tempModel: BookNewDto = {
          authorId: book.author?.id,
          title: book.title,
          genre: book.genre,
          publishYear: book.publishYear
        }
        this.model = tempModel;
      },
      error: (response) =>{
        this.model = undefined;
        this.id = undefined;
        this.router.navigate(["/book"]);
      }
    })
  }
  /*
  pageHasNoModel(): boolean{
    return (this.model === undefined || this.id ===undefined);
  }
  pageInUpdateMode(): boolean {
    let condition : boolean = (this.model === undefined || this.id ===undefined);
    condition = !condition;
    return condition;
  };*/
  pageHasNoModel(): boolean{
    return (this.id ===undefined);
  }

  submit(form: NgForm) {
    console.log(form)
    const bookModel : BookNewDto = {
      title: form.value.title,
      genre: form.value.genre,
      authorId: (form.value.authorId==0) ? null : form.value.authorId,
      publishYear: form.value.publishYear
    }
    if (this.pageHasNoModel()){
      console.log("Create condition");
      this.booksService.createBook(bookModel);
    }
    else {
      if (this.id != undefined) {
        console.log("Update condition");
        this.booksService.updateBook(this.id, bookModel)
      }
      else{
        console.log("Return condition");
      }
    }
  }
}
