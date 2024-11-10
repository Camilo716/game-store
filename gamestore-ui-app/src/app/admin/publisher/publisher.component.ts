import { Component, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { Publisher } from '../../core/models/publisher';
import { PublisherService } from '../../core/services/publisher.service';
import { PublisherFormComponent } from './publisher-form/publisher-form.component';

@Component({
  selector: 'app-publishers',
  standalone: true,
  imports: [
    MatTableModule,
    MatCardModule,
    CommonModule,
    MatMenuModule,
    MatIconModule,
  ],
  templateUrl: './publisher.component.html',
  styleUrl: './publisher.component.css',
})
export class PublisherComponent {
  title: string = 'Publishers';
  publishers: Publisher[] = [];

  columns = [
    { matColumnDef: 'id', data: 'id', text: 'Id' },
    { matColumnDef: 'companyName', data: 'companyName', text: 'Company name' },
    { matColumnDef: 'homePage', data: 'homePage', text: 'Home page' },
    { matColumnDef: 'description', data: 'description', text: 'Description' },
  ];

  displayedColumns: string[] = [];

  readonly formDialog = inject(MatDialog);

  constructor(private publisherService: PublisherService) {}

  ngOnInit(): void {
    this.setDisplayedColumns();
    this.loadPublishers();
  }

  loadPublishers(): void {
    this.publisherService.getAllPublishers().subscribe((data: Publisher[]) => {
      this.publishers = data;
    });
  }

  deletePublisher(id: string): void {
    this.publisherService.deletePublisher(id).subscribe(() => {
      this.loadPublishers();
    });
  }

  openEditPublisherDialog(publisher: Publisher): void {
    const dialogRef = this.formDialog.open(PublisherFormComponent, {
      data: {
        publisher: publisher,
        isEdit: true,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadPublishers();
    });
  }

  openAddPublisherDialog(): void {
    const dialogRef = this.formDialog.open(PublisherFormComponent, {
      data: {
        isEdit: false,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadPublishers();
    });
  }

  setDisplayedColumns() {
    this.displayedColumns = this.columns.map((column) => column.matColumnDef);
    this.displayedColumns.push('actions');
  }
}
