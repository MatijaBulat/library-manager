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
    public class AuthorRepository : IRepository<Author>
    {
        private static readonly string cs = EncryptionUtils.Decrypt(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);

        public void Add(Author author)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddAuthor";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Author.Name), author.Name);
                    SqlParameter idAuthor = new SqlParameter(nameof(Author.Id), SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idAuthor);
                    cmd.ExecuteNonQuery();
                    author.Id = (int)idAuthor.Value;
                }
            }
        }

        public void Delete(Author author)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeleteAuthor";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Author.Id), author.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Author Get(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetAuthor";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Author.Id), id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadAuthor(dr);
                        }
                    }
                }
            }
            throw new Exception("Author does not exist");
        }

        public IList<Author> GetAll()
        {
            IList<Author> authors = new List<Author>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetAuthors";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            authors.Add(ReadAuthor(dr));
                        }
                    }
                }
            }
            return authors;
        }

        public void Update(Author author)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UpdateAuthor";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Author.Name), author.Name);    
                    cmd.Parameters.AddWithValue(nameof(Author.Id), author.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static Author ReadAuthor(SqlDataReader dr) => new Author
        {
            Id = (int)dr[nameof(Author.Id)],
            Name = dr[nameof(Author.Name)].ToString()
        };
    }
}
