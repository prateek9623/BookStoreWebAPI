using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;
using DAL;

namespace WebApi.BLL
{
    public class UserBLL
    {
        public static User getUser(string sessionId)
        {
            DBConnection obj = DBConnection.getObject();

            User user = obj.getUser(sessionId);
            if (user != null)
            {
                user.CartBookList = obj.getCartsBook(user);
                user.OrderList = obj.getOrdersByUser(user);
                user.isAdmin = obj.validateAdmin(user.UserName);
            }
            return user;
        }
    }
}