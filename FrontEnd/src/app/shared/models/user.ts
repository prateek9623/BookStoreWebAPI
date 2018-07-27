import { BookCount } from './book';
import { Order } from './order';

export interface User {
    SessionId: string;
    CartBookList: BookCount[];
    OrderList: Order[];
    isAdmin: boolean;
    UserName: string;
    FirstName: string;
    LastName: string;
    Email: string;
}
