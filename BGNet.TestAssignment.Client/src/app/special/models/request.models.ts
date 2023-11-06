export interface ResponseWrapper<T>{
    requestId: string,
    requestedAt: string,
    status: number,
    hasData: boolean,
    hasErrors: boolean,
    data: T | undefined,
    errors: Array<string> | undefined
}
export interface GenericPaginationModel<T> {
  page: number;
  pageSize: number;
  totalSize: number;
  pages: number;
  numberSkipped: number;
  nextPage: number | null;
  previousPage: number | null;
  firstPage: number;
  lastPage: number;
  onFirstPage: Boolean;
  onLastPage: Boolean;
  hasNextPage: Boolean;
  hasPreviousPage: Boolean;
  entities: Array<T>;
}
