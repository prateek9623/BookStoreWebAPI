using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class BookController : ApiController
    {
        private DBConnection DBCon;

        BookController()
        {
            DBCon = DBConnection.getObject();
        }

        [HttpGet]
        [ResponseType(typeof(Book[]))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return request.CreateResponse(HttpStatusCode.Accepted, Json(DBCon.getBooks().ToArray<Book>()));
        }

        [HttpGet]
        [ResponseType(typeof(Book))]
        public HttpResponseMessage Get(HttpRequestMessage request, string bookName)
        {
            return request.CreateResponse(HttpStatusCode.Accepted, Json(DBCon.getBookByName(bookName)));
        }


        // PUT: api/Book/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Book/5
        public void Delete(int id)
        {
        }
    }
}
