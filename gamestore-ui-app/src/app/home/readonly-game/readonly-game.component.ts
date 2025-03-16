import { Component, inject, OnInit } from '@angular/core';
import { GameService } from '../../core/services/game.service';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { Game } from '../../core/models/game';
import { CommonModule } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { GameCommentComponent } from '../../game-comment/game-comment.component';

@Component({
  selector: 'app-readonly-game',
  standalone: true,
  imports: [
    MatTableModule,
    MatCardModule,
    CommonModule,
    MatMenuModule,
    MatIconModule,
  ],
  templateUrl: './readonly-game.component.html',
  styleUrl: './readonly-game.component.css',
})
export class ReadonlyGameComponent implements OnInit {
  title: string = 'Games';
  games: Game[] = [];

  columns = [
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
    { matColumnDef: 'publisherId', data: 'publisherId', text: 'Publisher' },
  ];

  displayedColumns: string[] = [];

  readonly commentDialog = inject(MatDialog);

  constructor(private gameService: GameService, private router: Router) {}

  ngOnInit(): void {
    this.setDisplayedColumns();
    this.loadGames();
  }

  loadGames(): void {
    this.gameService.getAllGames().subscribe((data: Game[]) => {
      this.games = data;
    });
  }

  addToCart(gameKey: string): void {
    this.gameService.addToCart(gameKey).subscribe(() => {
      this.router.navigate(['/cart']);
    });
  }

  openCommentsDialog(game: Game): void {
    const dialogRef = this.commentDialog.open(GameCommentComponent, {
      data: {
        game,
        isEdit: true,
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
}
