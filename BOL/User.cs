using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class User
    {
        public string UserId { get; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Role { get; }
        public IList<Dictionary<Book, int>> CartBookList { get; set; }
        public IList<Order> OrderList { get; set; }

        public User(string Id,string Name,string Fname, string Lname, string _Email, string _ContactNo, string _Role, IList<Dictionary<Book, int>> _CartBookList, IList<Order> _OrderList)
        {
            UserId = Id;
            UserName = Name;
            FirstName = Fname;
            LastName = Lname;
            Email = _Email;
            ContactNo = _ContactNo;
            Role = _Role;
            CartBookList = _CartBookList;
            OrderList = _OrderList;
        }

    }
}
