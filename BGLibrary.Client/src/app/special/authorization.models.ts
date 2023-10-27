export interface LoginDto {
  username: string,
  password: string
}

export interface RegisterDto {
  username: string,
  password: string,
  name: string,
  surname: string,
  birthday: string,
  address: string
}

export interface UserInfoDto {
  id: number,
  username: string,
  password: string,
  name: string,
  surname: string,
  birthday: string,
  address: string
}
