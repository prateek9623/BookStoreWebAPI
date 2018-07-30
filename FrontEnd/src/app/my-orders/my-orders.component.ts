import { Component, OnInit } from '@angular/core';
import { UserAuthenticationService } from '../shared/services/user-authentication.service';
import { BookService } from '../shared/services/book.service';
import { ActivatedRoute } from '@angular/router';
import { Observable, Observer } from 'rxjs';
import { Cart } from '../shared/models/cart';
import { CartService } from '../shared/services/cart.service';
import { OrderService } from '../shared/services/order.service';
import { Order } from '../shared/models/order';
import { User } from '../shared/models/user';
import { BookCount } from '../shared/models/book';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css']
})
export class MyOrdersComponent implements OnInit {

  order$: Observable<Order[]>;
  constructor(private booksService: BookService, private route: ActivatedRoute, private cartService: CartService,
    private userService: UserAuthenticationService, private orderService: OrderService) { }

  async ngOnInit() {
    this.order$ = this.orderService.getOrders();
  }
  totalPrice(OrderedItems: BookCount[]) {
    let sum = 0;
    OrderedItems.forEach((item: BookCount) => {
      sum += item._Book.BookCost * item.BookQuantity;
    });
    return sum;
  }
}
