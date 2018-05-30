﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Order
    {
        //books, quantity
        public IList<Dictionary<Book, int>> OrderedItems { get; set;}
        public string ReceiverName { get; set; }
        public Address ReceiverAddr { get; set; }
        public string ReceiverContactNo { get; set; }
        public bool OrderShipped { get; set; }
        public string OrderTransactonId { get; set; }

        public Order(string ReceiverName, Address ReceiverAddr, string ReceiverContactNo, IList<Dictionary<Book, int>> OrderedItems, bool OrderShipped, string OrderTransactonId)
        {
            this.ReceiverAddr = ReceiverAddr;
            this.ReceiverContactNo = ReceiverContactNo;
            this.ReceiverName = ReceiverName;
            this.OrderedItems = OrderedItems;
            this.OrderTransactonId = OrderTransactonId;
        }
    }
}