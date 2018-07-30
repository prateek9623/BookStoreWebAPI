using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class BookCount
    {
        public Book _Book { get; set; }
        public int BookQuantity { get; set; }

        public BookCount(Book _book, int _count)
        {
            _Book = _book;
            BookQuantity = _count;
        }
    }
}