import { Component, OnInit, ViewChild } from '@angular/core';
import { BookService } from '../../shared/services/book.service';
import { Book } from '../../shared/models/book';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-admin-books',
  templateUrl: './admin-books.component.html',
  styleUrls: ['./admin-books.component.css']
})
export class AdminBooksComponent implements OnInit {
  bookList$: Observable<Book[]>;
  bookList: Book[];
  displayedColumns: string[] = ['id', 'title', 'genre', 'author', 'publisher', 'cost', 'stock', 'editLink'];
  bookListDataSource = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private bookService: BookService) { }

  ngOnInit() {
    this.bookList$ = this.bookService.getBooks();
    this.bookList$.subscribe(x => this.bookListDataSource.data = x);
    this.bookListDataSource.paginator = this.paginator;
    this.bookListDataSource.sort = this.sort;
  }

  applyFilter(filterValue: string) {
    this.bookListDataSource.filter = filterValue.trim().toLowerCase();

    if (this.bookListDataSource.paginator) {
      this.bookListDataSource.paginator.firstPage();
    }
  }

}
