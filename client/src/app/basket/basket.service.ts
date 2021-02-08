import { IProduct } from './../shared/Models/Product';
import { Basket, IBasket, IBasketItem, IBasketTotals } from './../shared/Models/basket';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject, pipe } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
//baseUrl=environment.apiUrl;
baseUrl = 'https://localhost:5001/api/'

//A BehaviorSubject is a type of observable (i.e. a stream of data that
//we can subscribe to like the observable returned from HTTP requests in Angular).
private basketSource= new BehaviorSubject<IBasket>(null);
basket$= this.basketSource.asObservable(); //to make  the behaviorSubject public
private basketTotalSource= new BehaviorSubject<IBasketTotals>(null);
basketTotal$=this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  getBasket(id: string){
    return  this.http.get(this.baseUrl + 'basket?id=' + id)
      .pipe(
        map((basket:IBasket)=>
        {
          this.basketSource.next(basket);
          this.calculateTotals();
        })
      );
  }

  setBasket(basket: IBasket){
    return this.http.post(this.baseUrl+ 'basket',basket).subscribe((response: IBasket) =>{
      this.basketSource.next(response);
      this.calculateTotals();
    },
      error =>{
        console.log(error);
      }
    );
  }

  getCurrentBasketValue(){
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct, quantity=1){
    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item,quantity) //to resolve the naming variation in the IProduct and IBasketItem
    const basket = this.getCurrentBasketValue()  ?? this.createBasket();
    basket.items=this.addOrUpdateItem(basket.items,itemToAdd,quantity);
    this.setBasket(basket);
  }
  incrementItemQuantity(item: IBasketItem){
    const basket= this.getCurrentBasketValue();  //current basket value
    const foundItemIndex= basket.items.findIndex(x =>x.id===item.id);   //if the same type is there
    basket.items[foundItemIndex].quantity++;
    this.setBasket(basket);
  }

  decrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex(x => x.id === item.id);
    if (basket.items[foundItemIndex].quantity > 1) {
      basket.items[foundItemIndex].quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(item);
    }
  }

  removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    if (basket.items.some(x => x.id === item.id)) {
      basket.items = basket.items.filter(i => i.id !== item.id);
      if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket);
      }
    }
  }

  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl + 'basket?id=' + basket.id).subscribe(() => {
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem('basket_id');
    }, error => {
      console.log(error);
    });
  }

  private calculateTotals(){
    const basket= this.getCurrentBasketValue();
    const shipping=0;
    const subtotal=basket.items.reduce((a,b) => (b.price * b.quantity) + a, 0); //b is item and a is the number initialized with o
   // const total=subtotal + shipping;
    const tax=subtotal * .08;
    const total=subtotal + shipping + tax;  //added by another person
    this.basketTotalSource.next({shipping,total,tax,subtotal});

  }
  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const index = items.findIndex(i =>i.id === itemToAdd.id);
    if(index ===-1){          //If the item to add type is not in the basket
      itemToAdd.quantity=quantity;
      items.push(itemToAdd);
      } else                  //add if there is already an item in the basket
      {
        items[index].quantity +=quantity;
      }
      return items;
  }

  private createBasket(): IBasket {
    const basket= new Basket();
    localStorage.setItem('basket_id',basket.id)  //storing the id that we found from the api in the localstoraage/browser
    return basket;
  }
 private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity,     // since quantity is the same we don't need the mapping
      brand: item.productBrand,
      type: item.productType
    };
  }
}
