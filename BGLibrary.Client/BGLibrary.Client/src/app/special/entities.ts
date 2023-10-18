export interface Book {
  id: number;
  title: string;
  publishYear: number;
  genre: string;
  author?: Author;
}

export interface Author {
  id: number;
  name: string;
  surname: string;
}

export interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
