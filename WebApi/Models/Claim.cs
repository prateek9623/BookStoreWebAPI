using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Claim
    {
        public string SessionId { get; set; }
        public string UserName { get; set; }

        public Claim(string sessionId, string userName)
        {
            SessionId = sessionId;
            UserName = userName;
        }
    }
}