import { Component, OnInit } from '@angular/core';
import { PopularCategoryService } from './popular-category.service';
import { PopularManufacturerService } from './popular-manufacturer.service';
import { PopularProductService } from './popular-product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  popularCategories: Category[];
  popularManufacturers: Manufacturer[];
  popularProducts: Product[];

  constructor(
    private popularCategoryService: PopularCategoryService,
    private popularManufacturerService: PopularManufacturerService,
    private popularProductService: PopularProductService
  ) { }

  ngOnInit(): void {
    this.getPopularCategories();
    this.getPopularManufacturers();
    this.getPopularProducts();
  }

  getPopularCategories(): void {
    this.popularCategoryService.getPopularCategories().subscribe(categories => {
      this.popularCategories = categories;
    });
  }

  getPopularManufacturers(): void {
    this.popularManufacturerService.getPopularManufacturers().subscribe(manufacturers => {
      this.popularManufacturers = manufacturers;
    });
  }

  getPopularProducts(): void {
    this.popularProductService.getPopularProducts().subscribe(products => {
      this.popularProducts = products;
    });
  }
}
