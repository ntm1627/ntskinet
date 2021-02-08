import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';


const routes: Routes = [
  {path:'',component: HomeComponent, data: {breadcrumb: 'Home'}},
  {path:'test-error',component: TestErrorComponent, data: {breadcrumb: 'Test Errors'}},
  {path:'server-error',component: ServerErrorComponent, data: {breadcrumb: 'Server Errors'}},
  {path:'not-found',component: NotFoundComponent, data: {breadcrumb: 'Not Found'}},
  {path:'shop',loadChildren: () =>import ('./shop/shop.module').then(mod =>mod.ShopModule), data: {breadcrumb: 'Shop'}}, //this is for lazy loading
  {path:'basket',loadChildren: () =>import ('./basket/basket.module').then(mod =>mod.BasketModule), data: {breadcrumb: 'Basket'}}, //this is for lazy loading
  {path:'checkout',loadChildren: () =>import ('./checkout/checkout.module').then(mod =>mod.CheckoutModule), data: {breadcrumb: 'Checkout'}}, //this is for lazy loading

  {path:'**',redirectTo: 'not-found',pathMatch:'full'} // for a bad url go to home page('') or not found

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],  // when we use root it is referring to the app module
  exports: [RouterModule]
})
export class AppRoutingModule { }
