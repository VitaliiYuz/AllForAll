import { Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-products-section',
  templateUrl: './products-section.component.html',
  styleUrl: './products-section.component.css'
})
export class ProductsSectionComponent {
  constructor(private route: ActivatedRoute,) { }

  section = 'Drinks';
  filterOne = 'Manufacturers';
  filterTwo = 'Countries';

  manufacturers: { id: number, name: string }[] = [
    { id: 1, name: 'Manufacturer 1' },
    { id: 2, name: 'Manufacturer 2' },
    { id: 3, name: 'Manufacturer 3' }
    // Add more manufacturers as needed
  ];

  countries: { id: number, name: string }[] = [
    { id: 1, name: 'Country 1' },
    { id: 2, name: 'Country 2' },
    { id: 3, name: 'Country 3' }
    // Add more countries as needed
  ];

  items = [
    {
      id: 1,
      photoUrl: '../../../assets/logo.png',
      title: 'Item 1',
      manufacturer: 'Manufacturer A',
      rating: 4
    },
    {
      id: 2,
      photoUrl: '../../../assets/logo.png',
      title: 'Item 2',
      manufacturer: 'Manufacturer B',
      rating: 5
    },
    {
      id: 3,
      photoUrl: '../../../assets/logo.png',
      title: 'Item 3',
      manufacturer: 'Manufacturer C',
      rating: 3
    },
    {
      id: 4,
      photoUrl: '../../../assets/logo.png',
      title: 'Item 4',
      manufacturer: 'Manufacturer A',
      rating: 4
    },
    {
      id: 5,
      photoUrl: '../../../assets/logo.png',
      title: 'Item 5',
      manufacturer: 'Manufacturer B',
      rating: 5
    },
    {
      id: 6,
      photoUrl: '../../../assets/logo.png',
      title: 'Item 6',
      manufacturer: 'Manufacturer C',
      rating: 3
    },
    {
      id: 7,
      photoUrl: '../../../assets/logo.png',
      title: 'Item 7',
      manufacturer: 'Manufacturer A',
      rating: 4
    },
    {
      id: 8,
      photoUrl: '../../../assets/logo.png',
      title: 'Item 8',
      manufacturer: 'Manufacturer B',
      rating: 5
    },
    {
      id: 9,
      photoUrl: '../../../assets/logo.png',
      title: 'Item 9',
      manufacturer: 'Manufacturer C',
      rating: 3
    },
    {
      id: 10,
      photoUrl: '../../../assets/logo.png',
      title: 'Item 10',
      manufacturer: 'Manufacturer A',
      rating: 4
    },
    {
      id: 11,
      photoUrl: '../../../assets/logo.png',
      title: 'Item 11',
      manufacturer: 'Manufacturer B',
      rating: 5
    },
    {
      id: 12,
      photoUrl: '../../../assets/logo.png',
      title: 'Item 12',
      manufacturer: 'Manufacturer C',
      rating: 3
    },
    // Add more items as needed
  ];

  ngOnInit() {
    // this.route.params.subscribe((params: Params) => {
    //   const id = +params['id'];
    //   this.section =
    // });
  }

  currentPage = 1;
  itemsPerPage = 9;

  get startIndex() {
    return (this.currentPage - 1) * this.itemsPerPage;
  }

  get endIndex() {
    return Math.min(this.startIndex + this.itemsPerPage - 1, this.items.length - 1);
  }

  get paginatedItems() {
    return this.items.slice(this.startIndex, this.endIndex + 1);
  }

  onPageChange(pageNumber: number) {
    this.currentPage = pageNumber;
  }
}
