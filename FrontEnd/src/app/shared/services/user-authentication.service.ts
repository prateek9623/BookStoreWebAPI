import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of, Subject } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AppUser } from '../models/app-user';
import { Claim } from '../models/claim';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../models/user';
import { CookieService } from 'ngx-cookie-service';
import { CartService } from './cart.service';
import { Cart } from '../models/cart';

@Injectable({
  providedIn: 'root'
})
export class UserAuthenticationService {
  readonly rootUrl = environment.webApiUrl;
  user: User;
  user$ = new Subject<User>();

  constructor(private http: HttpClient, private route: ActivatedRoute, private router: Router, private cookieService: CookieService) { }

  registerUser(user: AppUser) {
    const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') || '/';
    localStorage.setItem('returnUrl', returnUrl);
    this.http.post(this.rootUrl + '/user/registration', user)
      .subscribe((data: User) => {
        this.user = data;
        this.cookieService.set('sessionId', this.user.SessionId, 2, '/', 'localhost');
        localStorage.setItem('user', JSON.stringify(data));
        this.user$.next(this.user);
        // this.cartService.cart$.next(new Cart(data.CartBookList));
        this.router.navigateByUrl(returnUrl);
      });
    // this.cookieService.set('sessionId', this.user.SessionId, 2, 'localhost', '/', true);
  }

  loginUser(claim: Claim) {
    const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') || '/';
    localStorage.setItem('returnUrl', returnUrl);
    this.http.post(this.rootUrl + '/user/login', claim, { withCredentials: true })
      .subscribe((data: User) => {
        this.user = data;
        // console.log(this.user.SessionId);
        this.cookieService.set('sessionId', this.user.SessionId, 2, '/', 'localhost');
        localStorage.setItem('user', JSON.stringify(data));
        this.user$.next(this.user);
        // this.cartService.cart$.next(new Cart(data.CartBookList));
        this.router.navigateByUrl(returnUrl);
      });
    // console.log(this.user.SessionId);
    // console.log(this.cookieService.get('sessionId'));
  }

  logOut() {
    // tslint:disable-next-line:max-line-length
    this.http.post(this.rootUrl + '/user/logout', {}, { headers: new HttpHeaders().set('Authorization', 'Bearer ' + this.cookieService.get('sessionId')) })
      .subscribe(() => {
        localStorage.removeItem('user');
        // this.cartService.cart$.next(null);
        this.user$.next(null);
        this.router.navigateByUrl('/');
        this.cookieService.deleteAll();
      },
        () => {
          localStorage.removeItem('user');
          this.user$.next(null);
          // this.cartService.cart$.next(null);
          this.router.navigateByUrl('/');
          this.cookieService.deleteAll();
        });
  }

  getUser(): Observable<User> {
    this.getUserDetails();
    return this.user$.asObservable();
  }

  getUserDetails() {
    const sessionId = this.cookieService.get('sessionId');
    if (sessionId !== null && sessionId !== '') {
      // tslint:disable-next-line:max-line-length
      this.http.post(this.rootUrl + '/user/details', {}, { headers: new HttpHeaders().set('Authorization', 'Bearer ' + sessionId) })
        .subscribe((data: User) => {
          this.user = data;
          localStorage.setItem('user', JSON.stringify(this.user));
        // this.cartService.cart$.next(new Cart(data.CartBookList));
          this.user$.next(this.user);
        }, err => {
          this.cookieService.deleteAll();
          localStorage.removeItem('user');
        });
      return this.user;
    }
  }
}
