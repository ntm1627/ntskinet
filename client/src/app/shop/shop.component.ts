import { ShopParams } from './../shared/Models/shopParams';
import { IType } from './../shared/Models/ProductType';
import { IBrand } from './../shared/Models/brand';
import { ShopService } from './shop.service';
import { IProduct } from './../shared/Models/Product';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})


export class ShopComponent implements OnInit {
  @ViewChild('search', { static: false }) searchTerm: ElementRef; //search should work once the elements are loaded
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  shopParams: ShopParams;
  totalCount: number;
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price:Low to High', value: 'priceAsc' },
    { name: 'Price:High to Low', value: 'priceDesc' },
  ];

  constructor(private shopService: ShopService) {
    this.shopParams = this.shopService.getShopParams();
  }

  ngOnInit(): void {
    this.getProducts(true);
    this.getBrands();
    this.getTypes();
  }

  getProducts(useCache = false) {
    this.shopService.getProducts(useCache).subscribe(
      (response) => {
        this.products = response.data;
        this.totalCount = response.count;   //this is for the pagination
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getBrands() {
    this.shopService.getBrands().subscribe(
      (response) => {
        this.brands = [{ id: 0, name: 'All' }, ...response];
      }, // the three dots are ... spread operator, adds the all in the side menu
      (error) => {
        console.log(error);
      }
    );
  }

  getTypes() {
    this.shopService.getTypes().subscribe(
      (response) => {
        this.types = [{ id: 0, name: 'All' }, ...response];
      },
      (error) => {
        console.log(error);
      }
    );
  }
//most of the methods will be acceed through the service in order to make the requests remembered by the service
  onBrandSelected(brandId: number) {
    const params = this.shopService.getShopParams();
    params.brandId = brandId;
    params.pageNumber = 1;
    this.shopService.setShopParams(params); //this way our service will remember what params have used
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    const params = this.shopService.getShopParams();
    params.typeId = typeId;
    params.pageNumber = 1; //this is added as it display an error inside the console while navigating in page 2 and 3 and sorting
    this.shopService.setShopParams(params); //this way our service will remember what params have used
    this.getProducts();
  }

  onSortSelected(sort: string) {
    const params = this.shopService.getShopParams();
    params.sort = sort;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onPageChanged(event: any) {
    const params = this.shopService.getShopParams();
    if (params.pageNumber !== event) {
      //it is rapped with an if as the request is sent twice to server inside network(inspect)
      params.pageNumber = event; //like event.page, the event is the page number and that is supplied by the child component
      this.shopService.setShopParams(params);
      this.getProducts(true);
    }
  }

  onSearch() {
    const params = this.shopService.getShopParams();
    params.search = this.searchTerm.nativeElement.value;
    params.pageNumber = 1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = ''; //setting to null
    this.shopParams = new ShopParams(); // this reset all to the initial values
    this.shopService.setShopParams(this.shopParams);
    this.getProducts();
  }
}
