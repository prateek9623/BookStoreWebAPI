using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using BOL;

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

        public IList<Author> GetAuthors()
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

        public bool AddAuthor(Author _author)
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

        public IList<Genre> GetGenre()
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
        
        public bool AddGenre(Genre _genre)
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

        public IList<Publisher> GetPublisher()
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
        
        public bool AddPublisher(Publisher publisher)
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

        public IList<Book> GetBooks()
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

        public Book GetBookByName(string bookTitle)
        {
            Book book = null;
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

                        book = new Book(title, genre, author, description, publisher, rating, cost, thumb, stock);
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

        public bool AddBook(Book book)
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

        public bool validateUser(User user, string pass)
        {
            bool status = false;
            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    string cmdString = "SELECT `userdetails`.`UserUname`, `userdetails`.`UserFname`, `userdetails`.`UserLname`,`userdetails`.`UserEmail`, `userdetails`.`UserContactNo`FROM `bookstore`.`userdetails`WHERE userdetails.UserUname = @userName AND userdetails.UserPassword = @password";
                    MySqlCommand cmd = new MySqlCommand(cmdString, con);
                    cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                    cmd.Parameters.Add(new MySqlParameter("@password", pass));

                    MySqlDataReader userDetails = cmd.ExecuteReader();
                    if (userDetails.HasRows) { 
                        user.FirstName=userDetails["UserFname"].ToString();
                        user.LastName = userDetails["UserLname"].ToString();
                        user.Email = userDetails["UserEmail"].ToString();
                        user.ContactNo = userDetails["UserContactNo"].ToString();
                        userDetails.Close();

                        cmdString = "SELECT cart.CartBookId,cart.CartQuantity FROM cart,userdetails where cart.CartCusId= userdetails.UserId and userdetails.UserUname = @userName";
                        cmd = new MySqlCommand(cmdString, con);
                        cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));

                        List<Book> bookList = (List<Book>)GetBooks();
                        MySqlDataReader userCartDetails = cmd.ExecuteReader();
                        Dictionary<Book, int> cartBookList = new Dictionary<Book, int>();
                        while (userCartDetails.Read())
                        {
                            string bookId = userCartDetails["CartBookId"].ToString();
                            int quantity = int.Parse(userCartDetails["CartQuantity"].ToString());
                            foreach (Book book in bookList)
                            {
                                if(book.BookId == bookId)
                                {
                                    cartBookList.Add(book, quantity);
                                    break;
                                }

                            }
                        }
                        user.CartBookList = cartBookList;

                        cmdString = "SELECT * FROM orders where CustomerId = (select UserId from userdetails where UserUname = '@userName')";
                        cmd = new MySqlCommand(cmdString, con);
                        cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                        MySqlDataReader userOrderDetails = cmd.ExecuteReader();

                        while (userOrderDetails.Read())
                        {
                            string orderId = userOrderDetails["OrderId"].ToString();
                            string orderAmount = userOrderDetails["OrderAmount"].ToString();
                            string orderShipName = userOrderDetails["OrderShipName"].ToString();
                            string orderShipAddress = userOrderDetails["OrderShipAddress"].ToString();
                            string orderCity = userOrderDetails["OrderCity"].ToString();
                            string orderState = userOrderDetails["OrderState"].ToString();
                            string orderZip = userOrderDetails["OrderZip"].ToString();
                            string orderCountry = userOrderDetails["OrderCountry"].ToString();
                            string orderContactNo = userOrderDetails["OrderContactNo."].ToString();
                            string orderData = userOrderDetails["OrderData"].ToString();
                            bool orderShipped = userOrderDetails["OrderShipped"].ToString().Contains("1");
                            string orderTransactionId = userOrderDetails["OrderTransactionId"].ToString();
                            Address address = new Address(orderShipAddress, orderCity, orderState, orderZip, orderCountry);
                            cmdString = "select * from orderdetails where DetailOrderId = '@orderId'";
                            MySqlCommand cmd1 = new MySqlCommand(cmdString, con);
                            cmd1.Parameters.Add(new MySqlParameter("@orderId", orderId));
                            MySqlDataReader userOrderDetail = cmd1.ExecuteReader();
                            IDictionary<Book, int> bookList1 = new Dictionary<Book, int>();
                            while (userOrderDetail.Read())
                            {
                                string bookId = userOrderDetail["DetailProductId"].ToString();
                                string quantity = userCartDetails["DetailQuantity"].ToString();
                                foreach (Book book in bookList)
                                {
                                    if (book.BookId == bookId)
                                    {
                                        bookList1.Add(book,int.Parse(quantity));
                                        break;
                                    }
                                }
                            }
                            user.OrderList.Add(new Order(orderShipName, address, orderContactNo, bookList1, orderShipped, orderTransactionId));
                        }

                        userDetails.Close();
                        userCartDetails.Close();
                        userOrderDetails.Close();
                    }
                    else
                    {
                        status = false;
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
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
    }
}
