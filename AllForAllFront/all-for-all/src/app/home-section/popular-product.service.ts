import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class PopularProductService {
  private popularProductsUrl = 'http://localhost:5158/api/products/popular'; 

  constructor(private http: HttpClient) { }

  getPopularProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.popularProductsUrl);
  }
}
