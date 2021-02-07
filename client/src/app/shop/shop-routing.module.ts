import { RouterModule, Routes } from '@angular/router';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ShopComponent } from './shop.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


const routes: Routes=[

  {path:'',component: ShopComponent},  // this is the root component for our shop module 'shop'
  {path:':id',component: ProductDetailsComponent, data: {breadcrumb: {alias: 'productDetails'}}},  // i.e 'shop/:id'
]
@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)  //these are added here for lazy loading, can be checked in the inspect/network by clicking home and shop
  ],
  exports: [RouterModule]
})
export class ShopRoutingModule { }
