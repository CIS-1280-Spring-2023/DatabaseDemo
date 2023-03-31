using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFDemo.Data;
using WPFDemo.Models;

namespace WPFDemo.Views
{
    /// <summary>
    /// Interaction logic for SchoolWindow.xaml
    /// </summary>
    public partial class SchoolWindow : Window
    {
        School school;
        CourseRepo courseRepo;
        CampusRepo campusRepo;
        MajorRepo majorRepo;
        StudentRepo studentRepo;

        public SchoolWindow()
        {
            InitializeComponent();
            school = new School();
            courseRepo = new CourseRepo();
            campusRepo = new CampusRepo();
            majorRepo = new MajorRepo();    
            studentRepo = new StudentRepo();

            RefreshData();
            lbCampuses.ItemsSource = school.Campuses;
            lbCourses.ItemsSource = school.Course;
            lbMajor.ItemsSource = school.Majors;
            lbStudents.ItemsSource = school.Students;
        }

        private void RefreshData()
        {            
            school.Campuses = campusRepo.GetCampuses();
            school.Majors = majorRepo.GetMajors();
            school.Course = courseRepo.GetCourses();
            school.Students = studentRepo.GetStudents();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student();
            StudentWindow studentWindow = new StudentWindow(newStudent, school);

            studentWindow.ShowDialog();
            if(studentWindow.DialogResult == true)
            {
                //Add to database
                studentRepo.InsertStudent(newStudent);
                //Update school list
                school.Students = studentRepo.GetStudents();
                //refresh list from database
                lbStudents.ItemsSource = null;
                lbStudents.ItemsSource = school.Students;
            }
        }

        private void btnAddCampus_Click(object sender, RoutedEventArgs e)
        {
            Campus campus = new Campus();
            campus.Name = txbCampus.Text;

            //Update db with new campus
            campusRepo.InsertCampus(campus);
            //Update school list 
            school.Campuses = campusRepo.GetCampuses();
            lbCampuses.ItemsSource = null;
            lbCampuses.ItemsSource = school.Campuses;
        }

        private void btnAddMajor_Click(object sender, RoutedEventArgs e)
        {
            string newMajor = txbMajor.Text;
            //Update db with new major
            majorRepo.InsertMajor(newMajor);
            //Update majors list 
            school.Majors = majorRepo.GetMajors();
            lbMajor.ItemsSource = null;
            lbMajor.ItemsSource = school.Majors;
        }

        private void btnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            string newCourse = txbCourse.Text;
            //Update db with new course
            courseRepo.InsertCourse(newCourse);
            //Update courses list 
            school.Course = courseRepo.GetCourses();
            lbCourses.ItemsSource = null;
            lbCourses.ItemsSource = school.Course;
        }

        private void lbCampuses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var campus = (Campus) lbCampuses.SelectedItem;
            campusRepo.Delete(campus.Id);
            //Update school list 
            school.Campuses = campusRepo.GetCampuses();
            lbCampuses.ItemsSource = null;
            lbCampuses.ItemsSource = school.Campuses;

        }

        private void lbStudents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
                

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Student student = (Student)lbStudents.SelectedItem;
            StudentWindow studentWindow = new StudentWindow(student, school);

            if (studentWindow.ShowDialog() == true)
            {
                studentRepo.UpdateStudent(studentWindow.Student);
                //Update school list
                school.Students = studentRepo.GetStudents();
                //refresh list from database
                lbStudents.ItemsSource = null;
                lbStudents.ItemsSource = school.Students;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Student student = (Student)lbStudents.SelectedItem;
            studentRepo.DeleteStudent(student.Id);
            //Update school list
            school.Students = studentRepo.GetStudents();
            //refresh list from database
            lbStudents.ItemsSource = null;
            lbStudents.ItemsSource = school.Students;
        }
    }
}
