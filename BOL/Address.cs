using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Address
    {
        public string LocalAddr { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public Address(string localAddr, string city, string state, string zipcode, string country)
        {
            this.LocalAddr = localAddr;
            this.City = city;
            this.State = state;
            this.ZipCode = zipcode;
            this.Country = country;
        }
    }
}
