import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {GenericPaginationModel} from "../../special/genericPagination.model";
import { AuthorCreateRequest, AuthorFullInfoDto, AuthorShortInfoDto, AuthorUpdateRequest } from 'src/app/special/models/author.models';

@Injectable({
  providedIn: 'root'
})
export class AuthorsService {
  private contentApiUrl: string = environment.contentApiUrl;
  private baseRoute: string = '/api/author';
  constructor(private http: HttpClient) {}
  getAllAuthors(page?: number | null, size?: number | null) : Observable<GenericPaginationModel<AuthorFullInfoDto>>{
    let httpParams: HttpParams = new HttpParams()
      .append("page", page ?? 1)
      .append("size", size ?? 5);
    return this.http.get<GenericPaginationModel<AuthorFullInfoDto>>(this.contentApiUrl+this.baseRoute, {params: httpParams});
  }
  getAuthor(id: number) : Observable<AuthorFullInfoDto>{
    return this.http.get<AuthorFullInfoDto>(this.contentApiUrl+this.baseRoute+'/'+id.toString());
  }
  createAuthor(author: AuthorCreateRequest) : Observable<AuthorShortInfoDto>{
    return this.http.post<AuthorShortInfoDto>(this.contentApiUrl+this.baseRoute,author);
  }
  updateAuthor(id: number, author: AuthorUpdateRequest) : Observable<AuthorShortInfoDto> {
    return this.http.put<AuthorShortInfoDto>(this.contentApiUrl+this.baseRoute+'/'+id.toString(),author);
  }
  deleteAuthor(id: number) : Observable<boolean>{
    return this.http.delete<boolean>(this.contentApiUrl+this.baseRoute+"/"+id.toString())
  }
}
