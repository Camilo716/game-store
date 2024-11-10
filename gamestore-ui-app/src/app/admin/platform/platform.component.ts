import { Component, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { Platform } from '../../core/models/platform';
import { PlatformService } from '../../core/services/platform.service';
import { PlatformFormComponent } from './platform-form/platform-form.component';

@Component({
  selector: 'app-platforms',
  standalone: true,
  imports: [
    MatTableModule,
    MatCardModule,
    CommonModule,
    MatMenuModule,
    MatIconModule,
  ],
  templateUrl: './platform.component.html',
  styleUrl: './platform.component.css',
})
export class PlatformComponent {
  title: string = 'Platforms';
  platforms: Platform[] = [];

  columns = [
    { matColumnDef: 'id', data: 'id', text: 'Id' },
    { matColumnDef: 'type', data: 'type', text: 'Type' },
  ];

  displayedColumns: string[] = [];

  readonly formDialog = inject(MatDialog);

  constructor(private platformService: PlatformService) {}

  ngOnInit(): void {
    this.setDisplayedColumns();
    this.loadPlatforms();
  }

  loadPlatforms(): void {
    this.platformService.getAllPlatforms().subscribe((data: Platform[]) => {
      this.platforms = data;
    });
  }

  deletePlatform(id: string): void {
    this.platformService.deletePlatform(id).subscribe(() => {
      this.loadPlatforms();
    });
  }

  openEditPlatformDialog(platform: Platform): void {
    const dialogRef = this.formDialog.open(PlatformFormComponent, {
      data: {
        platform: platform,
        isEdit: true,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadPlatforms();
    });
  }

  openAddPlatformDialog(): void {
    const dialogRef = this.formDialog.open(PlatformFormComponent, {
      data: {
        isEdit: false,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadPlatforms();
    });
  }

  setDisplayedColumns() {
    this.displayedColumns = this.columns.map((column) => column.matColumnDef);
    this.displayedColumns.push('actions');
  }
}
