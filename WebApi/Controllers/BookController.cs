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
        [ResponseType(typeof(string[]))]
        public HttpResponseMessage Genres()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, DBCon.getGenre().ToArray<string>());
        }

        [HttpGet]
        [Route("api/Publishers/")]
        [ResponseType(typeof(string[]))]
        public HttpResponseMessage Publishers()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, DBCon.getPublisher().ToArray<string>());
        }

        [HttpGet]
        [Route("api/Authors/")]
        [ResponseType(typeof(Author[]))]
        public HttpResponseMessage Authors()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, DBCon.getAuthors().ToArray<string>());
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
        public HttpResponseMessage Book(string bookId)
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, DBCon.getBookById(bookId).ToArray<Book>());
        }

        [HttpPost]
        [Route("api/Book/")]
        [ResponseType(typeof(User))]
        public HttpResponseMessage registerBook(JObject obj)
        {
            string sessionId = Request.Headers.Authorization.Parameter;
            if (DBCon.checkSession(ref sessionId) && sessionId!= null)
            {
                User user = UserBLL.getUser(sessionId);
                if (user.isAdmin)
                {
                    string bookTitle = obj.Value<String>("title");
                    string bookGenre = obj.Value<String>("genre");
                    string bookAuthor = obj.Value<String>("author");
                    string bookDescription = obj.Value<String>("description");
                    string bookPublisher = obj.Value<String>("publisher");
                    string bookThumb = obj.Value<String>("imageurl");
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
                    if (bookCon.addBook(new Book(bookTitle, new Genre(bookGenre), new Author(bookAuthor), bookDescription, new Publisher(bookPublisher), bookCost, bookThumb, bookStock)))
                        return Request.CreateResponse(HttpStatusCode.Accepted, user);
                    else
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, user);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

        [HttpPut]
        [Route("api/Book")]
        public HttpResponseMessage updateBook(JObject obj)
        {
            string sessionId = Request.Headers.Authorization.Parameter;
            if (DBCon.checkSession(ref sessionId) && sessionId != null)
            {
                User user = UserBLL.getUser(sessionId);
                if (user.isAdmin)
                {
                    string bookId = obj.Value<String>("id");
                    string bookTitle = obj.Value<String>("title");
                    string bookGenre = obj.Value<String>("genre");
                    string bookAuthor = obj.Value<String>("author");
                    string bookDescription = obj.Value<String>("description");
                    string bookPublisher = obj.Value<String>("publisher");
                    string bookThumb = obj.Value<String>("imageurl");
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
                    if (bookCon.updateBook(new Book(bookId,bookTitle, new Genre(bookGenre), new Author(bookAuthor), bookDescription, new Publisher(bookPublisher), bookCost, bookThumb, bookStock)))
                        return Request.CreateResponse(HttpStatusCode.Accepted, user);
                    else
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, user);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}
