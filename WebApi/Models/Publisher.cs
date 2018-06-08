using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Publisher
    {
        public string PublisherName { get; set; }

        public Publisher( string Name)
        {
            PublisherName = Name;
        }
    }
}
