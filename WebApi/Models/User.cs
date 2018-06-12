using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class User : UserDetails
    {
        public string SessionId { get; set; }  
        public IList<Cart> CartBookList { get; set; }
        public IList<Order> OrderList { get; set; }

        public User(string Name,string Fname, string Lname, string _Email, string _ContactNo, IList<Cart> _CartBookList, IList<Order> _OrderList) :base( Name, Fname, Lname, _Email, _ContactNo)
        {
            CartBookList = _CartBookList;
            OrderList = _OrderList;
        }

        public User(string Name) : base(Name) {
            CartBookList = new List<Cart>();
            OrderList = new List<Order>();
        }
        
        public User(string UName, string sessionId) : base(UName) {
            SessionId = sessionId;
            CartBookList = new List<Cart>();
            OrderList = new List<Order>();
        }

        public User(string Name, string Fname, string Lname, string _Email, string _ContactNo):base(Name, Fname, Lname, _Email, _ContactNo)
        {
            CartBookList = new List<Cart>();
            OrderList = new List<Order>();
        }
    }
}
