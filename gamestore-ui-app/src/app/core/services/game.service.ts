import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfigService } from './config.service';
import { Game } from '../models/game';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  constructor(private http: HttpClient, private config: ConfigService) {}

  getAllGames(): Observable<any> {
    return this.http.get(this.config.getGamesApiUrl());
  }

  getGamesByPublisherId(publisherId: string): Observable<any> {
    return this.http.get(
      `${this.config.getGamesByPublisherIdApiUrl(publisherId)}`
    );
  }

  getGamesByGenreId(genreId: string): Observable<any> {
    return this.http.get(`${this.config.getGamesByGenreIdApiUrl(genreId)}`);
  }

  getGamesByPlatformId(platformId: string): Observable<any> {
    return this.http.get(
      `${this.config.getGamesByPlatformIdApiUrl(platformId)}`
    );
  }

  updateGame(
    gameData: Game,
    genresIds: string[],
    platformsIds: string[],
    publisherId: string
  ): Observable<any> {
    let request = {
      game: gameData,
      genres: genresIds || [],
      platforms: platformsIds || [],
      publisher: publisherId,
    };
    return this.http.put(`${this.config.updateGameApiUrl()}`, request);
  }

  addGame(
    gameData: Game,
    genresIds: string[],
    platformsIds: string[],
    publisherId: string
  ): Observable<any> {
    let request = {
      game: gameData,
      genres: genresIds || [],
      platforms: platformsIds || [],
      publisher: publisherId,
    };
    return this.http.post(`${this.config.addGameApiUrl()}`, request);
  }

  deleteGame(id: string): Observable<any> {
    return this.http.delete(this.config.deleteGameApiUrl(id));
  }
}
