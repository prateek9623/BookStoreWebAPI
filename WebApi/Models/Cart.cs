using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Cart
    {
        public string BookId { get; set;}
        public string BookQuantity { get; set; }

        public Cart(string bookId , string bookQuantity)
        {
            this.BookId = bookId;
            this.BookQuantity = bookQuantity;
        }
    }
}