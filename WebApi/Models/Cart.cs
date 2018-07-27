using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Cart
    {
        public Book _Book { get; set;}
        public int BookQuantity { get; set; }

        public Cart(Book book , int bookQuantity)
        {
            this._Book = book;
            this.BookQuantity = bookQuantity;
        }
    }
}