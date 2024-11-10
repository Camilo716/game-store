import { Genre } from '../../../core/models/genre';
import { GameService } from './../../../core/services/game.service';
import { Component, inject, OnInit } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
} from '@angular/material/dialog';
import { Game } from '../../../core/models/game';
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
import { PlatformService } from '../../../core/services/platform.service';
import { Platform } from '../../../core/models/platform';
import { GameFormData } from './gameFormData';
import { Publisher } from '../../../core/models/publisher';
import { PublisherService } from '../../../core/services/publisher.service';

@Component({
  selector: 'app-game-form',
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
  templateUrl: './game-form.component.html',
  styleUrl: './game-form.component.css',
})
export class GameFormComponent implements OnInit {
  readonly dialogRef = inject(MatDialogRef<GameFormComponent>);
  readonly gameFormData = inject<GameFormData>(MAT_DIALOG_DATA);
  game: Game;

  isEdit: boolean;

  allGenres: Genre[] = [];
  allPlatforms: Platform[] = [];
  allPublishers: Publisher[] = [];

  form: FormGroup = this.formBuilder.group({
    name: [null, Validators.required],
    description: [null],
    key: [null],
    publisherId: [null, Validators.required],
    price: [null, Validators.required],
    unitsInStock: [0, Validators.required],
    discount: [0, Validators.required],
    genres: [[], Validators.required],
    platforms: [[], Validators.required],
  });

  constructor(
    private gameService: GameService,
    private genreService: GenreService,
    private platformService: PlatformService,
    private publisherService: PublisherService,
    private formBuilder: FormBuilder
  ) {
    this.isEdit = this.gameFormData.isEdit;
    this.game = this.gameFormData.game;
    this.setAllGenres();
    this.setAllPlatforms();
    this.setAllPublishers();
  }

  ngOnInit(): void {
    this.setForm();
  }

  setForm() {
    this.form.patchValue(this.game);

    if (this.isEdit) {
      this.setGenresOfGame();
      this.setPlatformsOfGame();
      this.setPublisherOfGame();
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.isEdit) {
      this.updateGame();
    } else {
      this.createGame();
    }
  }

  createGame() {
    const game: Game = {
      ...this.game,
      ...this.form.value,
    };

    this.gameService
      .addGame(
        game,
        this.form.get('genres')?.value,
        this.form.get('platforms')?.value,
        this.form.get('publisherId')?.value
      )
      .subscribe(() => this.dialogRef.close());
  }

  private updateGame() {
    const updatedGame: Game = {
      ...this.game,
      ...this.form.value,
    };

    this.gameService
      .updateGame(
        updatedGame,
        this.form.get('genres')?.value,
        this.form.get('platforms')?.value,
        this.form.get('publisherId')?.value
      )
      .subscribe(() => this.dialogRef.close());
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

  setAllPublishers(): void {
    this.publisherService
      .getAllPublishers()
      .subscribe((publishers) => (this.allPublishers = publishers));
  }

  setGenresOfGame() {
    this.genreService
      .getByGameKey(this.game.key)
      .subscribe((genres: Genre[]) =>
        this.form.get('genres')?.setValue(genres.map((g) => g.id))
      );
  }

  setPlatformsOfGame() {
    this.platformService
      .getByGameKey(this.game.key)
      .subscribe((platforms: Platform[]) =>
        this.form.get('platforms')?.setValue(platforms.map((p) => p.id))
      );
  }

  setPublisherOfGame() {
    this.publisherService
      .getByGameKey(this.game.key)
      .subscribe((publisher: Publisher) => {
        this.form.get('publisherId')?.setValue(publisher.id);
      });
  }
}
