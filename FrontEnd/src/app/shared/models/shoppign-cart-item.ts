import { Book } from './book';

export class ShoppingCartItem {
    book: Book;
    quantity: number;

    constructor(init?: Partial<ShoppingCartItem>) {
        Object.assign(this, init);
    }

    get totalPrice() { return this.book.cost * this.quantity; }
}
