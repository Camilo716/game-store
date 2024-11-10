import { Genre } from '../../../core/models/genre';
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
import { GenreService } from '../../../core/services/genre.service';
import { MatSelectModule } from '@angular/material/select';
import { Platform } from '../../../core/models/platform';
import { GenreFormData } from './genreFormData';

@Component({
  selector: 'app-genre-form',
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
  templateUrl: './genre-form.component.html',
  styleUrl: './genre-form.component.css',
})
export class GenreFormComponent implements OnInit {
  readonly dialogRef = inject(MatDialogRef<GenreFormComponent>);
  readonly genreFormData = inject<GenreFormData>(MAT_DIALOG_DATA);
  genre: Genre;

  isEdit: boolean;

  allGenres: Genre[] = [];

  form: FormGroup = this.formBuilder.group({
    id: null,
    name: [null, Validators.required],
    parentGenreId: [null],
  });

  constructor(
    private genreService: GenreService,
    private formBuilder: FormBuilder
  ) {
    this.isEdit = this.genreFormData.isEdit;
    this.genre = this.genreFormData.genre;
  }

  ngOnInit(): void {
    this.setForm();
    this.setAllGenres();
  }

  setForm() {
    this.form.patchValue(this.genre);
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.isEdit) {
      this.updateGenre();
    } else {
      this.createGenre();
    }
  }

  createGenre() {
    const genre: Genre = {
      ...this.genre,
      ...this.form.value,
    };

    this.genreService.addGenre(genre).subscribe(() => this.dialogRef.close());
  }

  private updateGenre() {
    const updatedGenre: Genre = {
      ...this.genre,
      ...this.form.value,
    };

    this.genreService
      .updateGenre(updatedGenre)
      .subscribe(() => this.dialogRef.close());
  }

  setAllGenres(): void {
    this.genreService
      .getAllGenres()
      .subscribe((genres) => (this.allGenres = genres));
  }
}
