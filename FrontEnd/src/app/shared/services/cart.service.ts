import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { BookCount, Book } from '../models/book';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { CookieService } from 'ngx-cookie-service';
import { User } from '../models/user';
import { UserAuthenticationService } from './user-authentication.service';
import { Cart } from '../models/cart';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  readonly rootUrl = environment.webApiUrl;
  public cart$ = new Subject<Cart>();
  constructor(private http: HttpClient, private userService: UserAuthenticationService
    , private cookieService: CookieService, private router: Router) { }

  addToCart(cartItem: BookCount) {
    const sessionId = this.cookieService.get('sessionId');
    if (sessionId !== null && sessionId !== '') {
      // tslint:disable-next-line:max-line-length
      this.http.post(this.rootUrl + '/cart/update', cartItem, { headers: new HttpHeaders().set('Authorization', 'Bearer ' + sessionId) })
        .subscribe((data: User) => {
          this.userService.user$.next(data);
          this.cart$.next(new Cart(data.CartBookList));
          localStorage.setItem('user', JSON.stringify(data));
          this.cookieService.deleteAll();
          this.cookieService.set('sessionId', data.SessionId, 2, '/', 'localhost');
        }, (data) => {
          this.userService.user$.next(data.error);
          localStorage.setItem('user', JSON.stringify(data.error));
          this.cookieService.deleteAll();
          this.cookieService.set('sessionId', data.error.SessionId, 2, '/', 'localhost');
          alert('Stock not available!!');
        });
    } else {
      this.router.navigate(['/login']);
    }
  }

  getCart(): Observable<Cart> {
    this.userService.user$.subscribe((user: User) => {
      if (user !== null) {
        this.cart$.next(new Cart(user.CartBookList));
      } else {
        this.cart$.next(new Cart([]));
      }
    });
    return this.cart$.asObservable();
  }

  clearCart() {
    const sessionId = this.cookieService.get('sessionId');
    if (sessionId !== null && sessionId !== '') {
      // tslint:disable-next-line:max-line-length
      this.http.post(this.rootUrl + '/cart/clear', {}, { headers: new HttpHeaders().set('Authorization', 'Bearer ' + sessionId) })
        .subscribe((data: User) => {
          this.userService.user$.next(data);
          this.cart$.next(new Cart(data.CartBookList));
          localStorage.setItem('user', JSON.stringify(data));
          this.cookieService.deleteAll();
          this.cookieService.set('sessionId', data.SessionId, 2, '/', 'localhost');
        });
    }
  }
}
