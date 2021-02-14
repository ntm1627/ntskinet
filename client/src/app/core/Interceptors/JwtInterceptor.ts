import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

//This token is expected to fetch token from local storage and if there is a token it will set to the header of any request
//that is going to our API server
//We need to If the Authorization token sent along with the delivery method inside the Network tab
//while delivery methods are selected in google/inspect
@Injectable()
export class JwtInterceptor implements HttpInterceptor{
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token=localStorage.getItem('token')

    if(token){
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`   //backtick and put a space as well
        }
      })
    }

    return next.handle(req);
  }


}