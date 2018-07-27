using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Book
    {
        public string BookId{ get; set; }
        public string BookTitle { get; set; }
        public Genre BookGenre { get; set; }
        public Author BookAuthor { get; set; }
        public string BookDescription { get; set; }
        public Publisher BookPublisher { get; set; }
        public int BookRating { get; set; }
        public double BookCost { get; set; }
        public string BookThumb { get; set; }
        public int BookStock { get; set; }

        public Book() { }
        
        public Book( string BookTitle, Genre _Genre, Author _Author, string Description, Publisher _Publisher, 
            int Rating, double Cost, string Thumb, int Stock)
        {
            this.BookTitle = BookTitle;
            this.BookGenre = _Genre;
            this.BookAuthor = _Author;
            this.BookDescription = Description;
            this.BookPublisher = _Publisher;
            this.BookRating = Rating;
            this.BookCost = Cost;
            this.BookThumb = Thumb;
            this.BookStock = Stock;
        }
        public Book(string BookId,string BookTitle, Genre _Genre, Author _Author, string Description, Publisher _Publisher,
           int Rating, double Cost, string Thumb, int Stock)
        {
            this.BookId = BookId;
            this.BookTitle = BookTitle;
            this.BookGenre = _Genre;
            this.BookAuthor = _Author;
            this.BookDescription = Description;
            this.BookPublisher = _Publisher;
            this.BookRating = Rating;
            this.BookCost = Cost;
            this.BookThumb = Thumb;
            this.BookStock = Stock;
        }

        public Book(string BookId, string BookTitle, Genre _Genre, Author _Author, string Description, Publisher _Publisher,
             double Cost, string Thumb, int Stock)
        {
            this.BookId = BookId;
            this.BookTitle = BookTitle;
            this.BookGenre = _Genre;
            this.BookAuthor = _Author;
            this.BookDescription = Description;
            this.BookPublisher = _Publisher;
            this.BookCost = Cost;
            this.BookThumb = Thumb;
            this.BookStock = Stock;
        }
        public Book(string BookTitle, Genre _Genre, Author _Author, string Description, Publisher _Publisher,
           double Cost, string Thumb, int Stock)
        {
            this.BookTitle = BookTitle;
            this.BookGenre = _Genre;
            this.BookAuthor = _Author;
            this.BookDescription = Description;
            this.BookPublisher = _Publisher;
            this.BookCost = Cost;
            this.BookStock = Stock;
            this.BookThumb = Thumb;
        }
    }
}
