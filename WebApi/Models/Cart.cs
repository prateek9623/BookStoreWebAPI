using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Cart
    {
        public Book _Book { get; set;}
        public string BookQuantity { get; set; }

        public Cart(Book book , string bookQuantity)
        {
            this._Book = book;
            this.BookQuantity = bookQuantity;
        }
    }
}