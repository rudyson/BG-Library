export interface ResponseWrapper<T>{
    requestId: string,
    requestedAt: string,
    status: number,
    hasData: boolean,
    hasErrors: boolean,
    data: T | undefined,
    errors: Array<string> | undefined
}