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
                user.SessionId = DBCon.createSession(user);
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
                user.SessionId = DBCon.createSession(user);
                return Request.CreateResponse(HttpStatusCode.Accepted, Json(user));
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        [ResponseType(typeof(User))]
        [Route("api/user/registration")]
        public HttpResponseMessage Registeration(User newUser, [FromBody]  string userPass )
        {
            if (DBCon.addUser(newUser, userPass)){
                newUser.SessionId = DBCon.createSession(newUser);
                return Request.CreateResponse(HttpStatusCode.Accepted, Json(newUser));
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("api/user/logout")]
        public HttpResponseMessage LogOut(JObject jsonObject)
        {
            if (DBCon.endSession(jsonObject.ToObject<Claim>()))
            {
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

    }

}
