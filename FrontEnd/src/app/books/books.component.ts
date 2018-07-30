import { Component, OnInit } from '@angular/core';
import { BookService } from '../shared/services/book.service';
import { ActivatedRoute } from '@angular/router';
import { Book, BookCount } from '../shared/models/book';
import { switchMap } from 'rxjs/operators';
import { CartService } from '../shared/services/cart.service';
import { Observable } from 'rxjs';
import { Cart } from '../shared/models/cart';
import { User } from '../shared/models/user';
import { UserAuthenticationService } from '../shared/services/user-authentication.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {
  books: Book[] = [];
  filteredBooks: Book[] = [];
  genres$;
  publishers$;
  genre: string;
  publisher: string;
  cart: Cart = new Cart([]);
  user$: Observable<User>;

  constructor(private booksService: BookService, private route: ActivatedRoute, private cartService: CartService,
    private userService: UserAuthenticationService) {
    this.cartService.getCart().subscribe(x => { this.cart = x; });
  }

  async ngOnInit() {
    this.user$ = this.userService.getUser();
    this.booksService.getBooks().pipe(switchMap((books: Book[]) => {
      this.books = books.filter(b => b.BookStock > 1);
      return this.route.queryParamMap;
    })).subscribe(params => {
      this.genre = params.get('genre');
      this.publisher = params.get('publisher');
      if (this.genre) {
        this.filteredBooks = this.books.filter(b => b.BookGenre.GenreName === this.genre);
      } else if (this.publisher) {
        this.filteredBooks = this.books.filter(b => b.BookPublisher.PublisherName === this.publisher);
      } else {
        this.filteredBooks = this.books;
      }
    });
    this.genres$ = this.booksService.getGenres();
    this.publishers$ = this.booksService.getPublishers();
    this.filteredBooks = this.books;
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
