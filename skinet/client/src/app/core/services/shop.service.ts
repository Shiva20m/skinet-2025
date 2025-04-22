import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Envelope } from '../../shared/models/envelope';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/product';
import { ShopPramas } from '../../shared/models/ShopPramas';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  types: string[]=[];
  brands:string[]=[];
  
  getProducts(shopParams:ShopPramas)
  {
    let params = new HttpParams();
    if(shopParams.brands.length>0)
    {
      params = params.append('brands', shopParams.brands.join(','));

    }
    if(shopParams.sort)
    {
      params = params.append('sort', shopParams.sort);
    }
    if(shopParams.types.length>0)
    {
      params = params.append('types', shopParams.types.join(','));
    }
    if(shopParams.search)
    {
      params = params.append('search', shopParams.search);
    }
    params = params.append('pageSize', shopParams.pageSize);
    params = params.append('pageIndex', shopParams.pageNumber);
    return this.http.get<Envelope<Pagination<Product>>>(this.baseUrl + 'products', {params});
  }
  // get product by id
  getProduct(id:number)
  {
    return this.http.get<Product>(this.baseUrl + 'products/' + id);
  }
  // applying the filter
  getBrands()
  {
    if(this.brands.length>0)return;
    return this.http.get<string[]>(this.baseUrl + 'products/brands').subscribe({
      next:response=>this.brands=response
    })
  }

  getTypes()
  {
    if(this.types.length>0) return;
    return this.http.get<string[]>(this.baseUrl + 'products/types').subscribe({
      next:response=>this.types=response
    })
  }
}
