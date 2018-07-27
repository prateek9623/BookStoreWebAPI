import { Component, OnInit } from '@angular/core';
import { UserAuthenticationService } from '../shared/services/user-authentication.service';
import { Observable } from 'rxjs';
import { User } from '../shared/models/user';
import { Cart } from '../shared/models/cart';
import { CartService } from '../shared/services/cart.service';

@Component({
  selector: 'app-bs-navbar',
  templateUrl: './bs-navbar.component.html',
  styleUrls: ['./bs-navbar.component.css']
})
export class BsNavbarComponent implements OnInit {
  claim$: Observable<User>;
  user: User;
  cart: Cart = new Cart([]);

  constructor(private userAuthentication: UserAuthenticationService, private cartService: CartService) { }

  ngOnInit() {
    this.claim$ = this.userAuthentication.getUser();
    this.claim$.subscribe(x => this.user);
    this.cartService.getCart().subscribe(x => { this.cart = x; });
  }

  logOut() {
    this.user = JSON.parse(localStorage.getItem('user'));
    this.userAuthentication.logOut();
  }
}
