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
import { PublisherFormData } from './publisherFormData';
import { Publisher } from '../../../core/models/publisher';
import { PublisherService } from '../../../core/services/publisher.service';

@Component({
  selector: 'app-publisher-form',
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
  templateUrl: './publisher-form.component.html',
  styleUrl: './publisher-form.component.css',
})
export class PublisherFormComponent implements OnInit {
  readonly dialogRef = inject(MatDialogRef<PublisherFormComponent>);
  readonly PublisherFormData = inject<PublisherFormData>(MAT_DIALOG_DATA);
  Publisher: Publisher;

  isEdit: boolean;

  allPublishers: Publisher[] = [];

  form: FormGroup = this.formBuilder.group({
    id: [null],
    companyName: [null, Validators.required],
    homePage: [null],
    description: [null],
  });

  constructor(
    private publisherService: PublisherService,
    private formBuilder: FormBuilder
  ) {
    this.isEdit = this.PublisherFormData.isEdit;
    this.Publisher = this.PublisherFormData.publisher;
  }

  ngOnInit(): void {
    this.setForm();
    this.setAllPublishers();
  }

  setForm() {
    this.form.patchValue(this.Publisher);
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.isEdit) {
      this.updatePublisher();
    } else {
      this.createPublisher();
    }
  }

  createPublisher() {
    const Publisher: Publisher = {
      ...this.Publisher,
      ...this.form.value,
    };

    this.publisherService
      .addPublisher(Publisher)
      .subscribe(() => this.dialogRef.close());
  }

  private updatePublisher() {
    const updatedPublisher: Publisher = {
      ...this.Publisher,
      ...this.form.value,
    };

    this.publisherService
      .updatePublisher(updatedPublisher)
      .subscribe(() => this.dialogRef.close());
  }

  setAllPublishers(): void {
    this.publisherService
      .getAllPublishers()
      .subscribe((Publishers) => (this.allPublishers = Publishers));
  }
}
