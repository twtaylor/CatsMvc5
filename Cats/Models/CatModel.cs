using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Cats.Models
{
    [DataContract]
    public class CatModel
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Legs { get; set; }

        [DataMember]
        public bool HasTail { get; set; }

        [DataMember]
        public string Type { get; set; }

        public static List<CatModel> GetCats()
        {
            var ret = new List<CatModel>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ID, Name, Legs, HasTail, Type FROM dbo.Cats", connection);

                connection.Open();
                SqlDataReader reader = null;

                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var cat = new CatModel();
                        cat.ID = int.Parse(reader["ID"].ToString());
                        cat.Name = reader["Name"].ToString();
                        cat.Legs = int.Parse(reader["Legs"].ToString());
                        cat.HasTail = bool.Parse(reader["HasTail"].ToString());
                        cat.Type = reader["Type"].ToString();
                        ret.Add(cat);
                    }
                }
                finally
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
            }

            return ret;
        }

        public static bool? Update(CatModel cat)
        {
            int ret = 0;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE dbo.Cats SET Name = @Name, Legs = @Legs, HasTail = @HasTail, Type = @Type WHERE ID = @ID", connection);
                command.Parameters.Add("@ID", SqlDbType.Int).Value = cat.ID;
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = cat.Name;
                command.Parameters.Add("@Legs", SqlDbType.Int).Value = cat.Legs;
                command.Parameters.Add("@HasTail", SqlDbType.Bit).Value = cat.HasTail;
                command.Parameters.Add("@Type", SqlDbType.VarChar).Value = cat.Type;

                try
                {
                    connection.Open();
                    ret = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return ret > 0;
        }

        public static bool? Create(CatModel cat)
        {
            int ret = 0;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO dbo.Cats (Name, Legs, HasTail, Type) VALUES (@Name, @Legs, @HasTail, @Type)", connection);
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = cat.Name;
                command.Parameters.Add("@Legs", SqlDbType.Int).Value = cat.Legs;
                command.Parameters.Add("@HasTail", SqlDbType.Bit).Value = cat.HasTail;
                command.Parameters.Add("@Type", SqlDbType.VarChar).Value = cat.Type;

                try
                {
                    connection.Open();
                    ret = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return ret > 0;
        }

        public static bool? Delete(int id)
        {
            int ret = 0;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM dbo.Cats WHERE ID = @ID", connection);
                command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    ret = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return ret > 0;
        }

    }
}