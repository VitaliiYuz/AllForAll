import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Manufacturer } from '../models/manufacturer.model';

@Injectable({
  providedIn: 'root'
})
export class PopularManufacturerService {
  private popularManufacturersUrl = 'http://localhost:5158/api/manufacturers/popular'; 

  constructor(private http: HttpClient) { }

  getPopularManufacturers(): Observable<Manufacturer[]> {
    return this.http.get<Manufacturer[]>(this.popularManufacturersUrl);
  }
}
