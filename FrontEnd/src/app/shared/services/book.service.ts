import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Book, Author, Publisher, Genre } from '../models/book';
import { UserAuthenticationService } from './user-authentication.service';
import { User } from '../models/user';
import { CookieService } from 'ngx-cookie-service';
import { Observable, of, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  readonly rootUrl = environment.webApiUrl;
  private bookList = new Subject<Book[]>();
  authorList = new Subject<string[]>();
  publisherList = new Subject<string[]>();
  genreList = new Subject<string[]>();

  constructor(private http: HttpClient, private userAuthenticationService: UserAuthenticationService,
    private cookieService: CookieService) { }

  getAuthors() {
    this.http.get(this.rootUrl + '/Authors/').subscribe((data: string[]) => {
      this.authorList.next(data);
      localStorage.setItem('authorsList', JSON.stringify(data));
    });
    return this.authorList.asObservable();
  }
  getPublishers() {
    this.http.get(this.rootUrl + '/Publishers/').subscribe((data: string[]) => {
      this.publisherList.next(data);
      localStorage.setItem('publisherList', JSON.stringify(data));
    });
    return this.publisherList.asObservable();
  }
  getGenres() {
    this.http.get(this.rootUrl + '/Genres/').subscribe((data: string[]) => {
      this.genreList.next(data);
      localStorage.setItem('genreList', JSON.stringify(data));
    });
    return this.genreList.asObservable();
  }

  getBooks() {
    this.http.get(this.rootUrl + '/Book/').subscribe((data: Book[]) => {
      localStorage.setItem('bookList', JSON.stringify(data)); this.bookList.next(data);
    });
    return this.bookList.asObservable();
  }

  getBook(id: String): Book {
    const books = JSON.parse(localStorage.getItem('bookList'));
    return books.find(x => x.BookId === id);
  }

  addBook(book: Book) {
    const data = {
      title: book.BookTitle,
      genre: book.BookGenre.GenreName,
      author: book.BookAuthor.AuthorName,
      description: book.BookDescription,
      publisher: book.BookPublisher.PublisherName,
      imageurl: book.BookThumb,
      cost: book.BookCost,
      stock: book.BookStock
    };
    this.http.post(this.rootUrl + '/Book', data,
      { headers: new HttpHeaders().set('Authorization', 'Bearer ' + this.cookieService.get('sessionId')) })
      .subscribe((x: User) => {
        localStorage.setItem('user', JSON.stringify(x));
        this.cookieService.deleteAll();
        this.cookieService.set('sessionId', x.SessionId, 2, '/', 'localhost');

        const books: Book[] = JSON.parse(localStorage.getItem('bookList'));
        books.push(book);
        this.bookList.next(books);
        localStorage.setItem('bookList', JSON.stringify(books));

      });
  }

  updateBook(book: Book) {
    const data = {
      id: book.BookId,
      title: book.BookTitle,
      genre: book.BookGenre.GenreName,
      author: book.BookAuthor.AuthorName,
      description: book.BookDescription,
      publisher: book.BookPublisher.PublisherName,
      imageurl: book.BookThumb,
      cost: book.BookCost,
      stock: book.BookStock
    };
    this.http.put(this.rootUrl + '/Book', data,
      { headers: new HttpHeaders().set('Authorization', 'Bearer ' + this.cookieService.get('sessionId')) })
      .subscribe((x: User) => {
        localStorage.setItem('user', JSON.stringify(x));
        this.cookieService.deleteAll();
        this.cookieService.set('sessionId', x.SessionId, 2, '/', 'localhost');

        const books = JSON.parse(localStorage.getItem('bookList'));
        books[books.indexOf(this.getBook(book.BookId))] = book;
        this.bookList.next(books);
        localStorage.setItem('bookList', JSON.stringify(books));
        // console.log(x); this.getBooks();
      });
  }
}
