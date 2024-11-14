using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student_information_system
{
    public partial class UpdateStudentForm : Form
    {
        private int _studentId;
        private string _connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\MARL DENZELL SILVA\\source\\repos\\student-information-system\\students-information-system.mdf\";Integrated Security=True;Connect Timeout=30";
        public UpdateStudentForm(string studentId)
        {
            InitializeComponent();
            _studentId = Int32.Parse(studentId);
        }

        private void UpdateStudentForm_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "SELECT * FROM Students WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = _studentId;

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
                    MessageBox.Show("Something went wrong. Try again later.");
                }
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
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

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to update this data?", "Confirm", MessageBoxButtons.YesNo);

            if (dialogResult != DialogResult.Yes)
            {
                return;
            }

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "UPDATE Students SET FirstName = @firstName, MiddleName = @middleName, LastName = @lastName, Birthdate = @birthdate, Gender = @gender, Phone = @phone, Address = @address WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@firstName", SqlDbType.VarChar, 255).Value = firstName;
                cmd.Parameters.Add("@middleName", SqlDbType.VarChar, 255).Value = middleName;
                cmd.Parameters.Add("@lastName", SqlDbType.VarChar, 255).Value = lastName;
                cmd.Parameters.Add("@birthdate", SqlDbType.Date).Value = birthdate;
                cmd.Parameters.Add("@gender", SqlDbType.VarChar, 255).Value = gender;
                cmd.Parameters.Add("@phone", SqlDbType.VarChar, 255).Value = phone;
                cmd.Parameters.Add("@address", SqlDbType.VarChar, 255).Value = address;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = _studentId;


                try
                {
                    conn.Open();
                    Int32 rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated successfully. Refresh the table to see updated data.");
                    conn.Close();
                    this.Hide();
                    //Console.WriteLine("RowsAffected: " + rowsAffected);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong. Try again later.");
                }
            }
        }
    }
}
