import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
 // baseUrl = environment.apiUrl;
 baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getOrdersForUser() {
    return this.http.get(this.baseUrl + 'orders');
  }

  getOrderDetailed(id: number) {
    return this.http.get(this.baseUrl + 'orders/' + id);
  }
}