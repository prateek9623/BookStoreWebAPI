export interface Genre {
    GenreName: string;
}

export interface Author {
    AuthorName: string;
}

export interface Publisher {
    PublisherName: string;
}

export interface Book {
    BookId?: string;
    BookTitle?: string;
    BookGenre?: Genre;
    BookAuthor?: Author;
    BookDescription?: string;
    BookPublisher?: Publisher;
    BookRating?: number;
    BookCost?: number;
    BookThumb?: string;
    BookStock?: number;
}

export interface BookCount {
    BookItem: Book;
    Count: number;
}
