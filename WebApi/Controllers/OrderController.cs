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
    public class OrderController : ApiController
    {
        [HttpPost]
        [Route("api/order/add")]
        public HttpResponseMessage add(JObject obj)
        {
            string sessionId = Request.Headers.Authorization.Parameter;
            if (sessionId != null && DBConnection.getObject().checkSession(ref sessionId))
            {
                User user = UserBLL.getUser(sessionId);

                //string receiverName = obj.Value<string>("ReceiverName");
                //string receiverContactNo = obj.Value<string>("ReceiverContactNo");
                //string orderTransactionId = obj.Value<string>("OrderTransactionId");
                //Address address = obj.Value<JObject>("ReceiverAddr").ToObject<Address>();

                Order order = obj.ToObject<Order>();
                if (OrderBLL.addOrder(user, order))
                    return Request.CreateResponse(HttpStatusCode.Accepted, UserBLL.getUser(sessionId));
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, UserBLL.getUser(sessionId));
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}
