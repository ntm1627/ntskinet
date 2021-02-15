import { IDeliveryMethod } from './../shared/Models/deliveryMethod';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { IOrderToCreate } from '../shared/Models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
 // baseUrl = environment.apiUrl;
 baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  createOrder(order: IOrderToCreate){
    return this.http.post(this.baseUrl + 'orders',order);
  }

  getDeliveryMethods(){
    return this.http.get(this.baseUrl + 'orders/deliveryMethods').pipe(
      map((dm: IDeliveryMethod[]) =>{
        return dm.sort((a,b) => b.price - a.price)   //in descending price
      })
    );
  }
}
