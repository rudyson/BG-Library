export interface Book {
  id: number;
  title: string;
  publishYear: number;
  genre: string;
  author?: Author;
}

export interface BookShortDto {
  id: number;
  title: string;
  publishYear: number;
  genre: string;
  author?: AuthorShortDto;
}

export interface BookFullDto {
  id: number;
  title: string;
  publishYear: number;
  genre: string;
  author?: AuthorShortDto;
}

export interface Author {
  id: number;
  name: string;
  surname: string;
}

export interface AuthorShortDto {
  id: number;
  books: number;
  name: string;
  surname: string;
  birthday: string;
}

export interface AuthorFullDto {
  id: number;
  books: Array<BookInfoDto>;
  name: string;
  surname: string;
  birthday: string;
}

export interface BookInfoDto {
  title: string;
  publishYear: number;
  genre: string;
}

export interface BookNewDto {
  title: string;
  publishYear: number;
  genre: string;
  authorId: number | undefined;
}
