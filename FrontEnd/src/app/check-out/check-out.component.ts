import { Component, OnInit } from '@angular/core';
import { CartService } from '../shared/services/cart.service';
import { Observable } from 'rxjs';
import { Cart } from '../shared/models/cart';

@Component({
  selector: 'app-check-out',
  templateUrl: './check-out.component.html',
  styleUrls: ['./check-out.component.css']
})
export class CheckOutComponent implements OnInit {
  cart$: Observable<Cart>;

  constructor(private cartService: CartService) { }

  async ngOnInit() {
    this.cart$ = await this.cartService.getCart();
  }

}
