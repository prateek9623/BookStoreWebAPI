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
    public class CartController : ApiController
    {
        private DBConnection dbObj = DBConnection.getObject();

        [HttpPost]
        [Route("api/cart/update")]
        public HttpResponseMessage update(JObject obj)
        {
            string sessionId = Request.Headers.Authorization.Parameter;
            if (sessionId != null && dbObj.checkSession(ref sessionId))
            {
                string bookId = obj.Value<String>("id");
                int bookQuantity = obj.Value<int>("quantity");

                User user = UserBLL.getUser(sessionId);
                if (CartBLL.updateCart(user, bookId, bookQuantity))
                    return Request.CreateResponse(HttpStatusCode.Accepted, UserBLL.getUser(sessionId));
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, user);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        [Route("api/cart/clear")]
        public HttpResponseMessage clear()
        {
            string sessionId = Request.Headers.Authorization.Parameter;
            if (sessionId != null && dbObj.checkSession(ref sessionId))
            {
                User user = UserBLL.getUser(sessionId);
                CartBLL.clearCart(user);
                return Request.CreateResponse(HttpStatusCode.Accepted, UserBLL.getUser(sessionId));
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

    }
}
