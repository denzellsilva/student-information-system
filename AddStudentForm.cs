using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace student_information_system
{
    public partial class AddStudentForm : Form
    {
        public AddStudentForm()
        {
            InitializeComponent();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            string firstName = firstNameTextBox.Text;
            string middleName = middleNameTextBox.Text;
            string lastName = lastNameTextBox.Text;
            DateTime birthdate = birthDateBox.Value;
            string gender = "";
            string phone = phoneTextBox.Text;
            string address = addressTextBox.Text;

            if (maleRadioBtn.Checked)
            {
                gender = maleRadioBtn.Text;
            }
            else if (femaleRadioBtn.Checked)
            {
                gender = femaleRadioBtn.Text;
            }

            if (firstName == "" || middleName == "" || lastName == "" || gender == "" || phone == "" || address == "")
            {
                MessageBox.Show("Fill in all fields");
                return;
            }

            string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\MARL DENZELL SILVA\\source\\repos\\student-information-system\\students-information-system.mdf\";Integrated Security=True;Connect Timeout=30";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Students (FirstName, MiddleName, LastName, Birthdate, Gender, Phone, Address) VALUES (@firstName, @middleName, @lastName, @birthdate, @gender, @phone, @address)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@firstName", SqlDbType.VarChar, 255).Value = firstName;
                cmd.Parameters.Add("@middleName", SqlDbType.VarChar, 255).Value = middleName;
                cmd.Parameters.Add("@lastName", SqlDbType.VarChar, 255).Value = lastName;
                cmd.Parameters.Add("@birthdate", SqlDbType.Date).Value = birthdate;
                cmd.Parameters.Add("@gender", SqlDbType.VarChar, 255).Value = gender;
                cmd.Parameters.Add("@phone", SqlDbType.VarChar, 255).Value = phone;
                cmd.Parameters.Add("@address", SqlDbType.VarChar, 255).Value = address;

                try
                {
                    conn.Open();
                    Int32 rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("Added successfully. Refresh the table to see updated data.");
                    conn.Close();
                    this.Hide();
                    //Console.WriteLine("RowsAffected: " + rowsAffected);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong. Try again later.");
                    //this.Hide();
                }
            }
        }
    }
}
