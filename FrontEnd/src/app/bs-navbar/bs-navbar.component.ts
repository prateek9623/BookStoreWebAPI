import { Component, OnInit } from '@angular/core';
import { UserAuthenticationService } from '../shared/services/user-authentication.service';
import { Observable } from 'rxjs';
import { User } from '../shared/models/user';

@Component({
  selector: 'app-bs-navbar',
  templateUrl: './bs-navbar.component.html',
  styleUrls: ['./bs-navbar.component.css']
})
export class BsNavbarComponent implements OnInit {
  claim$: Observable<User>;
  user: User;

  constructor(private userAuthentication: UserAuthenticationService) { }

  ngOnInit() {
    this.claim$ = this.userAuthentication.getUser();
    this.claim$.subscribe(x => this.user);
  }

  logOut() {
    this.user = JSON.parse(localStorage.getItem('user'));
    this.userAuthentication.logOut();
  }
}
