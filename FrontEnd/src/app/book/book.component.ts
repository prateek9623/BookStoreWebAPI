import { Component, OnInit } from '@angular/core';
import { UserAuthenticationService } from '../shared/services/user-authentication.service';
import { BookService } from '../shared/services/book.service';
import { ActivatedRoute } from '@angular/router';
import { CartService } from '../shared/services/cart.service';
import { Book, BookCount } from '../shared/models/book';
import { Observable } from 'rxjs';
import { User } from '../shared/models/user';
import { Cart } from '../shared/models/cart';
import { switchMap } from 'rxjs/operators';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {

  book: Book;
  user$: Observable<User>;
  id: string;
  cart: Cart = new Cart([]);

  constructor(private booksService: BookService, private route: ActivatedRoute, private cartService: CartService,
    private userService: UserAuthenticationService) {
      this.cartService.getCart().subscribe((c: Cart) => {
      this.cart = c;
    });
    }

  async ngOnInit() {
    this.user$ = this.userService.getUser();

    await this.route.queryParamMap.subscribe(params => {
      this.id = params.get('id');
      this.book = this.booksService.getBook(this.id);

    });
    console.log(this.book);
    console.log(this.cart);
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
}
