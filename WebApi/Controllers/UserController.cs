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
using System.Net.Http.Headers;
using WebApi.BLL;

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

            string username = obj.Value<String>("username");
            string password = obj.Value<String>("password");
            User user = new User(username);
            if (DBCon.validateUser(username, password)){
                user = UserBLL.getUser(DBCon.createSession(user));
                HttpResponseMessage httpResponse = Request.CreateResponse(HttpStatusCode.Accepted, user);
                return httpResponse;
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
            if (DBCon.addUser(newUser, password))
            {
                newUser.SessionId = DBCon.createSession(newUser);
                HttpResponseMessage httpResponse = Request.CreateResponse(HttpStatusCode.Accepted,  newUser);
                return httpResponse;
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        
        [HttpPost]
        [Route("api/user/logout")]
        public HttpResponseMessage LogOut()
        {
            string sessionId = Request.Headers.Authorization.Parameter;
            if (sessionId != null&&DBCon.endSession(sessionId))
            {
                HttpResponseMessage httpResponse = Request.CreateResponse(HttpStatusCode.Accepted);
                return httpResponse;
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ResponseType(typeof(User))]
        [Route("api/user/details")]
        public HttpResponseMessage Details()
        {
            string sessionId = Request.Headers.Authorization.Parameter;
            if (sessionId!=null)
            {
                User user = UserBLL.getUser(sessionId);
                HttpResponseMessage httpResponse = Request.CreateResponse(HttpStatusCode.Accepted,user);
                return httpResponse;
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }

}
