import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient, private config: ConfigService) {}

  getCart(): Observable<any> {
    return this.http.get(this.config.getCartApiUrl());
  }

  getOrderDetailsById(orderId: string): Observable<any> {
    return this.http.get(this.config.getOrderDetailsApiUrl(orderId));
  }
}
