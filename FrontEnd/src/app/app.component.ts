import { Component, OnInit } from '@angular/core';
// import { BookService } from './shared/services/book.service';
import { UserAuthenticationService } from './shared/services/user-authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private userService: UserAuthenticationService) {}
  ngOnInit() {
    // localStorage.clear();
    this.userService.getUserDetails();
  }
}
