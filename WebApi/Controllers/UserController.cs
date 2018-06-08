using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using DAL;
using System.Web.Http.Description;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    public class UserController : ApiController
    {
        DBConnection DBCon;
        UserController()
        {
            DBCon = DBConnection.getObject();
        }

        [HttpPost]
        [ResponseType(typeof(User))]
        public HttpResponseMessage  ValidateUser(HttpRequestMessage request, string username, string password)
        {
            User user = new User(username);
            if (DBCon.validateUser(user, password)){
                
                return request.CreateResponse(HttpStatusCode.Accepted, Json(user));
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        [ResponseType(typeof(User))]
        public HttpResponseMessage ValidateAdmin(HttpRequestMessage request, string username, string password)
        {
            User user = new User(username);
            if (DBCon.validateUser(user, password)&&DBCon.validateAdmin(user))
            {
                return request.CreateResponse(HttpStatusCode.Accepted, Json(user));
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        [ResponseType(typeof(User))]
        public HttpResponseMessage Registeration(HttpRequestMessage request, User newUser, string userPass )
        {
            if (DBCon.addUser(newUser, userPass)){
                return request.CreateResponse(HttpStatusCode.Accepted, Json(newUser));
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

    }
}
