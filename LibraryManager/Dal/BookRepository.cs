using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak.Models;
using Zadatak.Utils;
using System.Reflection;

namespace Zadatak.Dal
{
    public class BookRepository : IRepository<Book>
    {
        private static readonly string cs = EncryptionUtils.Decrypt(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);

        public void Add(Book book)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddBook";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("Title", book.Name);
                    cmd.Parameters.AddWithValue(nameof(Book.YearPublished), book.YearPublished);
                    cmd.Parameters.AddWithValue(nameof(Book.ISBN), book.ISBN);
                    cmd.Parameters.Add(new SqlParameter(nameof(Book.Picture), SqlDbType.Binary, book.Picture.Length)
                    {
                        Value = book.Picture
                    });
                    SqlParameter idBook = new SqlParameter(nameof(Book.Id), SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.AddWithValue(nameof(Book.Author), book.Author.Id);
                    cmd.Parameters.AddWithValue(nameof(Book.Publisher), book.Publisher.Id);
                    cmd.Parameters.Add(idBook);
                    cmd.ExecuteNonQuery();
                    book.Id = (int)idBook.Value;
                }
            }
        }

        public void Delete(Book book)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeleteBook";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Book.Id), book.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Book Get(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetBook";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Book.Id), id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadBook(dr);
                        }
                    }
                }
            }
            throw new Exception("Book does not exist");
        }

        public IList<Book> GetAll()
        {
            IList<Book> books = new List<Book>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetBooks";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            books.Add(ReadBook(dr));
                        }
                    }
                }
            }
            return books;
        }

        public void Update(Book book)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UpdateBook";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Title", book.Name);
                    cmd.Parameters.AddWithValue(nameof(Book.Author), book.Author.Id);
                    cmd.Parameters.AddWithValue(nameof(Book.Publisher), book.Publisher.Id);
                    cmd.Parameters.AddWithValue(nameof(Book.YearPublished), book.YearPublished);
                    cmd.Parameters.AddWithValue(nameof(Book.ISBN), book.ISBN);
                    cmd.Parameters.AddWithValue(nameof(Book.Id), book.Id);
                    cmd.Parameters.Add(new SqlParameter(nameof(Book.Picture), SqlDbType.Binary, book.Picture.Length)
                    {
                        Value = book.Picture
                    });
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static Book ReadBook(SqlDataReader dr) => new Book
        {
            Id = (int)dr[nameof(Book.Id)],
            Name = dr["Title"].ToString(),
            Author = RepositoryFactory.GetRepository<Author>().Get((int)dr[2]),
            Publisher = RepositoryFactory.GetRepository<Publisher>().Get((int)dr[3]),
            YearPublished = (int)dr[nameof(Book.YearPublished)],
            ISBN = dr[nameof(Book.ISBN)].ToString(),
            Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, 6) 
        };
    }
}
