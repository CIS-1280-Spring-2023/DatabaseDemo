using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WPFDemo.Models;

namespace WPFDemo
{
    public class School
    {
        private List<string> course;
        private List<Major> majors;
        private List<Campus> campuses;
        private List<Student> students;

        public List<string> Course
        {
            get { return course; }
            set { course = value; }
        }

        public List<Major> Majors
        {
            get { return majors; }
            set { majors = value; }
        }

        public List<Campus> Campuses
        {
            get { return campuses; }
            set { campuses = value; }
        }

        public List<Student> Students
        {
            get { return students; }
            set { students = value; }
        }

        public School()
        {            
        }
    }
}
