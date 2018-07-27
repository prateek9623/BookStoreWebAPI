import { Component, OnInit } from '@angular/core';
import { Book, Author, Genre, Publisher } from '../../shared/models/book';
import { BookService } from '../../shared/services/book.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css']
})
export class BookFormComponent implements OnInit {
  updateForm: boolean;
  // genreField = new FormControl();
  book: Book;
  authorList;
  genreList;
  publisherList;
  // filteredGenre: Observable<string[]>;

  constructor(private bookService: BookService, private router: Router, private route: ActivatedRoute, private snackBar: MatSnackBar) {
    this.updateForm = false;
    const id = this.route.snapshot.paramMap.get('id');
    this.book = {
      // BookTitle: '',
      BookAuthor: { AuthorName: '' },
      // // BookCost: 0,
      // BookDescription: '',
      BookGenre: { GenreName: '' },
      BookPublisher: { PublisherName: '' },
      // // BookRating: 0,
      // // BookStock: 0,
      // BookThumb: ''
    };
    this.authorList = this.bookService.getAuthors();
    this.genreList = this.bookService.getGenres();
    this.publisherList = this.bookService.getPublishers();
    // this.filteredGenre = this.genreField.valueChanges.pipe(
    //   startWith(''),
    //   map(value => this.genreList.filter(option => option.toLowerCase().indexOf(value.toLowerCase()) === 0))
    // );
    if (id) { this.book = bookService.getBook(id); this.updateForm = true; }
  }

  ngOnInit() {
    // this.book = {
    //   // BookTitle: '',
    //   BookAuthor: { AuthorName: '' },
    //   // // BookCost: 0,
    //   // BookDescription: '',
    //   BookGenre: { GenreName: '' },
    //   BookPublisher: { PublisherName: '' },
    //   // // BookRating: 0,
    //   // // BookStock: 0,
    //   // BookThumb: ''
    // };
    // this.bookService.getBooksProperty();
    // this.authorList = JSON.parse(localStorage.getItem('authorsList')) as string[];
    // this.genreList = JSON.parse(localStorage.getItem('genreList')) as string[];
    // this.publisherList = JSON.parse(localStorage.getItem('publisherList')) as string[];
    // // this.filteredGenre = this.genreField.valueChanges.pipe(
    // //   startWith(''),
    // //   map(value => this.genreList.filter(option => option.toLowerCase().indexOf(value.toLowerCase()) === 0))
    // // );
  }

  save() {
    // console.log(this.book);
    if (!this.updateForm) {
      this.bookService.addBook(this.book);
      this.snackBar.open(this.book.BookTitle + ' Added', null, { duration: 3000 });
    } else {
      this.bookService.updateBook(this.book);
      this.snackBar.open(this.book.BookTitle + ' Updated', null, { duration: 3000 });
    }
    this.router.navigateByUrl('/admin/books');
  }

}
