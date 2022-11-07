import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  
  
  isLoggedIn$!:any

  constructor(private router: Router) { 
      this.isLoggedIn$ = !!localStorage.getItem('token')
  }

  get token(){
    return localStorage.getItem('token') as string
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if(this.isLoggedIn$){
      return true
    }else{
      this.router.navigate(['login'])
      return false
    }
  }
  
}
