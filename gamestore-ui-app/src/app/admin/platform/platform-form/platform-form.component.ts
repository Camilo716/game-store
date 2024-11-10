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
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { Platform } from '../../../core/models/platform';
import { PlatformService } from '../../../core/services/platform.service';
import { PlatformFormData } from './platformFormData';

@Component({
  selector: 'app-platform-form',
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
  templateUrl: './platform-form.component.html',
  styleUrl: './platform-form.component.css',
})
export class PlatformFormComponent implements OnInit {
  readonly dialogRef = inject(MatDialogRef<PlatformFormComponent>);
  readonly platformFormData = inject<PlatformFormData>(MAT_DIALOG_DATA);
  platform: Platform;

  isEdit: boolean;

  allPlatforms: Platform[] = [];

  form: FormGroup = this.formBuilder.group({
    id: [null],
    type: [null, Validators.required],
  });

  constructor(
    private platformService: PlatformService,
    private formBuilder: FormBuilder
  ) {
    this.isEdit = this.platformFormData.isEdit;
    this.platform = this.platformFormData.platform;
  }

  ngOnInit(): void {
    this.setForm();
    this.setAllPlatforms();
  }

  setForm() {
    this.form.patchValue(this.platform);
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.isEdit) {
      this.updatePlatform();
    } else {
      this.createPlatform();
    }
  }

  createPlatform() {
    const platform: Platform = {
      ...this.platform,
      ...this.form.value,
    };

    this.platformService
      .addPlatform(platform)
      .subscribe(() => this.dialogRef.close());
  }

  private updatePlatform() {
    const updatedPlatform: Platform = {
      ...this.platform,
      ...this.form.value,
    };

    this.platformService
      .updatePlatform(updatedPlatform)
      .subscribe(() => this.dialogRef.close());
  }

  setAllPlatforms(): void {
    this.platformService
      .getAllPlatforms()
      .subscribe((platforms) => (this.allPlatforms = platforms));
  }
}
