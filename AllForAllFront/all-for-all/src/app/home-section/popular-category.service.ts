import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Category } from '../models/category.model';

@Injectable({
  providedIn: 'root'
})
export class PopularCategoryService {
  private popularCategoriesUrl = 'http://localhost:5158/api/categories/popular'; 

  constructor(private http: HttpClient) { }

  getPopularCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.popularCategoriesUrl);
  }
}
