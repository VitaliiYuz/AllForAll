import { Component } from '@angular/core';
import { PopularCategoryService } from './popular-category.service';
import { PopularManufacturerService } from './popular-manufacturer.service';
import { PopularProductService } from './popular-product.service';
import { Category } from '../models/category.model';
import { Manufacturer } from '../models/manufacturer.model';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  popularCategories: Category[] = [
    new Category(1, "Electronics", "Lorem ipsum dolor sit amet.", "../../../assets/logo.png"),
    new Category(2, "Clothing", "Consectetur adipiscing elit.", "../../../assets/logo.png"),
    new Category(3, "Books", "Sed do eiusmod tempor incididunt.", "../../../assets/logo.png"),
    new Category(4, "Electronics", "Lorem ipsum dolor sit amet.", "../../../assets/logo.png"),
    new Category(5, "Clothing", "Consectetur adipiscing elit.", "../../../assets/logo.png"),
    new Category(6, "Books", "Sed do eiusmod tempor incididunt.", "../../../assets/logo.png"),
    // Add more dummy data as needed
];
  popularManufacturers: Manufacturer[] = [
    new Manufacturer(1, "ABC Inc.", "USA", "Lorem ipsum dolor sit amet.", "../../../assets/logo.png"),
    new Manufacturer(2, "XYZ Ltd.", "Canada", "Consectetur adipiscing elit.", "../../../assets/logo.png"),
    new Manufacturer(3, "123 Corp.", "UK", "Sed do eiusmod tempor incididunt.", "../../../assets/logo.png"),
    new Manufacturer(4, "ABC Inc.", "USA", "Lorem ipsum dolor sit amet.", "../../../assets/logo.png"),
    new Manufacturer(5, "XYZ Ltd.", "Canada", "Consectetur adipiscing elit.", "../../../assets/logo.png"),
    new Manufacturer(6, "123 Corp.", "UK", "Sed do eiusmod tempor incididunt.", "../../../assets/logo.png"),
    // Add more dummy data as needed
];
  popularProducts: Product[] = [
    new Product(1, "Product 1", 1, 1, new Date(), "../../../assets/logo.png", true, "USA", 1),
    new Product(2, "Product 2", 2, 2, new Date(), "../../../assets/logo.png", true, "Canada", 2),
    new Product(3, "Product 3", 3, 3, new Date(), "../../../assets/logo.png", false, "UK", 3),
    new Product(4, "Product 4", 1, 2, new Date(), "../../../assets/logo.png", true, "USA", 4),
    new Product(5, "Product 5", 2, 3, new Date(), "../../../assets/logo.png", false, "Canada", 5),
    new Product(6, "Product 6", 3, 1, new Date(), "../../../assets/logo.png", true, "UK", 6)
    // Add more dummy data as needed
];

  constructor(
    private popularCategoryService: PopularCategoryService,
    private popularManufacturerService: PopularManufacturerService,
    private popularProductService: PopularProductService
  ) { }

  ngOnInit(): void {
    // this.getPopularCategories();
    // this.getPopularManufacturers();
    // this.getPopularProducts();
  }

  // getPopularCategories(): void {
  //   this.popularCategoryService.getPopularCategories().subscribe(categories => {
  //     this.popularCategories = categories;
  //   });
  // }

  // getPopularManufacturers(): void {
  //   this.popularManufacturerService.getPopularManufacturers().subscribe(manufacturers => {
  //     this.popularManufacturers = manufacturers;
  //   });
  // }

  // getPopularProducts(): void {
  //   this.popularProductService.getPopularProducts().subscribe(products => {
  //     this.popularProducts = products;
  //   });
  // }
}
