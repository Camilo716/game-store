import { OrderService } from './../../../core/services/order.service';
import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  MatDialog,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
} from '@angular/material/dialog';
import { PaymentMethod } from '../../../core/models/payment-method';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pay',
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
  templateUrl: './pay.component.html',
  styleUrl: './pay.component.css',
})
export class PayComponent {
  readonly formDialog = inject(MatDialog);

  readonly dialogRef = inject(MatDialogRef<PayComponent>);

  paymentMethods: PaymentMethod[] = [];

  constructor(
    private orderService: OrderService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.setPaymentMethods();
  }

  form: FormGroup = this.formBuilder.group({
    paymentMethod: [null, Validators.required],
  });

  setPaymentMethods() {
    this.orderService.getPaymentMethods().subscribe((paymentMethods) => {
      this.paymentMethods = paymentMethods;
    });
  }

  confirmPayment() {
    this.orderService
      .payOrder(this.form.get('paymentMethod')?.value)
      .subscribe(() => {
        this.router.navigate(['/order']);
        this.dialogRef.close();
      });
  }

  onCancel() {
    this.dialogRef.close();
  }
}
