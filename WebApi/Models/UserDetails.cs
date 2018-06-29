using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class UserDetails
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public UserDetails(string uName, string fName, string lName, string eMail)
        {
            UserName = uName;
            FirstName = fName;
            LastName = lName;
            Email = eMail;
        }

        public UserDetails(string uName)
        {
            UserName = uName;
        }
    }
}