import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators'; 
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class CountryService {
  private productUrl = 'http://localhost:5158/api/products';
  private countriesSubject: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]);
  public countries$: Observable<string[]> = this.countriesSubject.asObservable();

  constructor(private http: HttpClient) { 
    this.fetchCountries(); 
  }

  private fetchCountries(): void {
    this.http.get<Product[]>(this.productUrl).pipe(
      map(products => {
        const countrySet = new Set(products.map(product => product.country));
        return Array.from(countrySet);
      })
    ).subscribe({
      next: countries => {
        this.countriesSubject.next(countries); 
      },
      error: error => {
        console.error('Error fetching countries in service:', error);
      }
    });
  }
}
