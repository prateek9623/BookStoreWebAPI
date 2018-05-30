using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Author
    {
        public string AuthorFname { get; set; }
        public string AuthorLname { get; set; }

        public Author(string Fname, string Lname)
        {
            this.AuthorFname = Fname;
            this.AuthorLname = Lname;
        }
    }
}
