using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class BookCount
    {
        public Book BookItem { get; set; }
        public int Count { get; set; }

        public BookCount(Book _book, int _count)
        {
            BookItem = _book;
            Count = _count;
        }
    }
}