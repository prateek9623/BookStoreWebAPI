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
        public HttpResponseMessage  ValidateUser(JObject obj)
        {

            string username = obj.Value<String>("userName");
            string password = obj.Value<String>("password");
            User user = new User(username);
            if (DBCon.validateUser(user, password)){
                DBCon.createSession(user);
                if (DBCon.validateAdmin(user))
                {
                    user.isAdmin = true;
                }
                return Request.CreateResponse(HttpStatusCode.Accepted, user);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
        
        [HttpPost]
        [ResponseType(typeof(User))]
        [Route("api/user/registration")]
        public HttpResponseMessage Registeration(JObject obj)
        {
            string userName = obj.Value<String>("userName");
            string frontName = obj.Value<String>("firstName");
            string lastName = obj.Value<String>("lastName");
            string email = obj.Value<String>("email");
            string password = obj.Value<String>("password");
            User newUser = new User(userName, frontName, lastName, email);
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
