import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { UserAuthenticationService } from './user-authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AdminAuthGuardService implements CanActivate {
  constructor(private router: Router, private userAuthenticationService: UserAuthenticationService) { }

  canActivate(route, state: RouterStateSnapshot) {
    if (JSON.parse(localStorage.getItem('user')).isAdmin) {
      return true;
    } else {
      alert('This user doesnt have this priviledges!!');
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      return false;
    }
  }
}
