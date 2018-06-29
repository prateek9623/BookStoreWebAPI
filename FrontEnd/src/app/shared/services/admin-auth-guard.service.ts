import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Claim } from '../models/claim';
import { UserAuthenticationService } from './user-authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AdminAuthGuardService implements CanActivate {
  user: Claim;
  constructor(private router: Router, private userAuthenticationService: UserAuthenticationService) { }

  canActivate(route, state: RouterStateSnapshot) {
    if (JSON.parse(localStorage.getItem('user')).isAdmin) {
      return true;
    } else {
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      return false;
    }
  }
}
