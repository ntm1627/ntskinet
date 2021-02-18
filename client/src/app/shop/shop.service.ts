import { Pagination } from './../shared/Models/Pagination';
import { ShopParams } from './../shared/Models/shopParams';
import { IType } from './../shared/Models/ProductType';
import { IBrand } from './../shared/Models/brand';
import { IPagination } from '../shared/Models/Pagination';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { IProduct } from '../shared/Models/Product';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root', // means it  is provided in the app.module in the providers []
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  products: IProduct[] = []; //this is created to cache products in the client side for performance
  brands: IBrand[] = [];
  types: IType[] = [];
  pagination = new Pagination();
  shopParams = new ShopParams();
  productCache = new Map(); //newly added for the modified code

  //Original Code

  /*

  constructor(private http: HttpClient){}

//  HttpParams values are array of values. When you set the value, it will override all the values in the array.
//  When you append the value, it will push new values on the existing array.

    getProducts(useCache: boolean)
  {
    if (useCache === false) {
      this.products = [];
    }
    //this part is for performance improvement
    if (this.products.length > 0 && useCache === true) {
      const pagesReceived = Math.ceil(this.products.length / this.shopParams.pageSize);

      if (this.shopParams.pageNumber <= pagesReceived) {
        this.pagination.data =
          this.products.slice((this.shopParams.pageNumber - 1) * this.shopParams.pageSize,
            this.shopParams.pageNumber * this.shopParams.pageSize);

        return of(this.pagination);
      }
    }
        let params=new HttpParams ();

        if(this.shopParams.brandId!==0){
          params=params.append('brandId',this.shopParams.brandId.toString());
        }

        if(this.shopParams.typeId!==0){
          params=params.append('typeId',this.shopParams.typeId.toString());
        }

        if(this.shopParams.search){
          params=params.append('search',this.shopParams.search)
        }

          params=params.append('sort',this.shopParams.sort) //sort is  already  a string and by default it is selected by name no need for if
          params=params.append('pageIndex',this.shopParams.pageNumber.toString());
          params=params.append('pageIndex',this.shopParams.pageSize.toString());

        return this.http.get<IPagination>(this.baseUrl + 'products',{observe:'response',params})
        .pipe(                //pipe is a rapper and inside we have chained rxjs  operator e.g instead of map delay(1000) could be done
          map(response =>{
           // this.products = response.body.data; //we have stored/cached the data in the variable created above
           this.products = [...this.products, ...response.body.data]  //... spread operator, append the new API along with the existing and store it in products
           this.pagination = response.body;
            return this.pagination;
          })
        );
   }

   */

  //The below code is updated due to the pagination issus as discussed in section 285

  constructor(private http: HttpClient) {}

  getProducts(useCache: boolean) {
    if (useCache === false) {
      this.productCache = new Map();
    }

    if (this.productCache.size > 0 && useCache === true) {
      if (this.productCache.has(Object.values(this.shopParams).join('-'))) {
        this.pagination.data = this.productCache.get(
          Object.values(this.shopParams).join('-')
        );

        return of(this.pagination);
      }
    }

    let params = new HttpParams();

    if (this.shopParams.brandId !== 0) {
      params = params.append('brandId', this.shopParams.brandId.toString());
    }

    if (this.shopParams.typeId !== 0) {
      params = params.append('typeId', this.shopParams.typeId.toString());
    }

    if (this.shopParams.search) {
      params = params.append('search', this.shopParams.search);
    }

    params = params.append('sort', this.shopParams.sort);
    params = params.append('pageIndex', this.shopParams.pageNumber.toString());
    params = params.append('pageSize', this.shopParams.pageSize.toString());

    return this.http
      .get<IPagination>(this.baseUrl + 'products', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          this.productCache.set(
            Object.values(this.shopParams).join('-'),
            response.body.data
          );
          this.pagination = response.body;

          return this.pagination;
        })
      );
  }

  setShopParams(params: ShopParams) {
    this.shopParams = params;
  }

  getShopParams() {
    return this.shopParams;
  }

  getProduct(id: number) {
    //  Observable<IProduct> is the return type

    const product = this.products.find((p) => p.id === id); //this is added just for performance, to store the products in the client side
    if (product) {
      return of(product); //this is equal to observable of,
    }

    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }
  getBrands() {
    if (this.brands.length > 0)
      //this will be returned from if there is a cached one
      return of(this.brands);
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands').pipe(
      map((response) => {
        this.brands = response;
        return response;
      })
    );
  }

  getTypes() {
    if (this.types.length > 0) {
      //this will be returned from  if there is a cached one
      return of(this.types);
    }
    //this is the same as return this.http.get<IType[]>(this.baseUrl + 'products/types').pipe but we are caching it for future
    return this.http.get<IType[]>(this.baseUrl + 'products/types').pipe(
      map((response) => {
        this.types = response;
        return response;
      })
    );
  }
}
