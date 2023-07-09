using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Zadatak.Models;
using Zadatak.Utils;
using Publisher = Zadatak.Models.Publisher;

namespace Zadatak.Dal
{
    public class PublisherRepository : IRepository<Publisher>
    {
        private static readonly string cs = EncryptionUtils.Decrypt(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);

        public void Add(Publisher publisher)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "AddPublisher";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Publisher.Name), publisher.Name);
                    SqlParameter idPublisher = new SqlParameter(nameof(Publisher.Id), SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idPublisher);
                    cmd.ExecuteNonQuery();
                    publisher.Id = (int)idPublisher.Value;
                }
            }
        }

        public void Delete(Publisher publisher)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "DeletePublisher";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Publisher.Id), publisher.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Publisher Get(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetPublisher";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Publisher.Id), id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadPublisher(dr);
                        }
                    }
                }
            }
            throw new Exception("Publisher does not exist");
        }

        public IList<Publisher> GetAll()
        {
            IList<Publisher> publishers = new List<Publisher>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "GetPublishers";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            publishers.Add(ReadPublisher(dr));
                        }
                    }
                }
            }
            return publishers;
        }

        public void Update(Publisher publisher)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "UpdatePublisher";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(nameof(Publisher.Name), publisher.Name);
                    cmd.Parameters.AddWithValue(nameof(Publisher.Id), publisher.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static Publisher ReadPublisher(SqlDataReader dr) => new Publisher
        {
            Id = (int)dr[nameof(Publisher.Id)],
            Name = dr[nameof(Publisher.Name)].ToString()
        };
    }
}
