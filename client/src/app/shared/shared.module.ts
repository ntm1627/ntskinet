import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import {CarouselModule} from 'ngx-bootstrap/carousel';
import { PagingHeaderComponent } from './Components/paging-header/paging-header.component';
import { PagerComponent } from './Components/pager/pager.component';
import { OrderTotalsComponent } from './Components/order-totals/order-totals.component';
import { ReactiveFormsModule } from '@angular/forms';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import { TextInputComponent } from './Components/text-input/text-input.component';
import {CdkStepperModule} from '@angular/cdk/stepper';
import { StepperComponent } from './Components/stepper/stepper.component';
import { BasketSummaryComponent } from './Components/basket-summary/basket-summary.component';



@NgModule({
  declarations: [PagingHeaderComponent, PagerComponent, OrderTotalsComponent, TextInputComponent, StepperComponent, BasketSummaryComponent],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
    ReactiveFormsModule,
    CdkStepperModule,
    RouterModule      //this is important to use the links in the basket-summary page
  ],
  exports: [
    PaginationModule,  //whatever we put in the import should be exported in this module
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule,
    OrderTotalsComponent,
    ReactiveFormsModule,
    BsDropdownModule,
    TextInputComponent,
    CdkStepperModule,
    StepperComponent,
    BasketSummaryComponent
    ]
})
export class SharedModule { }  //err solved by 1. Go to File -> Preferences -> Settings. 2. Search "experimentalDecorators" 3. Check Enable/disable experimentalDecorators
