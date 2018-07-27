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

            IList<string> genreList = dbCon.getGenre();
            IList<string> publisherList = dbCon.getPublisher();
            IList<string> authorList = dbCon.getAuthors();

            //checking if genre already exist
            bool checkGenre = false;
            foreach (string genre in genreList)
                if (genre.Equals(book.BookGenre.GenreName))
                    checkGenre = true;

            //if not add genre
            if (!checkGenre)
                dbCon.addGenre(book.BookGenre);

            //checking if publisher already exist
            bool checkPublisher = false;
            foreach (string publisher in publisherList)
                if (publisher.Equals(book.BookPublisher.PublisherName))
                    checkPublisher = true;

            //if not add publisher
            if (!checkPublisher)
                dbCon.addPublisher(book.BookPublisher);

            //checking if author already exist
            bool checkAuthor = false;
            foreach (string author in authorList)
                if (author.Equals(book.BookAuthor.AuthorName))
                    checkAuthor = true;

            //if not add author
            if (!checkAuthor)
                dbCon.addAuthor(book.BookAuthor);

            if(dbCon.addBook(book))
                status = true;

            return status;
        }

        public bool updateBook(Book book)
        {
            bool status = false;

            IList<string> genreList = dbCon.getGenre();
            IList<string> publisherList = dbCon.getPublisher();
            IList<string> authorList = dbCon.getAuthors();

            //checking if genre already exist
            bool checkGenre = false;
            foreach (string genre in genreList)
                if (genre.Equals(book.BookGenre.GenreName))
                    checkGenre = true;

            //if not add genre
            if (!checkGenre)
                dbCon.addGenre(book.BookGenre);

            //checking if publisher already exist
            bool checkPublisher = false;
            foreach (string publisher in publisherList)
                if (publisher.Equals(book.BookPublisher.PublisherName))
                    checkPublisher = true;

            //if not add publisher
            if (!checkPublisher)
                dbCon.addPublisher(book.BookPublisher);

            //checking if author already exist
            bool checkAuthor = false;
            foreach (string author in authorList)
                if (author.Equals(book.BookAuthor.AuthorName))
                    checkAuthor = true;

            //if not add author
            if (!checkAuthor)
                dbCon.addAuthor(book.BookAuthor);

            if (dbCon.updateBook(book))
                status = true;

            return status;
        }
    }
}