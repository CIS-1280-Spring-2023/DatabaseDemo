using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDemo.Data
{
    public class CourseRepo
    {
        public List<string> GetCourses()
        {
            string sqlStr = "SELECT * FROM Course";
            List<string> items = new List<string>();
            string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand selectCmd = new SqlCommand(sqlStr, conn);
                conn.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();
                while (dr.Read())
                {
                    items.Add(dr.GetString(1));
                }
            }
            return items;
        }

        public void InsertCourse(string title)
        {
            string sqlStr = $"INSERT INTO Course (Title) VALUES (@Title);";
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
