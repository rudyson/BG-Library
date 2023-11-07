import { AuthorShortInfoDto } from "./author.models";

export interface BookFullInfoDto {
    id: number;
    title: string;
    publishYear: number;
    genre: string;
    author: AuthorShortInfoDto | undefined;
}

export interface BookShortInfoDto {
    title: string;
    publishYear: number;
    genre: string;
}

export interface BookCreateRequest {
    title: string;
    publishYear: number;
    genre: string;
    authorId: number;
}

export interface BookUpdateRequest {
    title?: string;
    publishYear?: number;
    genre?: string;
    authorId?: number;
}
