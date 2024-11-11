import { Order } from './../../core/models/order';
import { Component, inject, OnInit } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { CartFormData } from './CartFormData';
import { OrderService } from '../../core/services/order.service';
import { OrderGame } from '../../core/models/order-game';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-Order-form',
  standalone: true,
  imports: [
    MatTableModule,
    MatFormFieldModule,
    MatCardModule,
    MatDialogContent,
    MatDialogActions,
    MatInputModule,
    ReactiveFormsModule,
    MatOptionModule,
    MatSelectModule,
    CommonModule,
    MatMenuModule,
    MatIconModule,
  ],
  templateUrl: './cart-details.component.html',
  styleUrl: './cart-details.component.css',
})
export class CartDetailsComponent implements OnInit {
  title: string = 'Order Games';
  readonly dialogRef = inject(MatDialogRef<CartDetailsComponent>);
  readonly CartFormData = inject<CartFormData>(MAT_DIALOG_DATA);
  order: Order;

  orderGames: OrderGame[] = [];

  columns = [
    { matColumnDef: 'productId', data: 'productId', text: 'Product' },
    { matColumnDef: 'price', data: 'price', text: 'Price' },
    { matColumnDef: 'quantity', data: 'quantity', text: 'Quantity' },
    { matColumnDef: 'discount', data: 'discount', text: 'Discount' },
  ];

  displayedColumns: string[] = [];

  constructor(private orderService: OrderService) {
    this.order = this.CartFormData.order;
  }

  ngOnInit(): void {
    this.setOrderDetails();
    this.setDisplayedColumns();
  }

  onExit(): void {
    this.dialogRef.close();
  }

  openPayDialog(): void {
    // Navigate to pay component
  }

  setOrderDetails(): void {
    this.orderService
      .getOrderDetailsById(this.order.id)
      .subscribe((orderGames) => {
        this.orderGames = orderGames;
      });
  }

  setDisplayedColumns() {
    this.displayedColumns = this.columns.map((column) => column.matColumnDef);
  }
}
