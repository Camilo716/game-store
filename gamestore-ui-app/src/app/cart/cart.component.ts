import { Order } from './../core/models/order';
import { Component, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { OrderService } from '../core/services/order.service';
import { CartDetailsComponent } from './cart-details/cart-details.component';

@Component({
  selector: 'app-Carts',
  standalone: true,
  imports: [
    MatTableModule,
    MatCardModule,
    CommonModule,
    MatMenuModule,
    MatIconModule,
  ],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css',
})
export class CartComponent {
  title: string = 'Shopping Cart';
  cart: Order[] = [];

  columns = [
    { matColumnDef: 'id', data: 'id', text: 'Id' },
    { matColumnDef: 'date', data: 'date', text: 'Date' },
  ];

  displayedColumns: string[] = [];

  readonly formDialog = inject(MatDialog);

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.setDisplayedColumns();
    this.loadCart();
  }

  loadCart(): void {
    this.orderService.getCart().subscribe((data: Order[]) => {
      this.cart = data;
    });
  }

  openEditCartDialog(order: Order): void {
    const dialogRef = this.formDialog.open(CartDetailsComponent, {
      data: {
        order: order,
        isEdit: true,
      },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadCart();
    });
  }

  setDisplayedColumns() {
    this.displayedColumns = this.columns.map((column) => column.matColumnDef);
    this.displayedColumns.push('actions');
  }
}
