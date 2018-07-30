using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using WebApi.Models;
using System.Security.Cryptography;

namespace DAL
{
    public class DBConnection
    {
        private static DBConnection OBJ;
        private string conString;

        private DBConnection() {
            conString = ConfigurationManager.ConnectionStrings["conBookStore"].ConnectionString;
        }

        public static DBConnection getObject()
        {
            if (OBJ == null)
                OBJ = new DBConnection();

            return OBJ;
        }

        //BookDal
        public IList<string> getAuthors()
        {
            IList<string> authorList = new List<string>();
            MySqlConnection con = new MySqlConnection(conString);
            string cmdString = "select * from authors";
            MySqlCommand cmd = new MySqlCommand(cmdString, con);
            try
            {
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    string name = reader["AuthorName"].ToString();
                    authorList.Add(name);
                }
                reader.Close();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                con.Close();
            }
            return authorList;
        }

        public bool addAuthor(Author _author)
        {
            try { 
            using (MySqlConnection con = new MySqlConnection(conString)) {
                con.Open();
                string cmdString = "INSERT INTO `authors`(`AuthorName`)VALUES(@authorName)";
                MySqlCommand cmd = new MySqlCommand(cmdString, con);
                cmd.Parameters.Add(new MySqlParameter("@authorName", _author.AuthorName));
                cmd.ExecuteNonQuery();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                    return true;
            }
            }catch(MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public IList<string> getGenre()
        {
            IList<string> genreList = new List<string>();
            MySqlConnection con = new MySqlConnection(conString);
            string cmdString = "select * from genrelist";
            MySqlCommand cmd = new MySqlCommand(cmdString, con);
            try
            {
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["GenreName"].ToString();
                    genreList.Add(name);
                }
                reader.Close();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                con.Close();
            }
            return genreList;
        }
        
        public bool addGenre(Genre _genre)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string cmdString = "INSERT INTO `genrelist`(`GenreName`) VALUES (@genrename)";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@genrename", _genre.GenreName));
                    cmd.ExecuteNonQuery();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public IList<string> getPublisher()
        {
            IList<string> publisherList = new List<string>();
            MySqlConnection con = new MySqlConnection(conString);
            string cmdString = "select * from publishers";
            MySqlCommand cmd = new MySqlCommand(cmdString, con);
            try
            {
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["PublisherName"].ToString();
                    publisherList.Add(name);
                }
                reader.Close();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                con.Close();
            }
            return publisherList;
        }

