import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import {CarouselModule} from 'ngx-bootstrap/carousel';
import { PagingHeaderComponent } from './Components/paging-header/paging-header.component';
import { PagerComponent } from './Components/pager/pager.component';
import { OrderTotalsComponent } from './Components/order-totals/order-totals.component';



@NgModule({
  declarations: [PagingHeaderComponent, PagerComponent, OrderTotalsComponent],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot()
  ],
  exports: [
    PaginationModule,  //whatever we put in the import should be exported in this module
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule,
    OrderTotalsComponent
    ]
})
export class SharedModule { }  //err solved by 1. Go to File -> Preferences -> Settings. 2. Search "experimentalDecorators" 3. Check Enable/disable experimentalDecorators
