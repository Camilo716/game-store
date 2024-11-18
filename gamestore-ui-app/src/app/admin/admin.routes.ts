import { Routes } from '@angular/router';
import { GameComponent } from './game/game.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PlatformComponent } from './platform/platform.component';
import { GenreComponent } from './genre/genre.component';
import { PublisherComponent } from './publisher/publisher.component';
import { UserComponent } from './user/user.component';
import { RoleComponent } from './role/role.component';

export const ADMIN_ROUTES: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'games', component: GameComponent },
  { path: 'platforms', component: PlatformComponent },
  { path: 'genres', component: GenreComponent },
  { path: 'publishers', component: PublisherComponent },
  { path: 'users', component: UserComponent },
  { path: 'roles', component: RoleComponent },
];
