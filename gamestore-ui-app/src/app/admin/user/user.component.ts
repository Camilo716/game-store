import { Component, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { UserService } from '../../core/services/user.service';
import { User } from '../../core/models/user';
import { UserFormComponent } from './user-form/user-form.component';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [
    MatTableModule,
    MatCardModule,
    CommonModule,
    MatMenuModule,
    MatIconModule,
  ],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css',
})
export class UserComponent {
  title: string = 'Users';
  users: User[] = [];

  columns = [
    { matColumnDef: 'id', data: 'id', text: 'Id' },
    { matColumnDef: 'userName', data: 'userName', text: 'User Name' },
  ];

  displayedColumns: string[] = [];

  readonly formDialog = inject(MatDialog);

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.setDisplayedColumns();
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getAllUsers().subscribe((data: User[]) => {
      this.users = data;
    });
  }

  deleteUser(id: string): void {
    this.userService.deleteUser(id).subscribe(() => {
      this.loadUsers();
    });
  }

  openEditUserDialog(User: User): void {
    const dialogRef = this.formDialog.open(UserFormComponent, {
      data: {
        user: User,
        isEdit: true,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadUsers();
    });
  }

  openAddUserDialog(): void {
    const dialogRef = this.formDialog.open(UserFormComponent, {
      data: {
        isEdit: false,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadUsers();
    });
  }

  setDisplayedColumns() {
    this.displayedColumns = this.columns.map((column) => column.matColumnDef);
    this.displayedColumns.push('actions');
  }
}
