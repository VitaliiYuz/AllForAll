import { Component } from '@angular/core';
import { Product } from '../models/product.model';
import { Feedback } from '../models/feedback.model';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
  product = new Product(1, "some name", 2, 2, new Date(), "../../../assets/drink.jpg", true, "USA", 5);
  dummyFeedbacks: Feedback[] = [
    new Feedback(1, 1, 1, 4, "Great product!", new Date()),
    new Feedback(2, 2, 2, 3, "Needs improvement.", new Date()),
    new Feedback(3, 1, 3, 5, "Awesome!", new Date()),
    new Feedback(4, 1, 1, 4, "Great product!", new Date()),
    new Feedback(5, 2, 2, 3, "Needs improvement.", new Date()),
    new Feedback(6, 1, 3, 5, "Awesome!", new Date()),
    // Add more dummy data as needed
];
}
