import { shopParams } from './../shared/Models/shopParams';
import { IType } from './../shared/Models/ProductType';
import { IBrand } from './../shared/Models/brand';
import { ShopService } from './shop.service';
import { IProduct } from './../shared/Models/Product';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search',{static:false}) searchTerm: ElementRef;  //search should work once the elements are loaded
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  shopParams= new shopParams();
  totalCount: number;
  sortOptions=[
    {name:'Alphabetical', value:'name'},
    {name:'Price:Low to High',value:'priceAsc'},
    {name:'Price:High to Low',value:'priceDesc'}
  ];


  constructor(private shopService: ShopService) { }


  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe(
      response => {
      this.products = response.data;
      this.shopParams.pageNumber=response.pageIndex;
      this.shopParams.pageSize=response.pageSize;
      this.totalCount=response.count;

    },
      error => {
        console.log(error);
      });
  }

  getBrands(){
    this.shopService.getBrands().subscribe(
      response => {
      this.brands = [{id:0, name:'All'},...response]; },  // the three dots are ... spread operator, adds the all in the side menu
      error => {
        console.log(error);
      });
  }

  getTypes(){
    this.shopService.getTypes().subscribe(response => {
      this.types =  [{id:0, name:'All'},...response]; },
      error => {
        console.log(error);
      });
  }

  onBrandSelected(brandId: number){
    this.shopParams.brandId=brandId;
    this.shopParams.pageNumber=1;
    this.getProducts();

  }

  onTypeSelected(typeId: number){
    this.shopParams.typeId=typeId;
    this.shopParams.pageNumber=1; //this is added as it display an error inside the console while navigating in page 2 and 3 and sorting
    this.getProducts()
  }

  onSortSelected(sort: string){
    this.shopParams.sort=sort;
    this.getProducts();
  }

  onPageChanged(event: any){
    if(this.shopParams.pageNumber!==event){  //it is rapped with an if as the request is sent twice to server inside network(inspect)
      this.shopParams.pageNumber=event;   //like event.page, the event is the page number and that is supplied by the child component
      this.getProducts();
    }

  }

  onSearch(){
    this.shopParams.search=this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }

  onReset(){
    this.searchTerm.nativeElement.value= ''; //setting to null
    this.shopParams=new shopParams(); // this reset all to the initial values
    this.getProducts();
  }
}
