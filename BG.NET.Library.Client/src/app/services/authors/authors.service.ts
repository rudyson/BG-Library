import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {GenericPaginationModel} from "../../special/genericPagination.model";
import {AuthorDtoUpdate, AuthorFullDto, AuthorShortDto, BookFullDto} from "../../special/entities";

@Injectable({
  providedIn: 'root'
})
export class AuthorsService {
  private contentApiUrl: string = environment.contentApiUrl;
  private baseRoute: string = '/api/author';
  constructor(private http: HttpClient) {}
  getAllAuthors(page?: number | null, size?: number | null) : Observable<GenericPaginationModel<AuthorFullDto>>{
    let httpParams: HttpParams = new HttpParams()
      .append("page", page ?? 1)
      .append("size", size ?? 5);
    return this.http.get<GenericPaginationModel<AuthorFullDto>>(this.contentApiUrl+this.baseRoute, {params: httpParams});
  }
  getAuthor(id: number) : Observable<AuthorFullDto>{
    return this.http.get<AuthorFullDto>(this.contentApiUrl+this.baseRoute+'/'+id.toString());
  }
  createAuthor(author: AuthorDtoUpdate) : Observable<AuthorDtoUpdate>{
    return this.http.post<AuthorDtoUpdate>(this.contentApiUrl+this.baseRoute,author);
  }
  updateAuthor(id: number, author: AuthorDtoUpdate) : Observable<AuthorDtoUpdate> {
    return this.http.put<AuthorDtoUpdate>(this.contentApiUrl+this.baseRoute+'/'+id.toString(),author);
  }
  deleteAuthor(id: number) : Observable<void>{
    return this.http.delete<void>(this.contentApiUrl+this.baseRoute+"/"+id.toString())
  }
}
