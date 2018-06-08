using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class User : UserDetails
    {
        public IDictionary<Book, int> CartBookList { get; set; }
        public IList<Order> OrderList { get; set; }

        public User(string Name,string Fname, string Lname, string _Email, string _ContactNo, IDictionary<Book, int> _CartBookList, IList<Order> _OrderList) :base( Name, Fname, Lname, _Email, _ContactNo)
        {
            CartBookList = _CartBookList;
            OrderList = _OrderList;
        }

        public User(string Name) : base(Name) { }
        
        public User(string Name, string Fname, string Lname, string _Email, string _ContactNo):base(Name, Fname, Lname, _Email, _ContactNo)
        {

        }
    }
}
