import { Component, inject } from '@angular/core';
import { Genre } from '../../core/models/genre';
import { MatDialog } from '@angular/material/dialog';
import { GenreService } from '../../core/services/genre.service';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { GenreFormComponent } from './genre-form/genre-form.component';

@Component({
  selector: 'app-genre',
  standalone: true,
  imports: [
    MatTableModule,
    MatCardModule,
    CommonModule,
    MatMenuModule,
    MatIconModule,
  ],
  templateUrl: './genre.component.html',
  styleUrl: './genre.component.css',
})
export class GenreComponent {
  title: string = 'Genres';
  genres: Genre[] = [];

  columns = [
    { matColumnDef: 'id', data: 'id', text: 'Id' },
    { matColumnDef: 'name', data: 'name', text: 'Name' },
    {
      matColumnDef: 'parentGenreId',
      data: 'parentGenreId',
      text: 'Parent Genre Id',
    },
  ];

  displayedColumns: string[] = [];

  readonly formDialog = inject(MatDialog);

  constructor(private genreService: GenreService) {}

  ngOnInit(): void {
    this.setDisplayedColumns();
    this.loadGenres();
  }

  loadGenres(): void {
    this.genreService.getAllGenres().subscribe((data: Genre[]) => {
      this.genres = data;
    });
  }

  deleteGenre(id: string): void {
    this.genreService.deleteGenre(id).subscribe(() => {
      this.loadGenres();
    });
  }

  openEditGenreDialog(genre: Genre): void {
    const dialogRef = this.formDialog.open(GenreFormComponent, {
      data: {
        genre: genre,
        isEdit: true,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadGenres();
    });
  }

  openAddGenreDialog(): void {
    const dialogRef = this.formDialog.open(GenreFormComponent, {
      data: {
        isEdit: false,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadGenres();
    });
  }

  setDisplayedColumns() {
    this.displayedColumns = this.columns.map((column) => column.matColumnDef);
    this.displayedColumns.push('actions');
  }
}
