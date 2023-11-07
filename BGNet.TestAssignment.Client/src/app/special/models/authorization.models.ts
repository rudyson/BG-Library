export interface LoginDto {
    username: string;
    password: string;
}

export interface RegisterDto {
    username: string;
    password: string;
    name: string;
    surname: string;
    birthday: string;
    address: string;
}

export interface UserInfoDto {
    id: number;
    username: string;
    name: string;
    surname: string;
    birthday: string;
    address: string;
}

export interface JwtTokenDto {
    token: string;
    expiresAt: string;
    createdAt: string;
}
export interface JwtToken {
    "aud": string;
    "iss": string;

    "exp": number;

    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": string;
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": string;
}
/*
export interface RegisterRequest {
    username: string,
    password: string,
    name: string,
    surname: string,
    birthday: string,
    address: string
  }


export interface LoginRequest {
  username: string;
  password: string;
}

export interface UserInfoDto {
  id: number;
  username: string;
  name: string;
  surname: string;
  birthday: string;
  address: string;
}

export interface TokenCreatedDto {
  token: string;
  expires: string;
}
export interface JwtToken {
  aud: string;
  iss: string;
  exp: number;

  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name': string;
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier': string;
}

* */
