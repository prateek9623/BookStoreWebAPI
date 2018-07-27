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
        public static bool updateCart(User user, Cart cartItem)
        {
            bool status = false;
            Cart cartItemToBeUpdated = null;
            foreach (Cart c in user.CartBookList)
            {
                if (c._Book.BookId == cartItem._Book.BookId)
                {
                    cartItemToBeUpdated = c;
                    break;
                }
            }
            if (cartItemToBeUpdated != null)
            {
                if (cartItemToBeUpdated.BookQuantity != -cartItem.BookQuantity)
                {
                    Book book = DBConnection.getObject().getBookById(cartItem._Book.BookId).First<Book>();
                    if (book.BookStock >= cartItemToBeUpdated.BookQuantity + cartItem.BookQuantity)
                        status = DBConnection.getObject().updateCart(user, cartItem._Book.BookId, cartItem.BookQuantity);
                    else
                        status = false;
                }
                else
                {
                    DBConnection.getObject().deleteCartBook(user, cartItem._Book.BookId);
                    status = true;
                }
            }
            else
            {
                status = DBConnection.getObject().updateCart(user, cartItem._Book.BookId, cartItem.BookQuantity);
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