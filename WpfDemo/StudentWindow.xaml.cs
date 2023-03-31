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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        private Student st;

        public Student Student
        {
            get { return st; }
            set { st = value; }
        }     

        public StudentWindow(School school):this(new Student(),school)
        {            
        }

        public StudentWindow(Student st, School school)
        {
            InitializeComponent();
            this.st = st;
            txbFirstName.Text = st.FirstName;
            txbLastName.Text = st.LastName;
            txbStudentNum.Text = st.StudentNumber;
            if (st.Major != null)
            {
                for (int i = 0; i < school.Majors.Count; i++)
                {
                    if (school.Majors[i].Title == st.Major.Title)
                    {
                        cbxMajor.SelectedIndex = i; break;
                    }
                }
            }
            cbxMajor.SelectedIndex = 0;

            cbxCourse.ItemsSource = school.Course;
            cbxMajor.ItemsSource = school.Majors;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //Instantiate a class object
            //Student st = new Student();

            //Set the values from our controls into the class
            st.FirstName = txbFirstName.Text;
            st.LastName = txbLastName.Text;
            st.StudentNumber = txbStudentNum.Text;
            st.Major = (Major) cbxMajor.SelectedItem;
            List<Assignment> scores = new List<Assignment>();
            foreach (Assignment score in lbScores.Items)
            {
                scores.Add(score);    
            }
            st.Scores = scores;

            //Set the results from the class into a control on the form
            txbResults.Text = st.ToString();

            DialogResult = true;
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Assignment assign = new Assignment();
            assign.Title = cbxCourse.SelectedValue.ToString();
            assign.Score = int.Parse(txbScore.Text);
            lbScores.Items.Add(assign);
        }
    }
}
