import { shopParams } from './../shared/Models/shopParams';
import { IType } from './../shared/Models/ProductType';
import { IBrand } from './../shared/Models/brand';
import { IPagination } from '../shared/Models/Pagination';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {map} from 'rxjs/operators';
import { IProduct } from '../shared/Models/Product';



@Injectable({
  providedIn: 'root'  // means it  is provided in the app.module in the providers []
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient){}

//  HttpParams values are array of values. When you set the value, it will override all the values in the array.
//  When you append the value, it will push new values on the existing array.

    getProducts(shopParams: shopParams)
  {
        let params=new HttpParams ();

        if(shopParams.brandId!==0){
          params=params.append('brandId',shopParams.brandId.toString());
        }

        if(shopParams.typeId!==0){
          params=params.append('typeId',shopParams.typeId.toString());
        }

        if(shopParams.search){
          params=params.append('search',shopParams.search)
        }

          params=params.append('sort',shopParams.sort) //sort is  already  a string and by default it is selected by name no need for if
          params=params.append('pageIndex',shopParams.pageNumber.toString());
          params=params.append('pageIndex',shopParams.pageSize.toString());

        return this.http.get<IPagination>(this.baseUrl + 'products',{observe:'response',params})
        .pipe(                //pipe is a rapper and inside we have chained rxjs  operator e.g instead of map delay(1000) could be done
          map(response =>{
            return response.body;
          })
        );
   }

   getProduct(id: number){
    //  get<IProduct> is the return type
     return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
   }
   getBrands(){
     return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
   }


   getTypes(){
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }
}
