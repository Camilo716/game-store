import { Component, inject } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
} from '@angular/material/dialog';
import { BanFormData } from './ban-form-data';
import { UserBanDuration } from '../../../core/models/user-ban-duration';
import { User } from '../../../core/models/user';
import { UserService } from '../../../core/services/user.service';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DialogModule } from '@angular/cdk/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-ban-form',
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
  templateUrl: './ban-form.component.html',
  styleUrl: './ban-form.component.css',
})
export class BanFormComponent {
  readonly dialogRef = inject(MatDialogRef<BanFormComponent>);
  readonly banFormData = inject<BanFormData>(MAT_DIALOG_DATA);

  user: User;

  banDurations: UserBanDuration[] = [];

  form: FormGroup = this.formBuilder.group({
    description: [null, Validators.required],
  });

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder
  ) {
    this.user = this.banFormData.user;
    this.loadBanDurations();
  }

  loadBanDurations() {
    this.userService
      .getUserBanDurations()
      .subscribe((durations: UserBanDuration[]) => {
        this.banDurations = durations;
      });
  }

  onConfirm(): void {
    let description = this.form.get('description')?.value;
    let duration = this.banDurations.find((d) => d.description == description);

    if (!duration) {
      throw Error('Inconsistency found in selected duration.');
    }

    this.userService
      .banUser(duration, this.user.username)
      .subscribe(() => this.dialogRef.close());
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
