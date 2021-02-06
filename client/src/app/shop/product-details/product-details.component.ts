import { ShopService } from './../shop.service';
import { IProduct } from './../../shared/Models/Product';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  id: number;

  // the activated routes holds the id when we click on view details
  constructor(private ShopService: ShopService, private activatedRoute: ActivatedRoute) {}

  ngOnInit() {
    this.loadProduct();
  }

  loadProduct() {
    this.ShopService.getProduct(+ this.activatedRoute.snapshot.paramMap.get('id')).subscribe( //the + sign is just to cast to number
      product =>{
      this.product=product;
      },
      error =>{
      console.log(error);
      });

  }
}
