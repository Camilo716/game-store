import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';
import { Observable } from 'rxjs';
import { Role } from '../models/role';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  constructor(private http: HttpClient, private config: ConfigService) {}

  getAllRoles(): Observable<any> {
    let rolesApiUrl = this.config.getRolesApiUrl();
    console.log(rolesApiUrl);
    return this.http.get(rolesApiUrl);
  }

  updateRole(roleData: Role, permissionsIds: string[]): Observable<any> {
    let request = {
      Role: roleData,
      permissions: permissionsIds,
    };

    return this.http.put(`${this.config.updateRoleApiUrl()}`, request);
  }

  addRole(roleData: Role, permissionsIds: string[]): Observable<any> {
    let request = {
      Role: roleData,
      permissions: permissionsIds,
    };
    return this.http.post(`${this.config.addRoleApiUrl()}`, request);
  }

  deleteRole(id: string): Observable<any> {
    return this.http.delete(this.config.deleteRoleApiUrl(id));
  }
}
