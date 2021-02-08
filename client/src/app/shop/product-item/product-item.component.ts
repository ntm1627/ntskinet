import { BasketService } from './../../basket/basket.service';
import { IProduct } from './../../shared/Models/Product';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product: IProduct  // since product-item is a child class it has to import products from the parent shop.component

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
  }

  addItemToBasket(){
    this.basketService.addItemToBasket(this.product)
  }
}
