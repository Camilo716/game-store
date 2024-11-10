import { Component, OnInit } from '@angular/core';
import { GameService } from '../../core/services/game.service';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { Game } from '../../core/models/game';
import { CommonModule } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';

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
  ];

  displayedColumns: string[] = [];

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    this.setDisplayedColumns();
    this.loadGames();
  }

  loadGames(): void {
    this.gameService.getAllGames().subscribe((data: Game[]) => {
      this.games = data;
    });
  }

  setDisplayedColumns() {
    this.displayedColumns = this.columns.map((column) => column.matColumnDef);
  }
}
