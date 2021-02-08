import { BasketService } from './../../basket/basket.service';
import { ShopService } from './../shop.service';
import { IProduct } from './../../shared/Models/Product';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  id: number;
  quantity=1

  // the activated routes holds the id when we click on view details
  constructor(private ShopService: ShopService, private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService, private basketService: BasketService) {
      this.bcService.set('@productDetails',' ');  //to hide the no that is displaying before the product name

    }

  ngOnInit() {
    this.loadProduct();
  }
  addItemToBasket (){
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  incrementQuantity(){
    this.quantity++;
  }

  decrementQuantity(){
    if(this.quantity >1){
      this.quantity--;
  }
}

  loadProduct() {
    this.ShopService.getProduct(+ this.activatedRoute.snapshot.paramMap.get('id')).subscribe( //the + sign is just to cast to number
      product =>{
      this.product=product;
      this.bcService.set('@productDetails',product.name) //this is the alias we defined in shop-routing module
      },
      error =>{
      console.log(error);
      });

  }
}
