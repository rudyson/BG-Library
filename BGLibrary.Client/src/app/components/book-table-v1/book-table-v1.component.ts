import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Book } from '../../special/entities';

@Component({
  selector: 'app-book-table-v1',
  templateUrl: './book-table-v1.component.html',
  styleUrls: ['./book-table-v1.component.css']
})
export class BookTableV1Component {
  public books?: Book[];

  constructor(http: HttpClient) {
    http.get<Book[]>('https://localhost:44330/api/book').subscribe(result => {
      this.books = result;
    }, error => console.error(error));
  }
}
