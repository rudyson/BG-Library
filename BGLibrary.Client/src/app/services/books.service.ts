import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Book} from "../special/entities";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class BooksService {
  public books?: Book[];
  private contentApiUrl: string = environment.contentApiUrl;
  private baseRoute: string = '/api/book';
  constructor(private http: HttpClient) {}

  getAllBooks() : Observable<Book[]>{
    return this.http.get<Book[]>(this.baseRoute);
  }
  getBook(id: number) : Observable<Book>{
    return this.http.get<Book>(this.contentApiUrl+this.baseRoute+'/'+id.toString());
  }
  createBook(book: Book){
    this.http.post(this.contentApiUrl+this.baseRoute,book);
  }
  updateBook(id: number, book: Book){
    this.http.post(this.contentApiUrl+this.baseRoute+'/'+id.toString(),book);
  }

  deleteBook(id: number){
    this.http.delete(this.contentApiUrl+this.baseRoute+'/'+id.toString());
  }
}
