import { RouterModule } from '@angular/router';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [NavBarComponent],
  imports: [
    CommonModule,  //provides common modules e.g *NgFor to loop through records
    RouterModule
  ],
  exports: [NavBarComponent]
})
export class CoreModule { }
