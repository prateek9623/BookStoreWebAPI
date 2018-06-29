import { Component, OnInit } from '@angular/core';
import { AppUser } from '../shared/models/app-user';
import { NgForm } from '@angular/forms';
import { UserAuthenticationService } from '../shared/services/user-authentication.service';
import { Claim } from '../shared/models/claim';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: AppUser;
  claim: Claim;

  constructor(private userAuthentication: UserAuthenticationService) { }

  ngOnInit() {
    this.claim = {
      username: '',
      password: ''
    };
    this.user = {
      userName: '',
      password: '',
      email: '',
      firstName: '',
      lastName: '',
      repeat: ''
    };
  }

  Login(form: NgForm) {
    this.userAuthentication.loginUser(form.value);
  }

  Register(form: NgForm) {
    this.userAuthentication.registerUser(form.value);
  }
}

