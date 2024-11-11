import { PaymentMethod } from './../models/payment-method';
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

  getOrders(): Observable<any> {
    return this.http.get(this.config.getOrdersApiUrl());
  }

  getOrderDetailsById(orderId: string): Observable<any> {
    return this.http.get(this.config.getOrderDetailsApiUrl(orderId));
  }

  getPaymentMethods(): Observable<any> {
    return this.http.get(this.config.getPaymentMethodApiUrl());
  }

  payOrder(paymentMethod: PaymentMethod) {
    let request = {
      PaymentMethod: this.mapPaymentMethodToKey(paymentMethod),
    };
    return this.http.post(this.config.payOrderApiUrl(), request);
  }

  mapPaymentMethodToKey(paymentMethod: PaymentMethod): number {
    const map: { [key: string]: number } = {
      Bank: 0,
      'IBox terminal': 1,
      Visa: 2,
    };

    return map[paymentMethod.title];
  }
}
