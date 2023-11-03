import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {Observable} from "rxjs";
import {GenericPaginationModel} from "../../special/genericPagination.model";
import { BookCreateRequest, BookFullInfoDto, BookShortInfoDto, BookUpdateRequest } from 'src/app/special/models/book.models';
import { AuthorShortInfoDto } from 'src/app/special/models/author.models';
import { ResponseWrapper } from 'src/app/special/models/request.models';

@Injectable({
  providedIn: 'root'
})
export class BooksService {
  private contentApiUrl: string = environment.contentApiUrl;
  private baseRoute: string = 'book';
  constructor(private http: HttpClient) {}

  getAllBooks(page?: number | null, size?: number | null) : Observable<ResponseWrapper<GenericPaginationModel<BookFullInfoDto>>>{
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
    return this.http.get<ResponseWrapper<GenericPaginationModel<BookFullInfoDto>>>(this.contentApiUrl+this.baseRoute, {params: httpParams});
  }
  getBook(id: number) : Observable<ResponseWrapper<BookFullInfoDto>>{
    return this.http.get<ResponseWrapper<BookFullInfoDto>>(this.contentApiUrl+this.baseRoute+'/'+id.toString());
  }
  createBook(book: BookCreateRequest) : Observable<ResponseWrapper<BookShortInfoDto>>{
    return this.http.post<ResponseWrapper<BookShortInfoDto>>(this.contentApiUrl+this.baseRoute,book);
  }
  updateBook(id: number, book: BookUpdateRequest) : Observable<ResponseWrapper<BookShortInfoDto>> {
    return this.http.put<ResponseWrapper<BookShortInfoDto>>(this.contentApiUrl+this.baseRoute+'/'+id.toString(),book);
  }
  deleteBook(id: number): Observable<ResponseWrapper<BookFullInfoDto>>{
    return this.http.delete<ResponseWrapper<BookFullInfoDto>>(this.contentApiUrl+this.baseRoute+'/'+id.toString());
  }
}
