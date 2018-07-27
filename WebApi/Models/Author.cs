using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Author
    {
        public string AuthorName { get; set; }

        public Author() { }

        public Author(string name)
        {
            this.AuthorName = name;
        }
    }
}
