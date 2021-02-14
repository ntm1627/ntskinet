import { BasketService } from './../../../basket/basket.service';
import { IBasket, IBasketItem } from './../../Models/basket';
import { Observable } from 'rxjs';
import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss'],
})
//The child component uses the @Output() property to raise an event to notify the parent of the change. To raise an event,
//an @Output() must have the type of EventEmitter , which is a class in @angular/core that you use to emit custom events
export class BasketSummaryComponent implements OnInit {
  basket$: Observable<IBasket>;
  @Output() decrement: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() increment: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() remove: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Input() isBasket=true

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  decrementItemQuantity(item: IBasketItem) {
    this.decrement.emit(item);
  }

  incrementItemQuantity(item: IBasketItem) {
    this.increment.emit(item);
  }

  removeBasketItem(item: IBasketItem) {
    this.remove.emit(item);
  }
}