        public bool addPublisher(Publisher publisher)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string cmdString = "INSERT INTO `publishers`(`PublisherName`)VALUES(@publisherName)";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@publisherName", publisher.PublisherName));
                    cmd.ExecuteNonQuery();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public IList<Book> getBooks()
        {
            IList<Book> bookList = new List<Book>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string cmdString = "select * from getbooks";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    try
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string id = reader["BookId"].ToString();
                            string title = reader["BookTitle"].ToString();
                            int rating = int.Parse(reader["BookRating"].ToString());
                            Genre genre = new Genre(reader["GenreName"].ToString());
                            double cost = double.Parse(reader["BookCost"].ToString());
                            Author author = new Author(reader["AuthorName"].ToString());
                            Publisher publisher = new Publisher(reader["PublisherName"].ToString());
                            string thumb = reader["BookThumb"].ToString();
                            string description = reader["BookDescription"].ToString();
                            int stock = int.Parse(reader["BookStock"].ToString());
                                bookList.Add(new Book(id, title, genre, author, description, publisher, rating, cost, thumb, stock));
                        }
                        reader.Close();
                    }
                    catch (Exception exp)
                    {
                        throw exp;
                    }
                    finally
                    {
                        con.Close();
                    }
                    return bookList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return bookList;
        }

        public IList<Book> getBookById(string bookId)
        {
            IList<Book> bookList = new List<Book>();
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();
                string cmdString = "select * from getbooks where BookId = @bookId";
                MySqlCommand cmd = new MySqlCommand(cmdString, con);
                cmd.Parameters.Add(new MySqlParameter("@bookId", bookId));
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string id = reader["BookId"].ToString();
                        string title = reader["BookTitle"].ToString();
                        int rating = int.Parse(reader["BookRating"].ToString());
                        Genre genre = new Genre(reader["GenreName"].ToString());
                        double cost = double.Parse(reader["BookCost"].ToString());
                        Author author = new Author(reader["AuthorName"].ToString());
                        Publisher publisher = new Publisher(reader["PublisherName"].ToString());
                        string thumb = reader["BookThumb"].ToString();
                        string description = reader["BookDescription"].ToString();
                        int stock = int.Parse(reader["BookStock"].ToString());
                        if (stock > 0)
                            bookList.Add(new Book(id, title, genre, author, description, publisher, rating, cost, thumb, stock));
                    }
                    reader.Close();
                }
                catch (Exception exp)
                {
                    throw exp;
                }
                finally
                {
                    con.Close();
                }
                return bookList;
            }
            
        }

        public bool addBook(Book book)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string cmdString = "";
                    //cmdString = "insert into books(books.BookTitle, books.BookGenre, books.BookAuthorId,books.BookDescription,books.BookPublisherId,books.BookCost,books.BookStock)"
                    //            + " values(@BookTitle, (select genrelist.GenreId from genrelist where genrelist.GenreName = @GenreName),(select authors.AuthorId from authors where authors.AuthorName = @AuthorName)," +
                    //            " @BookDescription, (select publishers.PublisherId from publishers where publishers.PublisherName = @PublisherName), @BookCost, @BookStock)";

                    cmdString = "insert into books(books.BookTitle, books.BookGenre, books.BookAuthorId,books.BookDescription,books.BookPublisherId,books.BookCost,books.BookThumb,books.BookStock)"
                                + " values(@BookTitle, (select genrelist.GenreId from genrelist where genrelist.GenreName = @GenreName),(select authors.AuthorId from authors where authors.AuthorName = @AuthorName)," +
                                " @BookDescription, (select publishers.PublisherId from publishers where publishers.PublisherName = @PublisherName), @BookCost, @BookThumb , @BookStock)";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@BookTitle", book.BookTitle));
                    cmd.Parameters.Add(new MySqlParameter("@GenreName", book.BookGenre.GenreName));
                    cmd.Parameters.Add(new MySqlParameter("@AuthorName", book.BookAuthor.AuthorName));
                    cmd.Parameters.Add(new MySqlParameter("@BookDescription", book.BookDescription));
                    cmd.Parameters.Add(new MySqlParameter("@PublisherName", book.BookPublisher.PublisherName));
                    cmd.Parameters.Add(new MySqlParameter("@BookCost", book.BookCost));
                    cmd.Parameters.Add(new MySqlParameter("@BookThumb", book.BookThumb));
                    cmd.Parameters.Add(new MySqlParameter("@BookStock", book.BookStock));
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
        
        public bool updateBook(Book book)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string cmdString = "";
                    cmdString = "UPDATE `bookstore`.`books`"
                                +"SET"
                                +"`BookTitle` = @BookTitle,"
                                + "`BookGenre` = (select genrelist.GenreId from genrelist where genrelist.GenreName = @GenreName),"
                                + "`BookAuthorId` = (select authors.AuthorId from authors where authors.AuthorName = @AuthorName),"
                                + "`BookDescription` = @BookDescription,"
                                + "`BookPublisherId` = (select publishers.PublisherId from publishers where publishers.PublisherName = @PublisherName),"
                                + "`BookCost` = @BookCost,"
                                +"`BookThumb` = @BookThumb,"
                                +"`BookStock` = @BookStock"
                                +" WHERE `BookId` = @BookId";

                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@BookTitle", book.BookTitle));
                    cmd.Parameters.Add(new MySqlParameter("@GenreName", book.BookGenre.GenreName));
                    cmd.Parameters.Add(new MySqlParameter("@AuthorName", book.BookAuthor.AuthorName));
                    cmd.Parameters.Add(new MySqlParameter("@BookDescription", book.BookDescription));
                    cmd.Parameters.Add(new MySqlParameter("@PublisherName", book.BookPublisher.PublisherName));
                    cmd.Parameters.Add(new MySqlParameter("@BookCost", book.BookCost));
                    cmd.Parameters.Add(new MySqlParameter("@BookThumb", book.BookThumb));
                    cmd.Parameters.Add(new MySqlParameter("@BookStock", book.BookStock));
                    cmd.Parameters.Add(new MySqlParameter("@BookId", book.BookId));
                    int count = cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
        

        public IList<UserReview> getBookReviews(Book book)
        {
            IList < UserReview > reviewList = new List<UserReview>();
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();
                string cmdString = "select * from userdetails, customerreview where userdetails.userid=customerreview.CustomerId and BookId=@bookId";
                MySqlCommand cmd = new MySqlCommand(cmdString,con);
                cmd.Parameters.Add(new MySqlParameter("@bookId", book.BookId));
                MySqlDataReader dataSet = cmd.ExecuteReader();
                while (dataSet.HasRows)
                {
                    string uName = dataSet["UserUname"].ToString();
                    string fname = dataSet["UserFname"].ToString();
                    string lname = dataSet["UserLname"].ToString();
                    string email = dataSet["UserEmail"].ToString();
                    string userComment = dataSet["CustomerComment"].ToString();
                    int userRating = int.Parse(dataSet["CustomerRating"].ToString());
                    reviewList.Add( new UserReview(new User(uName,fname,lname,email),userRating,userComment));
                }
            }
            return reviewList;
        }

        public bool addUserReview(UserReview review, Book book)
        {
            string userId;
            using(MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();
                string cmdString = "select UserId from userreview where UserUname = @userUname";
                using(MySqlCommand cmd = new MySqlCommand(cmdString))
                {
                    cmd.Parameters.Add(new MySqlParameter("@userUname", review.ReviewOwner.UserName));
                    MySqlDataReader dataSet = cmd.ExecuteReader();
                    if (dataSet.HasRows) {
                        userId = dataSet["UserId"].ToString();
                    }
                    else
                    {
                        dataSet.Close();
                        con.Close();
                        return false;
                    }

                }
                cmdString = "INSERT INTO `bookstore`.`customerreview`(`CustomerId`,`BookId`,`CustomerRating`,`CustomerComment`) VALUES(@userId,@bookId,@bookRating,@bookComment)";
                using(MySqlCommand cmd = new MySqlCommand(cmdString))
                {
                    cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                    cmd.Parameters.Add(new MySqlParameter("@bookId", book.BookId));
                    cmd.Parameters.Add(new MySqlParameter("@bookRating", review.ReviewRating));
                    cmd.Parameters.Add(new MySqlParameter("@bookComment", review.ReviewComment));
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        //UserDAL
        public bool addUser(User newUser, string userPass)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string cmdString = "INSERT INTO `bookstore`.`userdetails`(`UserUname`,`UserFname`,`UserLname`,`UserPassword`,`UserEmail`)VALUES( @userName, @userFname, @userLname, @userPass, @userEmail);";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", newUser.UserName));
                    cmd.Parameters.Add(new MySqlParameter("@userFname", newUser.FirstName));
                    cmd.Parameters.Add(new MySqlParameter("@userLname", newUser.LastName));
                    cmd.Parameters.Add(new MySqlParameter("@userPass", userPass));
                    cmd.Parameters.Add(new MySqlParameter("@userEmail", newUser.Email));
                    cmd.ExecuteNonQuery();
                    status = true;
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }catch(SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return status;
        }

        public IList<User> getAllUser()
        {
            IList<User> usersList = new List<User>();
            string cmdString = "SELECT `userdetails`.`UserId`, `userdetails`.`UserUname`, `userdetails`.`UserFname`, `userdetails`.`UserLname`,`userdetails`.`UserEmail` FROM `bookstore`.`userdetails`";
            using(MySqlConnection con = new MySqlConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(cmdString, con);
                MySqlDataReader dataSet = cmd.ExecuteReader();
                if (dataSet.HasRows)
                {
                    while (dataSet.Read())
                    {
                        string userId = dataSet["UserId"].ToString();
                        string userName = dataSet["UserUname"].ToString();
                        string userFname = dataSet["UserFname"].ToString();
                        string userLname = dataSet["UserLname"].ToString();
                        string userEmail = dataSet["UserEmail"].ToString();
                        usersList.Add(new User(userName, userFname, userLname, userEmail));
                    }
                }

            }

            return usersList;
        }

        public bool validateUser(string user, string pass)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string cmdString = "SELECT `userdetails`.`UserId`, `userdetails`.`UserUname`, `userdetails`.`UserFname`, `userdetails`.`UserLname`,`userdetails`.`UserEmail` FROM `bookstore`.`userdetails`WHERE userdetails.UserUname = @userName AND userdetails.UserPassword = @password";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user));
                    cmd.Parameters.Add(new MySqlParameter("@password", pass));

                    MySqlDataReader userDetails = cmd.ExecuteReader();
                    if (userDetails.HasRows)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return status;
        }

        public User getUser(string sessionId)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string cmdString = "select * from userdetails where userid = (select sessionlog.SessionUserId from sessionlog where SessionId =@sessionId);";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@sessionId", sessionId));

                    MySqlDataReader userDetails = cmd.ExecuteReader();
                    if (userDetails.HasRows)
                    {
                        userDetails.Read();
                        User user = new User(userDetails["UserUname"].ToString(), userDetails["UserFname"].ToString(), userDetails["UserLname"].ToString(), userDetails["UserEmail"].ToString());
                        userDetails.Close();
                        user.SessionId = sessionId;
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return null;
        }

        //public User getUserDetails(string sessionId)
        //{
        //    try
        //    {
        //        using (MySqlConnection con = new MySqlConnection(conString))
        //        {
        //            con.Open();
        //            string cmdString = "select * from userdetails where userid = (select sessionlog.SessionUserId from sessionlog where SessionId =@sessionId);";
        //            MySqlCommand cmd = new MySqlCommand(cmdString, con);
        //            cmd.Parameters.Add(new MySqlParameter("@sessionId", sessionId));

        //            MySqlDataReader userDetails = cmd.ExecuteReader();
        //            if (userDetails.HasRows)
        //            {
        //                userDetails.Read();
        //                User user = new User(userDetails["UserUname"].ToString(), userDetails["UserFname"].ToString(), userDetails["UserLname"].ToString(), userDetails["UserEmail"].ToString());
        //                userDetails.Close();

        //                user.CartBookList = getCartsBook(user);
        //                user.OrderList = getOrdersByUser(user);
        //                user.isAdmin=validateAdmin(user.UserName);
        //                return user;
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //    }
        //    return null;
        //}

        public bool validateAdmin(string user)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string cmdstring = "select * from userdetails where UserRole = 'admin' && UserUname = @userName";
                    MySqlCommand cmd = new MySqlCommand(cmdstring, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user));
                    MySqlDataReader dataset = cmd.ExecuteReader();
                    if (dataset.HasRows)
                    {
                        status = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }
            return status;
        }

        public IList<Cart> getCartsBook(User user)
        {
            IList<Cart> cartsBook = new List<Cart>();
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();
                string cmdString = "select cart.CartBookId, cart.CartQuantity from cart, userdetails where CartCusId=UserId and UserUname=@userName";
                MySqlCommand cmd = new MySqlCommand(cmdString, con);
                cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    IList<Book> bookList = getBooks();
                    while (reader.Read())
                    {
                        string bookId = reader["CartBookId"].ToString();
                        foreach (Book book in bookList)
                        {
                            if(book.BookId==bookId)
                                cartsBook.Add(new Cart(book, int.Parse(reader["CartQuantity"].ToString())));
                        }
                    }
                    reader.Close();
                }
                catch (Exception exp)
                {
                    throw exp;
                }
                finally
                {
                    con.Close();
                }
            }
            return cartsBook;
        }

        public bool updateCart(User user,string bookId, int quantity)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string cmdstring = "INSERT INTO `bookstore`.`cart`(`CartCusId`,`CartBookId`,`CartQuantity`)VALUES((select `UserId` from userdetails where UserUname = @userName),@bookId,@quantity) on duplicate key update `CartQuantity` = `CartQuantity` +@quantity";
                    MySqlCommand cmd = new MySqlCommand(cmdstring, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                    cmd.Parameters.Add(new MySqlParameter("@bookId", bookId));
                    cmd.Parameters.Add(new MySqlParameter("@quantity", quantity));
                    cmd.ExecuteNonQuery();
                    status = true;
                }
            }
            catch (SqlException ex)
            {
                Console.Write(ex);
            }
            return status;
        }

        public bool  deleteCartBook(User user, string bookId)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string cmdstring = "delete from `bookstore`.`cart` where `CartCusId`=(select `UserId` from userdetails where UserUname = @userName) and `CartBookId`= @bookId";
                    MySqlCommand cmd = new MySqlCommand(cmdstring, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                    cmd.Parameters.Add(new MySqlParameter("@bookId", bookId));
                    cmd.ExecuteNonQuery();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return status;
        }

        public bool addOrder(User user, Order order)
        {
            bool status = false;
            try
            {
                IList<Cart> cart = getCartsBook(user);
                IList<Book> books = getBooks();

                foreach (Cart c in cart)
                {
                    foreach (Book b in books)
                    {
                        if (b.BookId.Equals(c._Book.BookId))
                        {
                            if (b.BookStock < c.BookQuantity)
                            {
                                return false;
                            }
                        }
                    }
                }

                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    String userId;
                    string cmdString = "select UserId from userdetails where UserUname = @userName";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                    MySqlDataReader dataset = cmd.ExecuteReader();
                    dataset.Read();
                    userId = dataset["UserId"].ToString();
                    dataset.Close();
                    con.Close();

                    con.Open();
                    cmdString = "insert into orders(`CustomerId`, `OrderShipName`, `OrderShipAddress`, `OrderCity`, `OrderState`, `OrderZip`, `OrderCountry`, `OrderContactNo.`, `OrderTransactionId`)"+
                        " values (@userId, @recieverName, @recieverAddr, @recieverCity, @recieverState, @recieverZip, @recieverCountry, @recieverContactNo, @recieverTransactionId)";
                    cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                    cmd.Parameters.Add(new MySqlParameter("@recieverName", order.ReceiverName));
                    cmd.Parameters.Add(new MySqlParameter("@recieverAddr", order.ReceiverAddr.LocalAddr));
                    cmd.Parameters.Add(new MySqlParameter("@recieverCity", order.ReceiverAddr.City));
                    cmd.Parameters.Add(new MySqlParameter("@recieverState", order.ReceiverAddr.State));
                    cmd.Parameters.Add(new MySqlParameter("@recieverZip", order.ReceiverAddr.ZipCode));
                    cmd.Parameters.Add(new MySqlParameter("@recieverCountry", order.ReceiverAddr.Country));
                    cmd.Parameters.Add(new MySqlParameter("@recieverContactNo", order.ReceiverContactNo));
                    cmd.Parameters.Add(new MySqlParameter("@recieverTransactionId", order.OrderTransactionId));
                    cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    string orderId;
                    cmdString = "SELECT max(orderid) as orderId FROM bookstore.orders where CustomerId = @userId group by CustomerId";
                    cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                    dataset = cmd.ExecuteReader();
                    dataset.Read();
                    orderId = dataset["orderId"].ToString();
                    dataset.Close();
                    con.Close();

                    con.Open();
                    cmdString = "insert into orderdetails values('" + orderId + "','" + cart[0]._Book.BookId + "'," + cart[0].BookQuantity + ")";
                    for (int i = 1;i < cart.Count;i++)
                    {
                        cmdString += ",('" + orderId + "','" + cart[i]._Book.BookId + "','" + cart[i].BookQuantity + "')";
                    }
                    cmd = new MySqlCommand(cmdString, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    cmdString = "delete from cart where CartCusId = @userId";
                    cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                    cmd.ExecuteNonQuery();

                    status = true;
                }
            }
            catch (SqlException ex)
            {
                Console.Write(ex);
            }
            return status;
        }

        public IDictionary<User,Order> getAllOrders()
        {
            IDictionary<User,Order> orderList = new Dictionary<User,Order>();
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();
                string cmdString = "SELECT userdetails.UserUname, orders.* FROM bookstore.userdetails, orders where orders.CustomerId = userdetails.UserId;";
                MySqlCommand cmd = new MySqlCommand(cmdString, con);
                MySqlDataReader userOrderDetails = cmd.ExecuteReader();
                if (userOrderDetails.HasRows)
                {
                    IList<Book> bookList = getBooks();
                    IList<User> userList = getAllUser();
                    while (userOrderDetails.Read())
                    {
                        string orderId = userOrderDetails["OrderId"].ToString();
                        string customerUserName = userOrderDetails["UserUname"].ToString();
                        string orderShipName = userOrderDetails["OrderShipName"].ToString();
                        string orderShipAddress = userOrderDetails["OrderShipAddress"].ToString();
                        string orderCity = userOrderDetails["OrderCity"].ToString();
                        string orderState = userOrderDetails["OrderState"].ToString();
                        string orderZip = userOrderDetails["OrderZip"].ToString();
                        string orderCountry = userOrderDetails["OrderCountry"].ToString();
                        string orderContactNo = userOrderDetails["OrderContactNo."].ToString();
                        DateTime orderDate = userOrderDetails.GetDateTime("OrderDate");
                        bool orderShipped = userOrderDetails["OrderShipped"].ToString().Contains("1");
                        string orderTransactionId = userOrderDetails["OrderTransactionId"].ToString();
                        Address address = new Address(orderShipAddress, orderCity, orderState, orderZip, orderCountry);
                        IList<BookCount> bookList1 = new List<BookCount>();
                        using (MySqlConnection con1 = new MySqlConnection(conString))
                        {
                            con1.Open();
                            cmdString = "select * from orderdetails where DetailOrderId = @orderId";
                            MySqlCommand cmd1 = new MySqlCommand(cmdString, con1);
                            cmd1.Parameters.Add(new MySqlParameter("@orderId", orderId));
                            MySqlDataReader orderDetail = cmd1.ExecuteReader();
                            while (orderDetail.Read())
                            {
                                string bookId = orderDetail["DetailProductId"].ToString();
                                string quantity = orderDetail["DetailQuantity"].ToString();
                                foreach (Book book in bookList)
                                {
                                    if (book.BookId == bookId)
                                    {
                                        bookList1.Add(new BookCount(book, int.Parse(quantity)));
                                        break;
                                    }
                                }
                            }

                            
                        }
                        foreach (User user in userList)
                        {
                            if (user.UserName == customerUserName)
                            {
                                Order o = new Order(orderShipName, address, orderContactNo, bookList1, orderShipped, orderTransactionId);
                                o.OrderId = orderId;
                                o.OrderPlaceTime = orderDate;

                                orderList.Add(user, o);
                                
                                break;
                            }
                        }

                    }
                }
            }
            return orderList;
        }

        public IList<Order> getOrdersByUser(User user)
        {
            IList<Order> orderList = new List<Order>();
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();
                string cmdString = "SELECT * FROM orders, userdetails where CustomerId = UserId and UserUname = @userName";
                MySqlCommand cmd = new MySqlCommand(cmdString, con);
                cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                MySqlDataReader userOrderDetails = cmd.ExecuteReader();
                if (userOrderDetails.HasRows)
                {
                    IList<Book> bookList = getBooks();
                    while (userOrderDetails.Read())
                    {
                        string orderId = userOrderDetails["OrderId"].ToString();
                        string orderShipName = userOrderDetails["OrderShipName"].ToString();
                        string orderShipAddress = userOrderDetails["OrderShipAddress"].ToString();
                        string orderCity = userOrderDetails["OrderCity"].ToString();
                        string orderState = userOrderDetails["OrderState"].ToString();
                        string orderZip = userOrderDetails["OrderZip"].ToString();
                        string orderCountry = userOrderDetails["OrderCountry"].ToString();
                        string orderContactNo = userOrderDetails["OrderContactNo."].ToString();
                        DateTime orderDate = userOrderDetails.GetDateTime("OrderDate");
                        bool orderShipped = userOrderDetails["OrderShipped"].ToString().Contains("1");
                        string orderTransactionId = userOrderDetails["OrderTransactionId"].ToString();
                        Address address = new Address(orderShipAddress, orderCity, orderState, orderZip, orderCountry);
                        IList<BookCount> bookList1 = new List<BookCount>();
                        using (MySqlConnection con1 = new MySqlConnection(conString))
                        {
                            con1.Open();
                            cmdString = "select * from orderdetails where DetailOrderId = @orderId";
                            MySqlCommand cmd1 = new MySqlCommand(cmdString, con1);
                            cmd1.Parameters.Add(new MySqlParameter("@orderId", orderId));
                            MySqlDataReader orderDetail = cmd1.ExecuteReader();
                            while (orderDetail.Read())
                            {
                                string bookId = orderDetail["DetailProductId"].ToString();
                                string quantity = orderDetail["DetailQuantity"].ToString();
                                foreach (Book book in bookList)
                                {
                                    if (book.BookId == bookId)
                                    {
                                        bookList1.Add(new BookCount(book, int.Parse(quantity)));
                                        break;
                                    }
                                }
                            }

                        }
                        Order temp = new Order(orderShipName, address, orderContactNo, bookList1, orderShipped, orderTransactionId);
                        temp.OrderId = orderId;
                        temp.OrderPlaceTime = orderDate;
                        orderList.Add(temp);
                    }
                }
                userOrderDetails.Close();

            }
            return orderList;
        }

        public string createSession(User user)
        {
            string sessionId = null;
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();
                string cmdString = "SELECT `userdetails`.`UserId` FROM `bookstore`.`userdetails`WHERE userdetails.UserUname = @userName";
                MySqlCommand cmd = new MySqlCommand(cmdString, con);
                cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                MySqlDataReader userDetails = cmd.ExecuteReader();
                if (userDetails.HasRows)
                {
                    userDetails.Read();
                    string userId = userDetails["UserId"].ToString();
                    userDetails.Close();
                    sessionId = getSession(user);
                    if (sessionId == null)
                    {
                        cmdString = "INSERT INTO `bookstore`.`sessionlog`(`SessionId`,`SessionExpiryTime`,`SessionUserId`)VALUES(@sessionId,date_add(current_timestamp(), interval 2 day),@userId)";
                        cmd = new MySqlCommand(cmdString, con);
                        cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                        sessionId = sessionIdGenerator();
                        cmd.Parameters.Add(new MySqlParameter("@sessionId", sessionId));
                        if (cmd.ExecuteNonQuery() == 1)
                            return sessionId;
                    }
                    else
                        return sessionId;
                }
            }
            return null;
        }

        public string getSession(User user)
        {
            string sessionId = null;
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    //check session
                    string cmdString = "SELECT * FROM `bookstore`.`sessionlog`, `bookstore`.`userdetails` WHERE userdetails.UserUname = @userName AND userdetails.UserId = sessionlog.SessionUserId and `SessionExpiryTime` > current_timestamp()";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                    MySqlDataReader userDetails = cmd.ExecuteReader();
                    if (userDetails.HasRows)
                    {
                        userDetails.Read();
                        string userId = userDetails["UserId"].ToString();
                        sessionId = userDetails["SessionId"].ToString();
                        userDetails.Close();
                    }
                    if (!userDetails.IsClosed)
                        userDetails.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return sessionId;
        }

        public bool checkSession(ref string sessionId)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    //check session
                    string cmdString = "SELECT * FROM `bookstore`.`sessionlog`, `bookstore`.`userdetails` WHERE userdetails.UserId = sessionlog.SessionUserId and sessionlog.SessionId=@sessionId and `SessionExpiryTime` > current_timestamp()";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@sessionId", sessionId));
                    MySqlDataReader userDetails = cmd.ExecuteReader();
                    if (userDetails.HasRows)
                    {
                        userDetails.Read();
                        string userId = userDetails["UserId"].ToString();
                        userDetails.Close();
                        //update session
                        cmdString = "UPDATE `bookstore`.`sessionlog` SET SessionId = @newSessionId, `SessionExpiryTime` = date_add(current_timestamp(), interval 30 minute) WHERE `SessionId` = @oldSessionId and `SessionUserId` = @userId"; ;
                        cmd = new MySqlCommand(cmdString, con);
                        cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                        cmd.Parameters.Add(new MySqlParameter("@oldSessionId", sessionId));
                        string newSessionId = sessionIdGenerator();
                        cmd.Parameters.Add(new MySqlParameter("@newSessionId", newSessionId));
                        cmd.ExecuteNonQuery();
                        sessionId = newSessionId;
                        status = true;
                    }
                    if(!userDetails.IsClosed)
                        userDetails.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return status;
        }

        public bool endSession(string sessionId)
        {
            bool status = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    //check session
                    string cmdString = "SELECT * FROM `bookstore`.`sessionlog`, `bookstore`.`userdetails` WHERE userdetails.UserId = sessionlog.SessionUserId and sessionlog.SessionId=@sessionId and `SessionExpiryTime` > current_timestamp()";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@sessionId", sessionId));
                    MySqlDataReader userDetails = cmd.ExecuteReader();
                    if (userDetails.HasRows)
                    {
                        userDetails.Read();
                        string userId = userDetails["UserId"].ToString();
                        userDetails.Close();
                        //update session
                        cmdString = "UPDATE `bookstore`.`sessionlog` SET `SessionExpiryTime` = current_timestamp() WHERE `SessionUserId` = @userId and `SessionId` = @oldSessionId";
                        cmd = new MySqlCommand(cmdString, con);
                        cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                        cmd.Parameters.Add(new MySqlParameter("@oldSessionId", sessionId));
                        cmd.ExecuteNonQuery();
                        status = true;
                    }
                    userDetails.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return status;
        }

        private string sessionIdGenerator()
        {
            char[] chars = new char[65];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            byte[] data = new byte[1];

            using(RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[10];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(10);
            foreach(byte b in data)
            {
                result.Append(chars[b % chars.Length]);
            }
            return result.ToString();
        }
    }
}