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
import { Observable } from 'rxjs';
import { Role } from '../../../core/models/role';
import { RoleFormData } from './role-form-data';
import { RoleService } from '../../../core/services/role.service';
import { Permission } from '../../../core/models/permission';

@Component({
  selector: 'app-role-form',
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
  templateUrl: './role-form.component.html',
  styleUrl: './role-form.component.css',
})
export class RoleFormComponent implements OnInit {
  readonly dialogRef = inject(MatDialogRef<RoleFormComponent>);
  readonly roleFormData = inject<RoleFormData>(MAT_DIALOG_DATA);

  role: Role;

  isEdit: boolean;

  allPermissions: Permission[] = [];

  form: FormGroup = this.formBuilder.group({
    id: [null],
    name: [null, Validators.required],
    permissions: [[], Validators.required],
  });

  constructor(
    private roleService: RoleService,
    private formBuilder: FormBuilder
  ) {
    this.isEdit = this.roleFormData.isEdit;
    this.role = this.roleFormData.role;
    this.setAllPermissions();
  }

  ngOnInit(): void {
    this.setForm();
  }

  setForm() {
    this.form.patchValue(this.role);
    this.setPermissionsOfRole();
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.isEdit) {
      this.updateRole();
    } else {
      this.createRole();
    }
  }

  createRole() {
    const Role: Role = {
      ...this.role,
      ...this.form.value,
    };

    this.roleService
      .addRole(Role, this.form.get('permissions')?.value)
      .subscribe(() => this.dialogRef.close());
  }

  private updateRole() {
    const updatedRole: Role = {
      ...this.role,
      ...this.form.value,
    };

    this.roleService
      .updateRole(updatedRole, this.form.get('permissions')?.value)
      .subscribe(() => this.dialogRef.close());
  }

  setAllPermissions(): void {
    this.roleService.getAllPermissions().subscribe((permissions) => {
      this.allPermissions = permissions;
    });
  }

  setPermissionsOfRole() {
    this.roleService
      .getRolePermissions(this.role.id)
      .subscribe((roles: Role[]) =>
        this.form.get('permissions')?.setValue(roles.map((r) => r.id))
      );
  }

  get password(): AbstractControl | null {
    return this.form.get('password');
  }
}
