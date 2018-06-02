using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class User
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public IDictionary<Book, int> CartBookList { get; set; }
        public IList<Order> OrderList { get; set; }

        public User(string Id,string Name,string Fname, string Lname, string _Email, string _ContactNo, IDictionary<Book, int> _CartBookList, IList<Order> _OrderList)
        {
            UserName = Name;
            FirstName = Fname;
            LastName = Lname;
            Email = _Email;
            ContactNo = _ContactNo;
            CartBookList = _CartBookList;
            OrderList = _OrderList;
        }

    }
}
