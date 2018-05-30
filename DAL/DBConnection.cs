using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data;
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
            SqlConnection con = new SqlConnection(conString);
            string cmdString = "select * from authors";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
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
            using (SqlConnection con = new SqlConnection(conString)) { 
                string cmdString = "insert into authors(AuthorFname, AuthorLname) values (@authorFname, @authorLname)";
                SqlCommand cmd = new SqlCommand(cmdString, con);
                cmd.Parameters.Add(new SqlParameter("@authorFname", _author.AuthorFname));
                cmd.Parameters.Add(new SqlParameter("@authorLname",_author.AuthorLname));
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
            SqlConnection con = new SqlConnection(conString);
            string cmdString = "select * from genrelist";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
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
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string cmdString = "insert into authors(GenreName) values (@genrename)";
                    SqlCommand cmd = new SqlCommand(cmdString, con);
                    cmd.Parameters.Add(new SqlParameter("@genrename", _genre.GenreName));
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
            SqlConnection con = new SqlConnection(conString);
            string cmdString = "select * from publishers";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
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
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string cmdString = "insert into publishers(PublisherName) values (@PublisherName)";
                    SqlCommand cmd = new SqlCommand(cmdString, con);
                    cmd.Parameters.Add(new SqlParameter("@PublisherName", publisher.PublisherName));
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
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string cmdString = "select * from getbooks";
                    SqlCommand cmd = new SqlCommand(cmdString, con);
                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
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

                            bookList.Add(new Book(title, genre, author, description, publisher, rating, cost, thumb, stock));
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

        public bool AddBook(Book book)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string cmdString = "insert into books(books.BookTitle, books.BookGenre, books.BookAuthorId,books.BookDescription,books.BookPublisherId,books.BookCost,books.BookThumb,books.BookStock)"
                                        +" values(@BookTitle, (select genrelist.GenreId from genrelist where genrelist.GenreName = @GenreName),(select authors.AuthorId from authors where authors.AuthorFname = @AuthorFname and authors.AuthorLname = @AuthorLname),"+
                                        " @BookDescription, (select publishers.PublisherId from publishers where publishers.PublisherName = @PublisherName), @BookCost, @BookThumb , @BookStock)";
                    SqlCommand cmd = new SqlCommand(cmdString, con);
                    cmd.Parameters.Add(new SqlParameter("@BookTitle", book.BookTitle));
                    cmd.Parameters.Add(new SqlParameter("@GenreName", book.BookGenre.GenreName));
                    cmd.Parameters.Add(new SqlParameter("@AuthorFname", book.BookAuthor.AuthorFname));
                    cmd.Parameters.Add(new SqlParameter("@AuthorLname", book.BookAuthor.AuthorLname));
                    cmd.Parameters.Add(new SqlParameter("@BookDescription", book.BookDescription));
                    cmd.Parameters.Add(new SqlParameter("@PublisherName", book.BookPublisher.PublisherName));
                    cmd.Parameters.Add(new SqlParameter("@BookCost", book.BookCost));
                    cmd.Parameters.Add(new SqlParameter("@BookThumb", book.BookThumb));
                    cmd.Parameters.Add(new SqlParameter("@BookStock", book.BookStock));
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


    }
}
