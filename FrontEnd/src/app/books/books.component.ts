import { Component, OnInit } from '@angular/core';
import { BookService } from '../shared/services/book.service';
import { ActivatedRoute } from '@angular/router';
import { Book } from '../shared/models/book';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {
  books: Book[] = [];
  filteredBooks: Book[] = [];
  genres$;
  publishers$;
  genre: string;
  publisher: string;

  constructor(private booksService: BookService, route: ActivatedRoute) {
    booksService.getBooks().pipe(switchMap(books => {
      this.books = books;
      return route.queryParamMap;
    })).subscribe(params => {
      this.genre = params.get('genre');
      this.publisher = params.get('publisher');
      if (this.genre) {
        this.filteredBooks = this.books.filter(b => b.BookGenre.GenreName === this.genre);
      } else if (this.publisher) {
        this.filteredBooks = this.books.filter(b => b.BookPublisher.PublisherName === this.publisher);
      } else {
        this.filteredBooks = this.books;
      }
    });
    this.genres$ = booksService.getGenres();
    this.publishers$ = booksService.getPublishers();
    this.filteredBooks = this.books;
  }

  ngOnInit() {
  }

}
