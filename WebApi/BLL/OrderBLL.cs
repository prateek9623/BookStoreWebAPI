using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.BLL
{
    public class OrderBLL
    {
        public static bool addOrder(User user, Order order)
        {
            bool status = false;
                   IList<Book> books = DBConnection.getObject().getBooks();
            foreach (BookCount item in order.OrderedItems) {
                foreach(Book book in books)
                {
                    if(book.BookId.Equals(item._Book.BookId))
                    if (book.BookStock <= 0 || book.BookStock < item.BookQuantity)
                        return false;
                }
            }
            DBConnection.getObject().addOrder(user, order);
            return true;
        }
    }
}