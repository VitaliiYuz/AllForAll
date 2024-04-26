export class Product {
    constructor(
        public productId: number,
        public name: string,
        public categoryId: number,
        public manufacturerId: number,
        public creationDate: Date,
        public productPhotoLink: string,
        public isVerified: boolean,
        public country: string,
        public userId: number
      ) {}
}