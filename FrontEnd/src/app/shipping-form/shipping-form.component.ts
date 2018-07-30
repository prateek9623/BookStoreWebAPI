import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { UserAuthenticationService } from '../shared/services/user-authentication.service';
import { OrderService } from '../shared/services/order.service';
import { Cart } from '../shared/models/cart';
import { Order } from '../shared/models/order';
import { CartService } from '../shared/services/cart.service';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'shipping-form',
  templateUrl: './shipping-form.component.html',
  styleUrls: ['./shipping-form.component.css']
})
export class ShippingFormComponent implements OnInit {
  @Input('cart') cart: Cart;
  shipping = {};
  orderDetails: Order;

  constructor(
    private router: Router,
    private authService: UserAuthenticationService,
    private orderService: OrderService,
    private cartService: CartService,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.orderDetails = {
      OrderedItems: this.cart.items,
      OrderShipped: false,
      OrderTransactionId: '',
      ReceiverAddr: {
        City: '',
        Country: '',
        LocalAddr: '',
        State: '',
        ZipCode: ''
      },
      ReceiverContactNo: '',
      ReceiverName: ''
    };
  }

  PlaceOrder(form: NgForm) {
    console.log(form.value);
    console.log(this.orderDetails);
    if (this.orderService.PlaceOrder(this.orderDetails)) {
      this.snackBar.open('Order Placed', null, { duration: 3000 });
      this.router.navigateByUrl('my/orders');
    }
  }
}
