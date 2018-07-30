import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CookieService } from 'ngx-cookie-service';

import { MatTabsModule, MatPaginatorModule, MatSortModule, MatSnackBarModule } from '@angular/material';
import { MatListModule } from '@angular/material/list';
import { MatCardModule, MatIconModule } from '@angular/material';
import { MatFormFieldModule, MatInputModule } from '@angular/material';
import { MatButtonModule } from '@angular/material/button';
import { MatAutocompleteModule } from '@angular/material';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatBadgeModule } from '@angular/material/badge';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatStepperModule } from '@angular/material/stepper';

import { AppComponent } from './app.component';
import { BsNavbarComponent } from './bs-navbar/bs-navbar.component';
import { HomeComponent } from './home/home.component';
import { BooksComponent } from './books/books.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { CheckOutComponent } from './check-out/check-out.component';
import { OrderSuccessComponent } from './order-success/order-success.component';
import { MyOrdersComponent } from './my-orders/my-orders.component';
import { AdminBooksComponent } from './admin/admin-books/admin-books.component';
import { AdminOrdersComponent } from './admin/admin-orders/admin-orders.component';
import { LoginComponent } from './login/login.component';
import { EqualValidator } from './shared/equal-validator.directive';
import { HttpClientModule } from '@angular/common/http';
import { UserAuthenticationService } from './shared/services/user-authentication.service';
import { AuthGuardService } from './shared/services/auth-guard.service';
import { AdminAuthGuardService } from './shared/services/admin-auth-guard.service';
import { BookFormComponent } from './admin/book-form/book-form.component';
import { BookCardComponent } from './book-card/book-card.component';
import { ShoppingCartSummaryComponent } from './shopping-cart-summary/shopping-cart-summary.component';
import { ShippingFormComponent } from './shipping-form/shipping-form.component';
import { BookComponent } from './book/book.component';
import { CartService } from './shared/services/cart.service';
import { OrderService } from './shared/services/order.service';
import { BookService } from './shared/services/book.service';
import { MyOrderComponent } from './my-order/my-order.component';
// import { RegisterComponent } from './user-authentication/register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    BsNavbarComponent,
    HomeComponent,
    BooksComponent,
    ShoppingCartComponent,
    CheckOutComponent,
    OrderSuccessComponent,
    MyOrdersComponent,
    AdminBooksComponent,
    AdminOrdersComponent,
    LoginComponent,
    EqualValidator,
    BookFormComponent,
    BookCardComponent,
    ShoppingCartSummaryComponent,
    ShippingFormComponent,
    BookComponent,
    MyOrderComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule, MatIconModule, MatTabsModule, MatCardModule, MatButtonModule, MatFormFieldModule, MatInputModule,
    MatTableModule, MatPaginatorModule, MatSortModule, MatSnackBarModule, MatListModule,
    MatAutocompleteModule, MatBadgeModule, MatExpansionModule, MatStepperModule,
    HttpClientModule,
    NgbModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: BooksComponent },
      { path: 'books', component: BooksComponent },
      { path: 'login', component: LoginComponent },
      { path: 'book', component: BookComponent },

      { path: 'my/order', component: MyOrderComponent, canActivate: [AuthGuardService] },
      { path: 'shopping-cart', component: ShoppingCartComponent, canActivate: [AuthGuardService] },
      { path: 'check-out', component: CheckOutComponent, canActivate: [AuthGuardService] },
      { path: 'order-success', component: OrderSuccessComponent, canActivate: [AuthGuardService] },
      { path: 'my/orders', component: MyOrdersComponent, canActivate: [AuthGuardService] },
      { path: 'admin/orders', component: AdminOrdersComponent, canActivate: [AuthGuardService, AdminAuthGuardService] },
      { path: 'admin/books/new', component: BookFormComponent, canActivate: [AuthGuardService, AdminAuthGuardService] },
      { path: 'admin/book/:id', component: BookFormComponent, canActivate: [AuthGuardService, AdminAuthGuardService] },
      { path: 'admin/books', component: AdminBooksComponent, canActivate: [AuthGuardService, AdminAuthGuardService] }
    ])
  ],
  providers: [UserAuthenticationService, CartService, OrderService, BookService, AuthGuardService, AdminAuthGuardService, CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
