import { HttpClient } from '@angular/common/http';
import { ConfigService } from './config.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Genre } from '../models/genre';

@Injectable({
  providedIn: 'root',
})
export class GenreService {
  constructor(private http: HttpClient, private config: ConfigService) {}

  getAllGenres(): Observable<any> {
    return this.http.get(this.config.getGenresApiUrl());
  }

  getByGameKey(key: string): Observable<any> {
    return this.http.get(this.config.getGenresByGameKeyApiUrl(key));
  }

  updateGenre(genreData: Genre): Observable<any> {
    let request = {
      Genre: genreData,
    };
    return this.http.put(`${this.config.updateGenreApiUrl()}`, request);
  }

  addGenre(genreData: Genre): Observable<any> {
    let request = {
      Genre: genreData,
    };
    return this.http.post(`${this.config.addGenreApiUrl()}`, request);
  }

  deleteGenre(id: string): Observable<any> {
    return this.http.delete(this.config.deleteGenreApiUrl(id));
  }
}
