import { Component, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { RoleService } from '../../core/services/role.service';
import { Role } from '../../core/models/role';
import { RoleFormComponent } from './role-form/role-form.component';

@Component({
  selector: 'app-role',
  standalone: true,
  imports: [
    MatTableModule,
    MatCardModule,
    CommonModule,
    MatMenuModule,
    MatIconModule,
  ],
  templateUrl: './role.component.html',
  styleUrl: './role.component.css',
})
export class RoleComponent {
  title: string = 'Roles';
  roles: Role[] = [];

  columns = [
    { matColumnDef: 'id', data: 'id', text: 'Id' },
    { matColumnDef: 'name', data: 'name', text: 'Name' },
  ];

  displayedColumns: string[] = [];

  readonly formDialog = inject(MatDialog);

  constructor(private roleService: RoleService) {}

  ngOnInit(): void {
    this.setDisplayedColumns();
    this.loadRoles();
  }

  loadRoles(): void {
    this.roleService.getAllRoles().subscribe((data: Role[]) => {
      this.roles = data;
    });
  }

  deleteRole(id: string): void {
    this.roleService.deleteRole(id).subscribe(() => {
      this.loadRoles();
    });
  }

  openEditRoleDialog(role: Role): void {
    const dialogRef = this.formDialog.open(RoleFormComponent, {
      data: {
        role: role,
        isEdit: true,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadRoles();
    });
  }

  openAddRoleDialog(): void {
    const dialogRef = this.formDialog.open(RoleFormComponent, {
      data: {
        isEdit: false,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadRoles();
    });
  }

  setDisplayedColumns() {
    this.displayedColumns = this.columns.map((column) => column.matColumnDef);
    this.displayedColumns.push('actions');
  }
}
