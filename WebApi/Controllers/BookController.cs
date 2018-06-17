using DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;
using WebApi.BLL;

namespace WebApi.Controllers
{
    public class BookController : ApiController
    {
        private DBConnection DBCon;
        private BookBLL bookCon;

        BookController()
        {
            DBCon = DBConnection.getObject();
            bookCon = BookBLL.getObject();
        }

        [HttpGet]
        [Route("api/Genres/")]
        [ResponseType(typeof(Genre[]))]
        public HttpResponseMessage Genres()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, DBCon.getGenre().ToArray<Genre>());
        }

        [HttpGet]
        [Route("api/Publishers/")]
        [ResponseType(typeof(Publisher[]))]
        public HttpResponseMessage Publishers()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, DBCon.getPublisher().ToArray<Publisher>());
        }

        [HttpGet]
        [Route("api/Authors/")]
        [ResponseType(typeof(Author[]))]
        public HttpResponseMessage Authors()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, DBCon.getAuthors().ToArray<Author>());
        }

        [HttpGet]
        [Route("api/Book/")]
        [ResponseType(typeof(Book[]))]
        public HttpResponseMessage Book()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, DBCon.getBooks().ToArray<Book>());
        }

        [HttpGet]
        [Route("api/Book/{bookName}")]
        [ResponseType(typeof(Book[]))]
        public HttpResponseMessage Book(string bookName)
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, DBCon.getBookByName(bookName).ToArray<Book>());
        }

        [HttpPost]
        [Route("api/Book/")]
        [ResponseType(typeof(User))]
        public HttpResponseMessage registerBook(JObject obj)
        {
            string username = obj.Value<String>("UserName");
            string sessionid = obj.Value<String>("SessionId");
            User user = new User(username,sessionid);
            if (DBCon.checkSession(user) && DBCon.validateAdmin(user))
            {
                string bookTitle = obj.Value<String>("title");
                string bookGenre = obj.Value<String>("genre");
                string bookAuthor = obj.Value<String>("author");
                string bookDescription = obj.Value<String>("description");
                string bookPublisher = obj.Value<String>("publisher");
                double bookCost;
                int bookStock;
                try
                {
                    bookCost = double.Parse(obj.Value<String>("cost"));
                    bookStock = int.Parse(obj.Value<String>("stock"));
                }
                catch
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, user);
                }
                DBCon.getUserDetails(user);
                if (bookCon.addBook(new Book(bookTitle, new Genre(bookGenre), new Author(bookAuthor), bookDescription, new Publisher(bookPublisher), bookCost, bookStock)))
                    return Request.CreateResponse(HttpStatusCode.Accepted,user);
                else
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, user);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

    }
}
