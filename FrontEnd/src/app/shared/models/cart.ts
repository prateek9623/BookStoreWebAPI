import { Book, BookCount } from './book';
import { Observable } from 'rxjs';

export class Cart {
    items: BookCount[] = [];

    constructor(private itemsMap: BookCount[]) {
        this.items = itemsMap;
    }

    getQuantity(book: Book): Number {
        let quantity = 0;
        this.items.forEach((item: BookCount) => {
            if (item._Book.BookId === book.BookId) {
                quantity = item.BookQuantity;
            }
        });
        return quantity;
    }
    get totalPrice() {
        let sum = 0;
        this.items.forEach((item: BookCount) => {
            sum += item._Book.BookCost * item.BookQuantity;
        });
        return sum;
    }

    totalItemsCount(): Number {
        let quantity = 0;
        this.items.forEach((item: BookCount) => {
            quantity += item.BookQuantity;
        });
        return quantity;
    }
}
