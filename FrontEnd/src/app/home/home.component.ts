import { Component, OnInit } from '@angular/core';
import { Book } from '../shared/models/book';
import { BookService } from '../shared/services/book.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  bookList: Book[];
  constructor(private bookService: BookService) { }

  ngOnInit() {
    this.bookService.getBooks();
    this.bookList = JSON.parse(localStorage.getItem('bookList'));
  }

}
