import { Component, OnInit } from '@angular/core';
import { UserAuthenticationService } from '../shared/services/user-authentication.service';
import { Claim } from '../shared/models/claim';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-bs-navbar',
  templateUrl: './bs-navbar.component.html',
  styleUrls: ['./bs-navbar.component.css']
})
export class BsNavbarComponent implements OnInit {
  claim$: Observable<Claim>;
  claim: Claim;

  constructor(private userAuthentication: UserAuthenticationService) { }

  ngOnInit() {
    this.claim = JSON.parse(localStorage.getItem('user'));
    this.userAuthentication.getUserDetails(this.claim);
    this.claim$ = this.userAuthentication.getUser();
    this.claim$.subscribe(x => this.claim);
  }

  logOut() {
    this.claim = {
      username: localStorage.getItem('userName'),
      sessionId: localStorage.getItem('sessionId')
    };
    this.userAuthentication.logOut(this.claim);
  }
}
