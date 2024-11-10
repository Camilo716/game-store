import { Component } from '@angular/core';
import { ReadonlyGameComponent } from './readonly-game/readonly-game.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ReadonlyGameComponent, RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {}
