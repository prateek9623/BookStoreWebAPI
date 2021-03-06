import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Cart } from '../shared/models/cart';
import { CartService } from '../shared/services/cart.service';
import { Book, BookCount } from '../shared/models/book';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {

  cart$: Observable<Cart>;

  constructor(private cartService: CartService) {
  }

  async ngOnInit() {
    this.cart$ = this.cartService.getCart();
  }

  addToCart(book: Book) {
    const bookCount: BookCount = {
      _Book: book,
      BookQuantity: 1
    };
    this.cartService.addToCart(bookCount);
  }

  removeFromCart(book: Book) {
    const bookCount: BookCount = {
      _Book: book,
      BookQuantity: -1
    };
    this.cartService.addToCart(bookCount);
  }

  clearCart() {
    this.cartService.clearCart();
  }
}
