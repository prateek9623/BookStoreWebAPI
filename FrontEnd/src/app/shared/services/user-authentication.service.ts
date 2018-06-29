import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, Subject } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AppUser } from '../models/app-user';
import { Claim } from '../models/claim';
import { ActivatedRoute, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserAuthenticationService {
  readonly rootUrl = environment.webApiUrl;
  user: Claim;
  private logger = new Subject<Claim>();

  constructor(private http: HttpClient, private route: ActivatedRoute, private router: Router) { }

  registerUser(user: AppUser) {
    const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') || '/';
    localStorage.setItem('returnUrl', returnUrl);
    const body: AppUser = {
      userName: user.userName,
      password: user.password,
      email: user.email,
      firstName: user.firstName,
      lastName: user.lastName
    };
    return this.http.post(this.rootUrl + '/user/registration', body)
      .subscribe((data: any) => {
        this.user = { username: data.UserName, sessionId: data.SessionId, isAdmin: data.isAdmin };
        localStorage.setItem('user', JSON.stringify(this.user));
        this.logger.next(this.user);
        this.router.navigateByUrl(returnUrl);
      });
  }

  loginUser(claim: Claim) {
    const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') || '/';
    localStorage.setItem('returnUrl', returnUrl);
    const body: any = {
      userName: claim.username,
      password: claim.password
    };
    return this.http.post(this.rootUrl + '/user/login', body)
      .subscribe((data: any) => {
        this.user = { username: data.UserName, sessionId: data.SessionId, isAdmin: data.isAdmin };
        localStorage.setItem('user', JSON.stringify(this.user));
        this.logger.next(this.user);
        this.router.navigateByUrl(returnUrl);
      });

  }

  logOut(claim: Claim) {
    const body: any = {
      UserName: claim.username,
      SessionId: claim.sessionId
    };
    localStorage.removeItem('user');
    this.logger.next(null);
    this.router.navigateByUrl('/');
    return this.http.post(this.rootUrl + '/user/logout', body);
  }

  getUser(): Observable<Claim> {
    return this.logger.asObservable();
  }

  getUserDetails(claim: Claim) {
    this.logger.next(claim);
  }
}
