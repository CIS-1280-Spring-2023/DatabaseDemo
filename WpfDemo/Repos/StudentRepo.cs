using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDemo.Data
{
    public class StudentRepo
    {
        public List<Student> GetStudents()
        {
            string sqlStr = "SELECT * FROM Student";
            List<Student> students = new List<Student>();
            string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand selectCmd = new SqlCommand(sqlStr, conn);
                conn.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();
                while (dr.Read())
                {
                    Student student = new Student();
                    student.Id = dr.GetInt32(0);
                    student.FirstName = dr.GetString(1);
                    student.LastName = dr.GetString(2);
                    student.StudentNumber = dr.GetString(3);
                    int majorId = dr.GetInt32(4);
                    MajorRepo majorRepo = new MajorRepo();
                    Major major = majorRepo.GetMajor(majorId);
                    students.Add(student);
                }
            }
            return students;
        }

        public void InsertStudent(Student student)
        {
            string sqlStr = $"INSERT INTO Student (FirstName, LastName, StudentNumber, MajorId) VALUES (@FirstName, @LastName, @StudentNumber, @MajorId);";
            string connStr = ConfigurationManager.ConnectionStrings["SchoolDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);
                cmd.Parameters.AddWithValue("@MajorId", student.Major.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
