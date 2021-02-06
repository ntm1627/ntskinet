import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { ShopComponent } from './shop/shop.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {path:'',component: HomeComponent},
  {path:'shop',loadChildren: () =>import ('./shop/shop.module').then(mod =>mod.ShopModule)}, //this is for lazy loading
  {path:'**',redirectTo: '',pathMatch:'full'} // for a bad url go to home page

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],  // when we use root it is referring to the app module
  exports: [RouterModule]
})
export class AppRoutingModule { }
