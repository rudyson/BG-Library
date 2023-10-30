import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Book, BookFullDto, BookShortDto} from "../../special/entities";
import {environment} from "../../../environments/environment";
import {Observable} from "rxjs";
import {GenericPaginationModel} from "../../special/genericPagination.model";

@Injectable({
  providedIn: 'root'
})
export class BooksService {
  public books?: Book[];
  private contentApiUrl: string = environment.contentApiUrl;
  private baseRoute: string = '/api/book';
  constructor(private http: HttpClient) {}

  getAllBooks(page?: number | null, size?: number | null) : Observable<GenericPaginationModel<BookFullDto>>{
    let httpParams: HttpParams = new HttpParams()
      .append("page", page ?? 1)
      .append("size", size ?? 5);
    return this.http.get<GenericPaginationModel<BookFullDto>>(this.contentApiUrl+this.baseRoute, {params: httpParams});
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

  deleteBook(id: number): Observable<void>{
    return this.http.delete<void>(this.contentApiUrl+this.baseRoute+'/'+id.toString());
  }
}
