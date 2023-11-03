import {Component, OnInit} from '@angular/core';
import {BooksService} from "../../services/books/books.service";
import {GenericPaginationModel} from "../../special/genericPagination.model";
import {ActivatedRoute, Router} from "@angular/router";
import {PageEvent} from "@angular/material/paginator";
import { BookFullInfoDto } from 'src/app/special/book.models';

@Component({
  selector: 'app-book-list-v1',
  templateUrl: './book-list-v1.component.html',
  styleUrls: ['./book-list-v1.component.css']
})
export class BookListV1Component implements OnInit{
  public books?: GenericPaginationModel<BookFullInfoDto>;
  public unableLoad: boolean = false;
  constructor(
    private booksService: BooksService,
    private router: Router,
    private route: ActivatedRoute) {

  }
  ngOnInit(): void {
    this.handlePaginationEvent(undefined);
  }

  handlePaginationEvent($event?: PageEvent) {
    //$event?.pageIndex
    // Number(this.route.snapshot.paramMap.get('id') ?? 1)
    let pageNum: number = 1;
    if ($event?.pageIndex===undefined){
      if (this.route.snapshot.paramMap.get('id')===undefined){
        pageNum = 1;
      }
      else{
        pageNum = Number(this.route.snapshot.paramMap.get('id') ?? 1)
      }
    }
    else {
      pageNum = $event?.pageIndex + 1;
    }
    this.booksService.getAllBooks(
      pageNum,
      $event?.pageSize
    )
      .subscribe({
        next: (books) => {
          this.unableLoad = false;
          this.books = books;
          console.log(books)
          window.history.replaceState({},'',`/books/${this.books.page}`)
        },
        error: (response) =>{
          if (response.status==404){
            this.unableLoad = true;
          }
          console.log(response)
          //this.router.navigate(["/books/1"]);
          //this.handlePaginationEvent(undefined);
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
