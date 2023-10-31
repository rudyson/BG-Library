import {Component, OnInit} from '@angular/core';
import {BookFullDto} from '../../special/entities';
import {BooksService} from "../../services/books/books.service";
import {GenericPaginationModel} from "../../special/genericPagination.model";
import {ActivatedRoute, Router} from "@angular/router";
import {PageEvent} from "@angular/material/paginator";
import {MatDialog, MatDialogRef, MatDialogModule} from '@angular/material/dialog';

@Component({
  selector: 'app-book-list-v1',
  templateUrl: './book-list-v1.component.html',
  styleUrls: ['./book-list-v1.component.css']
})
export class BookListV1Component implements OnInit{
  public books?: GenericPaginationModel<BookFullDto>;
  constructor(
    private booksService: BooksService,
    private router: Router,
    private route: ActivatedRoute) {

  }
  ngOnInit(): void {
    this.handlePaginationEvent(undefined);
  }
  handlePaginationEvent($event?: PageEvent) {
    this.booksService.getAllBooks(
      $event?.pageIndex===undefined ? Number(this.route.snapshot.paramMap.get('id') ?? 1) : $event?.pageIndex + 1,
      $event?.pageSize
    )
      .subscribe({
        next: (books) => {
          this.books = undefined;
          this.books = books;
          window.history.replaceState({},'',`/books/${this.books.page}`)
        },
        error: (response) =>{
          this.router.navigate(["/books/1"]);
          this.handlePaginationEvent(undefined);
        }
      })
  }
  deleteBook(id: number){
    /*
    let dialogRef = this.dialog.open(BookListV1Component, {
      height: '200px',
      width: '200px',
    });*/

    if (confirm("Do you want to delete book with id "+id+"?")){
      this.booksService.deleteBook(id).subscribe({
        next: ()=>{
          this.handlePaginationEvent(undefined)
        },
        error:(response)=>{
          console.log("Deletion error")
          console.log(response)
        }
      })
    }
  }
}
