using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDemo.Data
{
    public class MajorRepo
    {
        public List<Major> GetMajors()
        {
            string sqlStr = "SELECT * FROM Major";
            List<Major> majors = new List<Major>();
            string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand selectCmd = new SqlCommand(sqlStr, conn);
                conn.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();
                while (dr.Read())
                {
                    Major major = new Major();
                    major.Id = dr.GetInt32(0);
                    major.Title = dr.GetString(1);
                    majors.Add(major);
                }
            }
            return majors;
        }

        public Major GetMajor(int id)
        {
            string sqlStr = "SELECT * FROM Major Where Id = @Id";
            Major major = new Major();
            string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand selectCmd = new SqlCommand(sqlStr, conn);
                selectCmd.Parameters.AddWithValue("Id", id);
                conn.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();
                dr.Read();              
                major.Id = dr.GetInt32(0);
                major.Title = dr.GetString(1);
            }
            return major;
        }

        public void InsertMajor(string title)
        {
            string sqlStr = $"INSERT INTO Major (Title) VALUES (@Title);";
            string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                cmd.Parameters.AddWithValue("@Title", title);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
