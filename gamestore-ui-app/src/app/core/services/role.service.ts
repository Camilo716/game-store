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
    return this.http.get(this.config.getRolesApiUrl());
  }

  getRolePermissions(roleId: string): Observable<any> {
    return this.http.get(this.config.getRolePermissionsApiUrl(roleId));
  }

  getAllPermissions(): Observable<any> {
    return this.http.get(this.config.getPermissionsApiUrl());
  }

  updateRole(roleData: Role, permissionsIds: string[]): Observable<any> {
    let request = {
      role: roleData,
      permissions: permissionsIds,
    };

    return this.http.put(`${this.config.updateRoleApiUrl()}`, request);
  }

  addRole(roleData: Role, permissionsIds: string[]): Observable<any> {
    roleData.id = roleData.id != null ? roleData.id : roleData.name;

    let request = {
      role: roleData,
      permissions: permissionsIds,
    };

    return this.http.post(`${this.config.addRoleApiUrl()}`, request);
  }

  deleteRole(id: string): Observable<any> {
    return this.http.delete(this.config.deleteRoleApiUrl(id));
  }
}
