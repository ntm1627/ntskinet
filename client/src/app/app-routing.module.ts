import { TestErrorComponent } from './core/test-error/test-error.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { ShopComponent } from './shop/shop.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';


const routes: Routes = [
  {path:'',component: HomeComponent},
  {path:'test-error',component: TestErrorComponent},
  {path:'server-error',component: ServerErrorComponent},
  {path:'not-found',component: NotFoundComponent},
  {path:'shop',loadChildren: () =>import ('./shop/shop.module').then(mod =>mod.ShopModule)}, //this is for lazy loading

  {path:'**',redirectTo: '',pathMatch:'full'} // for a bad url go to home page

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],  // when we use root it is referring to the app module
  exports: [RouterModule]
})
export class AppRoutingModule { }
