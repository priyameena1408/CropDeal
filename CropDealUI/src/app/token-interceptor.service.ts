import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem("token"); 
    if(token){
      req = req.clone({
        //headers: req.headers.set('Authorization', `Bearer ${token}`),
        setHeaders:{Authorization:`Bearer ${token}`}
      });
    }
    return next.handle(req);
  }

}
