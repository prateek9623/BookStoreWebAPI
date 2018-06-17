using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using DAL;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Web.Script.Serialization;
using WebApi.Filters;

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
        [Route("api/user/login")]
        public HttpResponseMessage  ValidateUser()
        {
            string username = HttpContext.Current.Request.Params["username"];
            string password = HttpContext.Current.Request.Params["password"];
            User user = new User(username);
            if (DBCon.validateUser(user, password)){
                DBCon.createSession(user);
                return Request.CreateResponse(HttpStatusCode.Accepted, user);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        [ResponseType(typeof(User))]
        [Route("api/user/admin/login")]
        public HttpResponseMessage ValidateAdmin()
        {
            string username = HttpContext.Current.Request.Params["username"];
            string password = HttpContext.Current.Request.Params["password"];
            User user = new User(username);
            if (DBCon.validateUser(user, password)&&DBCon.validateAdmin(user))
            {
                DBCon.createSession(user);
                return Request.CreateResponse(HttpStatusCode.Accepted, Json(user));
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
        
        [HttpPost]
        [ResponseType(typeof(User))]
        [Route("api/user/registration")]
        public HttpResponseMessage Registeration()
        {
            string userName = HttpContext.Current.Request.Params["userName"];
            string frontName = HttpContext.Current.Request.Params["frontName"];
            string lastName = HttpContext.Current.Request.Params["lastName"];
            string email = HttpContext.Current.Request.Params["email"];
            string contactNo = HttpContext.Current.Request.Params["contactno"];
            string password = HttpContext.Current.Request.Params["password"];
            User newUser = new User(userName, frontName, lastName, email, contactNo);
            if (DBCon.addUser(newUser, password)){
                DBCon.createSession(newUser);
                return Request.CreateResponse(HttpStatusCode.Accepted, newUser);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        
        [HttpPost]
        [Route("api/user/logout")]
        public HttpResponseMessage LogOut(Claim claim)
        {
            if (DBCon.endSession(claim))
            {
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ResponseType(typeof(User))]
        [Route("api/user/details")]
        public HttpResponseMessage Details(Claim claim)
        {
            User user = new User(claim.UserName, claim.SessionId);

            if (DBCon.checkSession(user))
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, DBCon.getUserDetails(user));
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }

}
