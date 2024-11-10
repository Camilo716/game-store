import { Component, inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { GameService } from '../../core/services/game.service';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { Game } from '../../core/models/game';
import { CommonModule } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { GameFormComponent } from './game-form/game-form.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Publisher } from '../../core/models/publisher';
import { PublisherService } from '../../core/services/publisher.service';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { GenreService } from '../../core/services/genre.service';
import { Genre } from '../../core/models/genre';
import { PlatformService } from '../../core/services/platform.service';
import { Platform } from '../../core/models/platform';

@Component({
  selector: 'app-game',
  standalone: true,
  imports: [
    MatTableModule,
    MatCardModule,
    CommonModule,
    MatMenuModule,
    MatIconModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatOptionModule,
  ],
  templateUrl: './game.component.html',
  styleUrl: './game.component.css',
})
export class GameComponent implements OnInit {
  title: string = 'Games';
  games: Game[] = [];

  allPublishers: Publisher[] = [];

  allGenres: Genre[] = [];

  allPlatforms: Platform[] = [];

  filterForm: FormGroup = this.formBuilder.group({
    publisherId: null,
    genreId: null,
    platformId: null,
  });

  columns = [
    { matColumnDef: 'id', data: 'id', text: 'Id' },
    { matColumnDef: 'name', data: 'name', text: 'Name' },
    { matColumnDef: 'description', data: 'description', text: 'Description' },
    { matColumnDef: 'key', data: 'key', text: 'Key' },
    { matColumnDef: 'price', data: 'price', text: 'Price' },
    {
      matColumnDef: 'unitsInStock',
      data: 'unitsInStock',
      text: 'Units in stock',
    },
    { matColumnDef: 'discount', data: 'discount', text: 'Discount' },
    { matColumnDef: 'publisherId', data: 'publisherId', text: 'Publisher Id' },
  ];

  displayedColumns: string[] = [];

  readonly formDialog = inject(MatDialog);

  constructor(
    private gameService: GameService,
    private publisherService: PublisherService,
    private genreService: GenreService,
    private platformService: PlatformService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.setDisplayedColumns();

    this.loadGames();

    this.onChangeGenreFilter();
    this.onChangePlatformFilter();
    this.onChangePublisherFilter();

    this.setAllPublishers();
    this.setAllGenres();
    this.setAllPlatforms();
  }

  onChangeGenreFilter() {
    this.filterForm.get('genreId')?.valueChanges.subscribe((value) => {
      if (value !== null) {
        this.filterForm.patchValue(
          { publisherId: null, platformId: null },
          { emitEvent: false }
        );
      }

      this.loadGames();
    });
  }

  onChangePlatformFilter() {
    this.filterForm.get('platformId')?.valueChanges.subscribe((value) => {
      if (value !== null) {
        this.filterForm.patchValue(
          { publisherId: null, genreId: null },
          { emitEvent: false }
        );
      }

      this.loadGames();
    });
  }

  onChangePublisherFilter() {
    this.filterForm.get('publisherId')?.valueChanges.subscribe((value) => {
      if (value !== null) {
        this.filterForm.patchValue(
          { platformId: null, genreId: null },
          { emitEvent: false }
        );
      }

      this.loadGames();
    });
  }

  loadGames(): void {
    if (this.publisherId) {
      this.loadGamesByPublisher();
      return;
    }

    if (this.genreId) {
      this.loadGamesByGenre();
      return;
    }

    if (this.platformId) {
      this.loadGamesByPlatform();
      return;
    }

    this.loadAllGames();
  }

  loadGamesByPublisher() {
    this.gameService
      .getGamesByPublisherId(this.publisherId)
      .subscribe((data: Game[]) => {
        this.games = data;
      });
  }

  loadGamesByGenre() {
    this.gameService
      .getGamesByGenreId(this.genreId)
      .subscribe((data: Game[]) => {
        this.games = data;
      });
  }

  loadGamesByPlatform() {
    this.gameService
      .getGamesByPlatformId(this.platformId)
      .subscribe((data: Game[]) => {
        this.games = data;
      });
  }

  loadAllGames() {
    this.gameService.getAllGames().subscribe((data: Game[]) => {
      this.games = data;
    });
  }

  deleteGame(id: string): void {
    this.gameService.deleteGame(id).subscribe(() => {
      this.loadGames();
    });
  }

  openEditGameDialog(game: Game): void {
    const dialogRef = this.formDialog.open(GameFormComponent, {
      data: {
        game,
        isEdit: true,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadGames();
    });
  }

  openAddGameDialog(): void {
    const dialogRef = this.formDialog.open(GameFormComponent, {
      data: {
        isEdit: false,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadGames();
    });
  }

  setDisplayedColumns() {
    this.displayedColumns = this.columns.map((column) => column.matColumnDef);
    this.displayedColumns.push('actions');
  }

  setAllPublishers(): void {
    this.publisherService
      .getAllPublishers()
      .subscribe((publishers) => (this.allPublishers = publishers));
  }

  setAllGenres(): void {
    this.genreService
      .getAllGenres()
      .subscribe((genres) => (this.allGenres = genres));
  }

  setAllPlatforms(): void {
    this.platformService
      .getAllPlatforms()
      .subscribe((platforms) => (this.allPlatforms = platforms));
  }

  get publisherId(): string {
    return this.filterForm.get('publisherId')?.value;
  }

  get genreId(): string {
    return this.filterForm.get('genreId')?.value;
  }

  get platformId(): string {
    return this.filterForm.get('platformId')?.value;
  }
}
