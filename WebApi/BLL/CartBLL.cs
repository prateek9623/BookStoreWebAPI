using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;
using DAL;

namespace WebApi.BLL
{
    public class CartBLL
    {
        public static bool updateCart(User user, string bookId,int quantity )
        {
            bool status = false;
            Cart cartItemToBeUpdated = null;
            foreach (Cart c in user.CartBookList)
            {
                if (c._Book.BookId == bookId)
                {
                    cartItemToBeUpdated = c;
                    break;
                }
            }
            if (cartItemToBeUpdated != null)
            {
                if (int.Parse(cartItemToBeUpdated.BookQuantity) != -quantity)
                    status = DBConnection.getObject().updateCart(user, bookId, quantity);
                else
                {
                    DBConnection.getObject().deleteCartBook(user, bookId);
                    status = true;
                }
            }
            else
            {
                status = DBConnection.getObject().updateCart(user, bookId, quantity);
            }

            return status;
        }

        public static void clearCart(User user)
        {
            foreach (Cart c in user.CartBookList)
            {
                 DBConnection.getObject().deleteCartBook(user, c._Book.BookId);
            }
        }
    }
}