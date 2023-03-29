using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDemo.Models;

namespace WPFDemo.Data
{
    public class CampusRepo
    {
        public List<Campus> GetCampuses()
        {
            string sqlStr = "SELECT * FROM Campus";
            List<Campus> campuses = new List<Campus>();
            string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand selectCmd = new SqlCommand(sqlStr, conn);
                conn.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();
                while (dr.Read())
                {
                    Campus campus = new Campus();
                    campus.Id = dr.GetInt32(0);
                    campus.Name = dr.GetString(1);
                    campuses.Add(campus);
                }
            }
            return campuses;
        }

        public Campus GetCampus(int id)
        {
            string sqlStr = "SELECT * FROM Campus WHERE Id = @Id";
            Campus campus = new Campus();
            string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                campus.Id = dr.GetInt32(0);
                campus.Name = dr.GetString(1);
            }
            return campus;
        }

        public void InsertCampus(Campus campus)
        {
            string sqlStr = $"INSERT INTO Campus (Name) VALUES (@Name);";
            string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                cmd.Parameters.AddWithValue("@Name", campus.Name);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string sqlStr = $"DELETE FROM Campus WHERE Id = @Id;";
            string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
