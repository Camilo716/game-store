import { Component, inject, OnInit } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { UserFormData } from './user-form-data';
import { User } from '../../../core/models/user';
import { UserService } from '../../../core/services/user.service';
import { Role } from '../../../core/models/role';
import { RoleService } from '../../../core/services/role.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatDialogContent,
    MatDialogActions,
    MatInputModule,
    ReactiveFormsModule,
    MatOptionModule,
    MatSelectModule,
    CommonModule,
  ],
  templateUrl: './user-form.component.html',
  styleUrl: './user-form.component.css',
})
export class UserFormComponent implements OnInit {
  readonly dialogRef = inject(MatDialogRef<UserFormComponent>);
  readonly UserFormData = inject<UserFormData>(MAT_DIALOG_DATA);
  user: User;

  isEdit: boolean;

  allRoles: Role[] = [];

  form: FormGroup = this.formBuilder.group({
    id: [null],
    userName: [null, Validators.required],
    password: [null, Validators.required],
    roles: [[], Validators.required],
  });

  constructor(
    private userService: UserService,
    private roleService: RoleService,
    private formBuilder: FormBuilder
  ) {
    this.isEdit = this.UserFormData.isEdit;
    this.user = this.UserFormData.user;
    this.setAllRoles();
  }

  ngOnInit(): void {
    this.setForm();
  }

  setForm() {
    this.form.patchValue(this.user);
    this.setRolesOfGame();
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.isEdit) {
      this.updateUser();
    } else {
      this.createUser();
    }
  }

  createUser() {
    const user: User = {
      ...this.user,
      ...this.form.value,
    };

    this.userService
      .addUser(user, this.password?.value, this.form.get('roles')?.value)
      .subscribe(() => this.dialogRef.close());
  }

  private updateUser() {
    const updatedUser: User = {
      ...this.user,
      ...this.form.value,
    };

    this.userService
      .updateUser(
        updatedUser,
        this.password?.value,
        this.form.get('roles')?.value
      )
      .subscribe(() => this.dialogRef.close());
  }

  setAllRoles(): void {
    this.roleService.getAllRoles().subscribe((roles) => {
      this.allRoles = roles;
    });
  }

  setRolesOfGame() {
    this.userService
      .getUserRoles(this.user.id)
      .subscribe((roles: Role[]) =>
        this.form.get('roles')?.setValue(roles.map((r) => r.id))
      );
  }

  get password(): AbstractControl | null {
    return this.form.get('password');
  }
}
