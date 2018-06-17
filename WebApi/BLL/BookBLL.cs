using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using WebApi.Models;

namespace WebApi.BLL
{
    public class BookBLL
    {
        private static BookBLL obj;
        private DBConnection dbCon;

        private BookBLL()
        {
            dbCon = DBConnection.getObject();   
        }

        public static BookBLL getObject()
        {
            if (obj == null)
                obj = new BookBLL();
            return obj;
        }

        public bool addBook(Book book)
        {
            bool status = false;

            IList<Genre> genreList = dbCon.getGenre();
            IList<Publisher> publisherList = dbCon.getPublisher();
            IList<Author> authorList = dbCon.getAuthors();

            //checking if genre already exist
            bool checkGenre = false;
            foreach (Genre genre in genreList)
                if (genre.GenreName.Equals(book.BookGenre.GenreName))
                    checkGenre = true;

            //if not add genre
            if (!checkGenre)
                dbCon.addGenre(book.BookGenre);

            //checking if publisher already exist
            bool checkPublisher = false;
            foreach (Publisher publisher in publisherList)
                if (publisher.PublisherName.Equals(book.BookPublisher.PublisherName))
                    checkPublisher = true;

            //if not add publisher
            if (!checkPublisher)
                dbCon.addPublisher(book.BookPublisher);

            //checking if author already exist
            bool checkAuthor = false;
            foreach (Author author in authorList)
                if (author.AuthorName.Equals(book.BookAuthor.AuthorName))
                    checkAuthor = true;

            //if not add author
            if (!checkAuthor)
                dbCon.addAuthor(book.BookAuthor);

            if(dbCon.addBook(book))
                status = true;

            return status;
        }
    }
}