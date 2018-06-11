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
        public IList<Author> getAuthors()
        {
            IList<Author> authorList = new List<Author>();
            MySqlConnection con = new MySqlConnection(conString);
            string cmdString = "select * from authors";
            MySqlCommand cmd = new MySqlCommand(cmdString, con);
            try
            {
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    string fname = reader["AuthorFname"].ToString();
                    string lname = reader["AuthorLname"].ToString();
                    authorList.Add(new Author(fname, lname));
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
                string cmdString = "insert into authors(AuthorFname, AuthorLname) values (@authorFname, @authorLname)";
                MySqlCommand cmd = new MySqlCommand(cmdString, con);
                cmd.Parameters.Add(new MySqlParameter("@authorFname", _author.AuthorFname));
                cmd.Parameters.Add(new MySqlParameter("@authorLname",_author.AuthorLname));
                cmd.ExecuteNonQuery();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                    return true;
            }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public IList<Genre> getGenre()
        {
            IList<Genre> genreList = new List<Genre>();
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
                    genreList.Add(new Genre(name));
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
                    string cmdString = "insert into authors(GenreName) values (@genrename)";
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public IList<Publisher> getPublisher()
        {
            IList<Publisher> publisherList = new List<Publisher>();
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
                    publisherList.Add(new Publisher( name));
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
                    string cmdString = "insert into publishers(PublisherName) values (@PublisherName)";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@PublisherName", publisher.PublisherName));
                    cmd.ExecuteNonQuery();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    return true;
                }
            }
            catch (Exception ex)
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
                    string cmdString = "select * from getbooks";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    try
                    {
                        con.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string id = reader["BookId"].ToString();
                            string title = reader["BookTitle"].ToString();
                            int rating = int.Parse(reader["BookRating"].ToString());
                            Genre genre = new Genre(reader["GenreName"].ToString());
                            double cost = double.Parse(reader["BookCost"].ToString());
                            Author author = new Author(reader["Authorfname"].ToString(), reader["Authorlname"].ToString());
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

        public IList<Book> getBookByName(string bookTitle)
        {
            IList<Book> book = null;
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                string cmdString = "select * from getbooks where BookTitle = @bookName";
                MySqlCommand cmd = new MySqlCommand(cmdString, con);
                cmd.Parameters.Add(new MySqlParameter("@bookName", bookTitle));
                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        string title = reader["BookTitle"].ToString();
                        int rating = int.Parse(reader["BookRating"].ToString());
                        Genre genre = new Genre(reader["GenreName"].ToString());
                        double cost = double.Parse(reader["BookCost"].ToString());
                        Author author = new Author(reader["Authorfname"].ToString(), reader["Authorlname"].ToString());
                        Publisher publisher = new Publisher(reader["PublisherName"].ToString());
                        string thumb = reader["BookThumb"].ToString();
                        string description = reader["BookDescription"].ToString();
                        int stock = int.Parse(reader["BookStock"].ToString());

                        book.Add(new Book(title, genre, author, description, publisher, rating, cost, thumb, stock));
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
                return book;
            }
            
        }

        public bool addBook(Book book)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    string cmdString = "insert into books(books.BookTitle, books.BookGenre, books.BookAuthorId,books.BookDescription,books.BookPublisherId,books.BookCost,books.BookThumb,books.BookStock)"
                                        +" values(@BookTitle, (select genrelist.GenreId from genrelist where genrelist.GenreName = @GenreName),(select authors.AuthorId from authors where authors.AuthorFname = @AuthorFname and authors.AuthorLname = @AuthorLname),"+
                                        " @BookDescription, (select publishers.PublisherId from publishers where publishers.PublisherName = @PublisherName), @BookCost, @BookThumb , @BookStock)";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@BookTitle", book.BookTitle));
                    cmd.Parameters.Add(new MySqlParameter("@GenreName", book.BookGenre.GenreName));
                    cmd.Parameters.Add(new MySqlParameter("@AuthorFname", book.BookAuthor.AuthorFname));
                    cmd.Parameters.Add(new MySqlParameter("@AuthorLname", book.BookAuthor.AuthorLname));
                    cmd.Parameters.Add(new MySqlParameter("@BookDescription", book.BookDescription));
                    cmd.Parameters.Add(new MySqlParameter("@PublisherName", book.BookPublisher.PublisherName));
                    cmd.Parameters.Add(new MySqlParameter("@BookCost", book.BookCost));
                    cmd.Parameters.Add(new MySqlParameter("@BookThumb", book.BookThumb));
                    cmd.Parameters.Add(new MySqlParameter("@BookStock", book.BookStock));
                    cmd.ExecuteNonQuery();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
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
                    string contactNo = dataSet["UserContactNo"].ToString();
                    string userComment = dataSet["CustomerComment"].ToString();
                    int userRating = int.Parse(dataSet["CustomerRating"].ToString());
                    reviewList.Add( new UserReview(new User(uName,fname,lname,email,contactNo),userRating,userComment));
                }
            }
            return reviewList;
        }

        public bool addUserReview(UserReview review, Book book)
        {
            string userId;
            using(MySqlConnection con = new MySqlConnection(conString))
            {

                string cmdString = "select UserId from userreview where UserUname = '@userUname'";
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
                cmdString = "INSERT INTO `bookstore`.`customerreview`(`CustomerId`,`BookId`,`CustomerRating`,`CustomerComment`) VALUES(@userId,@bookId,@bookRating,'@bookComment')";
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
                    string cmdString = "INSERT INTO `bookstore`.`userdetails`(`UserUname`,`UserFname`,`UserLname`,`UserPassword`,`UserEmail`,`UserContactNo`)VALUES( @userName, @userFname, @userLname, @userPass, @userEmail, @userContactNo);";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", newUser.UserName));
                    cmd.Parameters.Add(new MySqlParameter("@userFname", newUser.FirstName));
                    cmd.Parameters.Add(new MySqlParameter("@userLname", newUser.LastName));
                    cmd.Parameters.Add(new MySqlParameter("@userPass", userPass));
                    cmd.Parameters.Add(new MySqlParameter("@userEmail", newUser.Email));
                    cmd.Parameters.Add(new MySqlParameter("@userContactNo", newUser.ContactNo));
                    cmd.ExecuteNonQuery();
                    status = true;
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return status;
        }

        public IList<User> getAllUser()
        {
            IList<User> usersList = new List<User>();
            string cmdString = "SELECT `userdetails`.`UserId`, `userdetails`.`UserUname`, `userdetails`.`UserFname`, `userdetails`.`UserLname`,`userdetails`.`UserEmail`, `userdetails`.`UserContactNo`FROM `bookstore`.`userdetails`";
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
                        string userContactNo = dataSet["UserContactNo"].ToString();
                        usersList.Add(new User(userName, userFname, userLname, userEmail, userContactNo));
                    }
                }

            }

            return usersList;
        }

        public bool validateUser(User user, string pass)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    string cmdString = "SELECT `userdetails`.`UserId`, `userdetails`.`UserUname`, `userdetails`.`UserFname`, `userdetails`.`UserLname`,`userdetails`.`UserEmail`, `userdetails`.`UserContactNo`FROM `bookstore`.`userdetails`WHERE userdetails.UserUname = @userName AND userdetails.UserPassword = @password";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                    cmd.Parameters.Add(new MySqlParameter("@password", pass));

                    MySqlDataReader userDetails = cmd.ExecuteReader();
                    if (userDetails.HasRows)
                    {
                        userDetails.Read();
                        string userId = userDetails["UserId"].ToString();
                        user.FirstName = userDetails["UserFname"].ToString();
                        user.LastName = userDetails["UserLname"].ToString();
                        user.Email = userDetails["UserEmail"].ToString();
                        user.ContactNo = userDetails["UserContactNo"].ToString();
                        userDetails.Close();

                        cmdString = "SELECT cart.CartBookId,cart.CartQuantity FROM cart where cart.CartCusId=  @userId";
                        cmd = new MySqlCommand(cmdString, con);
                        cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                        List<Book> bookList = (List<Book>)getBooks();
                        MySqlDataReader userCartDetails = cmd.ExecuteReader();
                        if(userCartDetails.HasRows)
                        {
                            while (userCartDetails.Read())
                            {
                                string bookId = userCartDetails["CartBookId"].ToString();
                                int quantity = int.Parse(userCartDetails["CartQuantity"].ToString());
                                foreach (Book book in bookList)
                                {
                                    if (book.BookId == bookId)
                                    {
                                        user.CartBookList.Add(new BookCount(book,quantity));
                                        break;
                                    }

                                }
                            }
                        }
                        userCartDetails.Close();
                        cmdString = "SELECT * FROM orders where CustomerId = @userId";
                        cmd = new MySqlCommand(cmdString, con);
                        cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                        MySqlDataReader userOrderDetails = cmd.ExecuteReader();
                        if (userOrderDetails.HasRows)
                        {
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
                                string orderData = userOrderDetails["OrderDate"].ToString();
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
                                                bookList1.Add(new BookCount(book,int.Parse(quantity)));
                                                break;
                                            }
                                        }
                                    }

                                }
                                user.OrderList.Add(new Order(orderShipName, address, orderContactNo, bookList1, orderShipped, orderTransactionId));
                            }
                        }
                        userOrderDetails.Close();
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

            }
            return status;
        }

        public bool validateAdmin(User user)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    string cmdstring = "select * from userdetails where UserRole = 'admin' && UserUname = '@userName'";
                    MySqlCommand cmd = new MySqlCommand(cmdstring, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
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
                string cmdString = "select cart.CartBookId, cart.CartQuantity from cart where CartCusId=@userName";
                MySqlCommand cmd = new MySqlCommand(cmdString, con);
                cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cartsBook.Add(new Cart(reader["CartBookId"].ToString(),reader["CartQuantity"].ToString()));
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

        public bool addBookToCart(User user,Book book, int quantity)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    string cmdstring = "INSERT INTO `bookstore`.`cart`(`CartCusId`,`CartBookId`,`CartQuantity`)VALUES('(select `UserId form userdetails where UserName = '@username')','@bookId',@quantity) on duplicate key update `CartQuantiy` = values(`CartQuantity`)";
                    MySqlCommand cmd = new MySqlCommand(cmdstring, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                    cmd.Parameters.Add(new MySqlParameter("@bookId", book.BookId));
                    cmd.Parameters.Add(new MySqlParameter("@quantity", quantity));
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

        public bool remBookFromCart(User user, Book book, int remQuantity)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    string cmdstring = "update `bookstore`.`cart` set `CartQuantity` = `CartQuantity- @quantity where CartCustId=('(select `UserId form userdetails where UserName = '@username')' and CartBookId='@bookId')";
                    MySqlCommand cmd = new MySqlCommand(cmdstring, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                    cmd.Parameters.Add(new MySqlParameter("@bookId", book.BookId));
                    cmd.Parameters.Add(new MySqlParameter("@quantity", remQuantity));
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
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    String userId;
                    string cmdString = "select UserId from userdetails where UserUname = '@userName'";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                    MySqlDataReader dataset = cmd.ExecuteReader();
                    userId = dataset["UserId"].ToString();

                    cmdString = "insert into orders(CustomerId, OrderShipName, OrderShipAddress, OrderCity, OrderState, OrderZip, OrderCountry, OrderContactNo, OrderTransactionId) values (@userId, '@recieverName', '@recieverAddr', '@recieverCity', '@recieverState', '@recieverZip', '@recieverCountry', '@recieverContactNo', '@recieverTransactionId')";
                    cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                    cmd.Parameters.Add(new MySqlParameter("@recieverName", order.ReceiverName));
                    cmd.Parameters.Add(new MySqlParameter("@recieverAddr", order.ReceiverAddr.LocalAddr));
                    cmd.Parameters.Add(new MySqlParameter("@recieverCity", order.ReceiverAddr.City));
                    cmd.Parameters.Add(new MySqlParameter("@recieverState", order.ReceiverAddr.State));
                    cmd.Parameters.Add(new MySqlParameter("@recieverZip", order.ReceiverAddr.ZipCode));
                    cmd.Parameters.Add(new MySqlParameter("@recieverCountry", order.ReceiverAddr.Country));
                    cmd.Parameters.Add(new MySqlParameter("@recieverContactNo", order.ReceiverContactNo));
                    cmd.Parameters.Add(new MySqlParameter("@recieverTransactionId", order.OrderTransactonId));
                    cmd.ExecuteNonQuery();

                    string orderId;
                    cmdString = "SELECT max(orderid) as orderId FROM bookstore.orders where CustomerId = @userId group by CustomerId";
                    cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                    dataset = cmd.ExecuteReader();
                    orderId = dataset["orderId"].ToString();

                    IList<Cart> cart = getCartsBook(user);
                    cmdString = "insert into orderdetails values('" + orderId + "','" + cart[0].BookId + "'," + cart[0].BookQuantity + ")";
                    for (int i = 1;i < cart.Count;i++)
                    {
                        cmdString += ",('" + orderId + "','" + cart[i].BookId + "','" + cart[i].BookQuantity + "')";
                    }
                    cmd = new MySqlCommand(cmdString, con);
                    cmd.ExecuteNonQuery();

                    cmdString = "delete from cart where CartCusId = '@userId'";
                    cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userId", userId));
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
                        string orderData = userOrderDetails["OrderDate"].ToString();
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

                            foreach(User user in userList)
                            {
                                if(user.UserName == customerUserName)
                                {
                                    orderList.Add(user, new Order(orderShipName, address, orderContactNo, bookList1, orderShipped, orderTransactionId));
                                    break;
                                }
                            }
                        }

                    }
                }
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
                    cmdString = "INSERT INTO `bookstore`.`sessionlog`(`SessionId`,`SessionExpiryTime`,`SessionUserId`)VALUES(@sessionId,date_add(current_timestamp(), interval 30 minute),@userId)";
                    cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                    sessionId = sessionIdGenerator();
                    cmd.Parameters.Add(new MySqlParameter("@sessionId", sessionId));
                    cmd.ExecuteNonQuery();
                }
            }
            return sessionId;
        }

        public bool checkSession(User user)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    //check session
                    string cmdString = "SELECT * FROM `bookstore`.`sessionlog`, `bookstore`.`userdetails` WHERE userdetails.UserUname = @userName AND userdetails.UserId = sessionlog.SessionUserId and sessionlog.SessionId=@sessionId";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                    cmd.Parameters.Add(new MySqlParameter("@sessionId", user.SessionId));
                    MySqlDataReader userDetails = cmd.ExecuteReader();
                    if (userDetails.HasRows)
                    {
                        userDetails.Read();
                        string userId = userDetails["UserId"].ToString();
                        userDetails.Close();
                        //update session
                        cmdString = "UPDATE `bookstore`.`sessionlog` SET `SessionId` = '@newSessionId', `SessionExpiryTime` = date_add(current_timestamp(), interval 30 minute), `SessionUserId` = '@userId' WHERE `SessionId` = '@oldSessionId'"; ;
                        cmd = new MySqlCommand(cmdString, con);
                        cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                        cmd.Parameters.Add(new MySqlParameter("@oldSessionId", user.SessionId));
                        string newSessionId = sessionIdGenerator();
                        cmd.Parameters.Add(new MySqlParameter("@newSessionId", newSessionId));
                        cmd.ExecuteNonQuery();
                        user.SessionId = newSessionId;
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

        public bool endSession(Claim user)
        {
            bool status = false;

            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    //check session
                    string cmdString = "SELECT * FROM `bookstore`.`sessionlog`, `bookstore`.`userdetails` WHERE userdetails.UserUname = @userName AND userdetails.UserId = sessionlog.SessionUserId and sessionlog.SessionId=@sessionId";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                    cmd.Parameters.Add(new MySqlParameter("@sessionId", user.SessionId));
                    MySqlDataReader userDetails = cmd.ExecuteReader();
                    if (userDetails.HasRows)
                    {
                        userDetails.Read();
                        string userId = userDetails["UserId"].ToString();
                        userDetails.Close();
                        //update session
                        cmdString = "UPDATE `bookstore`.`sessionlog` SET `SessionExpiryTime` = current_timestamp(), `SessionUserId` = '@userId' WHERE `SessionId` = '@oldSessionId'"; ;
                        cmd = new MySqlCommand(cmdString, con);
                        cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                        cmd.Parameters.Add(new MySqlParameter("@oldSessionId", user.SessionId));
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