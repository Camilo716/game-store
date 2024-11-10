import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';
import { Observable } from 'rxjs';
import { Publisher } from '../models/publisher';

@Injectable({
  providedIn: 'root',
})
export class PublisherService {
  constructor(private http: HttpClient, private config: ConfigService) {}

  getByGameKey(key: string): Observable<any> {
    return this.http.get(this.config.getPublishersByGameKeyApiUrl(key));
  }

  getAllPublishers(): Observable<any> {
    return this.http.get(this.config.getPublishersApiUrl());
  }

  updatePublisher(publisherData: Publisher): Observable<any> {
    let request = {
      Publisher: publisherData,
    };
    return this.http.put(`${this.config.updatePublisherApiUrl()}`, request);
  }

  addPublisher(publisherData: Publisher): Observable<any> {
    let request = {
      publisher: publisherData,
    };
    return this.http.post(`${this.config.addPublisherApiUrl()}`, request);
  }

  deletePublisher(id: string): Observable<any> {
    return this.http.delete(this.config.deletePublisherApiUrl(id));
  }
}
