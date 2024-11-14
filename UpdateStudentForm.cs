using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student_information_system
{
    public partial class UpdateStudentForm : Form
    {
        private string _studentId;
        private string _connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\MARL DENZELL SILVA\\source\\repos\\student-information-system\\students-information-system.mdf\";Integrated Security=True;Connect Timeout=30";
        public UpdateStudentForm(string studentId)
        {
            InitializeComponent();
            _studentId = studentId;
        }

        private void UpdateStudentForm_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "SELECT * FROM Students WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("id", _studentId);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string firstName = reader["FirstName"].ToString();
                        string middleName = reader["MiddleName"].ToString();
                        string lastName = reader["LastName"].ToString();
                        string birthdate = reader["Birthdate"].ToString();
                        string gender = reader["Gender"].ToString();
                        string phone = reader["Phone"].ToString();
                        string address = reader["Address"].ToString();

                        firstNameTextBox.Text = firstName;
                        middleNameTextBox.Text = middleName;
                        lastNameTextBox.Text = lastName;
                        phoneTextBox.Text = phone;
                        addressTextBox.Text = address;
                        
                        if (DateTime.TryParse(birthdate, out DateTime dateValue))
                        {
                            birthDateBox.Value = dateValue;
                        }
                        else
                        {
                            MessageBox.Show("Invalid date format.");
                        }

                        if (maleRadioBtn.Text == gender)
                        {
                            maleRadioBtn.Checked = true;
                        }
                        else
                        {
                            femaleRadioBtn.Checked = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong. Try again later");
                }
            }
        }
    }
}
