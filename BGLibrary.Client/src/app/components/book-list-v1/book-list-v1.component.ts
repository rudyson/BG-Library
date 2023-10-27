import {Component, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Book } from '../../special/entities';
import {BooksService} from "../../services/books.service";

@Component({
  selector: 'app-book-list-v1',
  templateUrl: './book-list-v1.component.html',
  styleUrls: ['./book-list-v1.component.css']
})
export class BookListV1Component implements OnInit{
  public books?: Book[];

  constructor(private booksService: BooksService) {

  }

  ngOnInit(): void {
    this.reloadBooks();
  }
  reloadBooks():void{
    this.books = undefined;
    this.booksService.getAllBooks()
      .subscribe({
        next: (books) => {
          this.books = books;
        },
        error: (response) =>
          console.log(response)
      })
  }
}
