import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ConfigService } from './config.service';
import { jwtDecode, JwtPayload } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenSubject = new BehaviorSubject<string | null>(null);

  constructor(private http: HttpClient, private config: ConfigService) {
    const savedToken = localStorage.getItem('authToken');
    if (savedToken) {
      this.tokenSubject.next(savedToken);
    }
  }

  get token(): Observable<string | null> {
    return this.tokenSubject.asObservable();
  }

  login(login: string, password: string): Observable<{ token: string }> {
    const payload = {
      login,
      password,
      internalAuth: true,
    };

    return this.http
      .post<{ token: string }>(this.config.loginApiUrl(), payload)
      .pipe(
        tap((response) => {
          localStorage.setItem('authToken', response.token);
          this.tokenSubject.next(response.token);
        })
      );
  }

  logout(): void {
    localStorage.removeItem('authToken');
    this.tokenSubject.next(null);
  }

  getDecodedToken(): any | null {
    const token = this.tokenSubject.value;
    if (token) {
      return jwtDecode<JwtPayload & { [key: string]: any }>(token);
    }
    return null;
  }

  getUsername(): string | null {
    const decodedToken = this.getDecodedToken();
    return decodedToken ? decodedToken['nameid'] : null;
  }
}
