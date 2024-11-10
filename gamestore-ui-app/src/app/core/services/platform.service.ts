import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';
import { Observable } from 'rxjs';
import { Platform } from '../models/platform';

@Injectable({
  providedIn: 'root',
})
export class PlatformService {
  constructor(private http: HttpClient, private config: ConfigService) {}

  getByGameKey(key: string): Observable<any> {
    return this.http.get(this.config.getPlatformsByGameKeyApiUrl(key));
  }

  getAllPlatforms(): Observable<any> {
    return this.http.get(this.config.getPlatformsApiUrl());
  }

  updatePlatform(PlatformData: Platform): Observable<any> {
    let request = {
      Platform: PlatformData,
    };
    return this.http.put(`${this.config.updatePlatformApiUrl()}`, request);
  }

  addPlatform(PlatformData: Platform): Observable<any> {
    let request = {
      Platform: PlatformData,
    };
    return this.http.post(`${this.config.addPlatformApiUrl()}`, request);
  }

  deletePlatform(id: string): Observable<any> {
    return this.http.delete(this.config.deletePlatformApiUrl(id));
  }
}
