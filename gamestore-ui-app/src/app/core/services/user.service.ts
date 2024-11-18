import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';
import { Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient, private config: ConfigService) {}

  getAllUsers(): Observable<any> {
    return this.http.get(this.config.getUsersApiUrl());
  }

  updateUser(
    userData: User,
    password: string,
    rolesIds: string[]
  ): Observable<any> {
    let request = {
      User: userData,
      Roles: rolesIds,
      Password: password,
    };
    return this.http.put(`${this.config.updateUserApiUrl()}`, request);
  }

  addUser(
    userData: User,
    password: string,
    rolesIds: string[]
  ): Observable<any> {
    userData.id = userData.id != null ? userData.id : userData.username;

    let request = {
      User: userData,
      Roles: rolesIds,
      Password: password,
    };
    return this.http.post(`${this.config.addUserApiUrl()}`, request);
  }

  deleteUser(id: string): Observable<any> {
    return this.http.delete(this.config.deleteUserApiUrl(id));
  }

  getUserRoles(id: string): Observable<any> {
    return this.http.get(this.config.getUserRolesApiUrl(id));
  }
}
