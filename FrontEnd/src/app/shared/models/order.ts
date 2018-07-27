import { BookCount } from './book';
import { Address } from './Address';

export interface Order {
    OrderedItems: BookCount[];
    ReceiverName: string;
    ReceiverAddr: Address;
    ReceiverContactNo: string;
    OrderShipped: boolean;
    OrderTransactionId: string;
}

