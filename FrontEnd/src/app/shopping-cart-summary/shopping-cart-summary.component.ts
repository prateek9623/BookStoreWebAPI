import { Component, OnInit, Input } from '@angular/core';
import { Cart } from '../shared/models/cart';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'shopping-cart-summary',
  templateUrl: './shopping-cart-summary.component.html',
  styleUrls: ['./shopping-cart-summary.component.css']
})
export class ShoppingCartSummaryComponent implements OnInit {
  @Input('cart') cart: Cart;

  constructor() { }

  ngOnInit() {
  }

}
