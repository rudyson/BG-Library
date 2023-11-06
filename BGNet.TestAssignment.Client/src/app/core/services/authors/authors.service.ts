import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import { AuthorAutocompleteDto, AuthorCreateRequest, AuthorFullInfoDto, AuthorShortInfoDto, AuthorUpdateRequest } from 'src/app/special/models/author.models';
import {GenericPaginationModel, ResponseWrapper} from 'src/app/special/models/request.models';
import {environment} from "../../../shared/environment";

@Injectable({
  providedIn: 'root'
})
export class AuthorsService {
  private contentApiUrl: string = environment.contentApiUrl;
  private baseRoute: string = 'author';
  constructor(private http: HttpClient) {}
  getAllAuthors(skip?: number | null, take?: number | null) : Observable<ResponseWrapper<GenericPaginationModel<AuthorFullInfoDto>>>{
    let httpParams: HttpParams = new HttpParams()
      .append("skip", skip ?? 0)
      .append("take", take ?? 5);
      //.append("page", page ?? 1)
      //.append("size", size ?? 5);
    return this.http.get<ResponseWrapper<GenericPaginationModel<AuthorFullInfoDto>>>(this.contentApiUrl+this.baseRoute, {params: httpParams});
  }
  getAuthor(id: number) : Observable<ResponseWrapper<AuthorFullInfoDto>>{
    return this.http.get<ResponseWrapper<AuthorFullInfoDto>>(this.contentApiUrl+this.baseRoute+'/'+id.toString());
  }
  createAuthor(author: AuthorCreateRequest) : Observable<ResponseWrapper<AuthorShortInfoDto>>{
    return this.http.post<ResponseWrapper<AuthorShortInfoDto>>(this.contentApiUrl+this.baseRoute,author);
  }
  updateAuthor(id: number, author: AuthorUpdateRequest) : Observable<ResponseWrapper<AuthorShortInfoDto>> {
    return this.http.put<ResponseWrapper<AuthorShortInfoDto>>(this.contentApiUrl+this.baseRoute+'/'+id.toString(),author);
  }
  deleteAuthor(id: number) : Observable<ResponseWrapper<AuthorShortInfoDto>>{
    return this.http.delete<ResponseWrapper<AuthorShortInfoDto>>(this.contentApiUrl+this.baseRoute+"/"+id.toString())
  }
  searchAuthor(query: string) : Observable<ResponseWrapper<Array<AuthorAutocompleteDto>>>{
    return this.http.get<ResponseWrapper<Array<AuthorAutocompleteDto>>>(this.contentApiUrl+this.baseRoute+'/search?query='+query);
  }
}
