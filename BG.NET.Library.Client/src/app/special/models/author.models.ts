import { BookShortInfoDto } from "../book.models";

export interface AuthorAutocompleteDto
{
    id : number;
    name: string;
    surname: string;
}
export interface AuthorFullInfoDto
{
    id : number;
    name: string;
    surname: string;
    birthday : string;
    books: Array<BookShortInfoDto>
}
export interface AuthorShortInfoDto
{
    id : number;
    name: string;
    surname: string;
    birthday : string;
}
export interface AuthorCreateRequest
{
    name: string;
    surname: string;
    birthday : string;
}
export interface AuthorUpdateRequest
{
    name: string | undefined;
    surname: string | undefined;
    birthday : string| undefined;
}