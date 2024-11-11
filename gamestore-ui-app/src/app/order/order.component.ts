import { Component, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { OrderService } from '../core/services/order.service';
import { Order } from '../core/models/order';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    MatTableModule,
    MatCardModule,
    CommonModule,
    MatMenuModule,
    MatIconModule,
  ],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css',
})
export class OrderComponent {
  title: string = 'Orders';
  orders: Order[] = [];

  columns = [
    { matColumnDef: 'id', data: 'id', text: 'Id' },
    { matColumnDef: 'date', data: 'date', text: 'Date' },
    {
      matColumnDef: 'status',
      data: 'status',
      text: 'Status',
      parseFunction: (data: number): string => this.mapStatusCodeToName(data),
    },
  ];

  displayedColumns: string[] = [];

  readonly formDialog = inject(MatDialog);

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.setDisplayedColumns();
    this.loadOrders();
  }

  loadOrders(): void {
    this.orderService.getOrders().subscribe((data: Order[]) => {
      this.orders = data;
    });
  }

  setDisplayedColumns() {
    this.displayedColumns = this.columns.map((column) => column.matColumnDef);
  }

  mapStatusCodeToName(orderStatus: number): string {
    const statusMap: { [key: string]: string } = {
      0: 'Open',
      1: 'Checkout',
      2: 'Paid',
      3: 'Cancelled',
    };

    return statusMap[orderStatus];
  }
}
