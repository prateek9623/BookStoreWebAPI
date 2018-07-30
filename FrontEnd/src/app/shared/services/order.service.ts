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
import { Order } from '../models/order';
import { CartService } from './cart.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  readonly rootUrl = environment.webApiUrl;
  orders$ = new Subject<Order[]>();
  constructor(private http: HttpClient, private userService: UserAuthenticationService
    , private cookieService: CookieService, private router: Router, private cartService: CartService) {
    userService.getUser().subscribe((user: User) => {
      this.orders$.next(user.OrderList);
    });
  }

  PlaceOrder(OrderDetails: Order): boolean {
    const sessionId = this.cookieService.get('sessionId');
    if (sessionId !== null && sessionId !== '') {
      // tslint:disable-next-line:max-line-length
      this.http.post(this.rootUrl + '/order/add', OrderDetails, { headers: new HttpHeaders().set('Authorization', 'Bearer ' + sessionId) })
        .subscribe((data: User) => {
          this.userService.user$.next(data);
          this.cartService.cart$.next(new Cart(data.CartBookList));
          this.orders$.next(data.OrderList);
          localStorage.setItem('user', JSON.stringify(data));
          this.cookieService.deleteAll();
          this.cookieService.set('sessionId', data.SessionId, 2, '/', 'localhost');
          this.router.navigate(['/my/orders']);
        }, (data) => {
          this.userService.user$.next(data.error);
          localStorage.setItem('user', JSON.stringify(data.error));
          this.cookieService.deleteAll();
          this.cookieService.set('sessionId', data.error.SessionId, 2, '/', 'localhost');
          alert('Order not Placed due to stock unavailablity!!');
          this.router.navigate(['/']);
        });
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }

  getOrders(): Observable<Order[]> {
    this.orders$.subscribe(o => {
      o.sort((a, b) => {
        if (new Date(a.OrderPlaceTime) > new Date(b.OrderPlaceTime)) {
          return -1;
        } else if (new Date(a.OrderPlaceTime) < new Date(b.OrderPlaceTime)) {
          return 1;
        } else {
          return 0;
        }
      });
      console.log(o);
    });
    return this.orders$;
  }
}
