import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {Observable} from "rxjs";
import {GenericPaginationModel} from "../../special/genericPagination.model";
import { BookCreateRequest, BookFullInfoDto, BookUpdateRequest } from 'src/app/special/book.models';
import { AuthorShortInfoDto } from 'src/app/special/models/author.models';

@Injectable({
  providedIn: 'root'
})
export class BooksService {
  private contentApiUrl: string = environment.contentApiUrl;
  private baseRoute: string = '/api/book';
  constructor(private http: HttpClient) {}

  getAllBooks(page?: number | null, size?: number | null) : Observable<GenericPaginationModel<BookFullInfoDto>>{
    let httpParams: HttpParams = new HttpParams();
    if (page==null){
        httpParams.append("page", 1)
    }
    else {
      httpParams.
      append("page", page)
    }
    if (size==null){
      httpParams.append("size", 1)
  }
  else {
    httpParams.append("size", size)
  }
    return this.http.get<GenericPaginationModel<BookFullInfoDto>>(this.contentApiUrl+this.baseRoute, {params: httpParams});
  }
  getBook(id: number) : Observable<BookFullInfoDto>{
    return this.http.get<BookFullInfoDto>(this.contentApiUrl+this.baseRoute+'/'+id.toString());
  }
  createBook(book: BookCreateRequest) : Observable<AuthorShortInfoDto>{
    return this.http.post<AuthorShortInfoDto>(this.contentApiUrl+this.baseRoute,book);
  }
  updateBook(id: number, book: BookUpdateRequest) : Observable<AuthorShortInfoDto> {
    return this.http.put<AuthorShortInfoDto>(this.contentApiUrl+this.baseRoute+'/'+id.toString(),book);
  }

  deleteBook(id: number): Observable<boolean>{
    return this.http.delete<boolean>(this.contentApiUrl+this.baseRoute+'/'+id.toString());
  }
}
