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
using System.Data.OleDb;

namespace student_information_system
{
    public partial class StudentListForm : Form
    {
        private string _connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\MARL DENZELL SILVA\\source\\repos\\student-information-system\\students-information-system.mdf\";Integrated Security=True;Connect Timeout=30";

        public StudentListForm()
        {
            InitializeComponent();
        }

        private void StudentsTableLoadData()
        {
            string query = "SELECT * FROM Students";
            SqlConnection conn = new SqlConnection(_connString);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                // SqlDataAdapter adapter = new SqlDataAdapter();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                studentsDataGridView.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading data");
            }
            finally
            {
                conn.Close();
            }
        }

        private void StudentListForm_Load(object sender, EventArgs e)
        {
            searchComboBox.SelectedIndex = 0;
            StudentsTableLoadData();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            Form addStudentForm = new AddStudentForm();
            addStudentForm.ShowDialog();
        }

        private void refreshTbBtn_Click(object sender, EventArgs e)
        {
            StudentsTableLoadData();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (studentsDataGridView.SelectedRows.Count <= 0)
            {
                MessageBox.Show("No row selected.");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete it?", "Confirm", MessageBoxButtons.YesNo);

            if (dialogResult != DialogResult.Yes)
            {
                return;
            }

            DataGridViewRow selectedRow = studentsDataGridView.SelectedRows[0];
            var id = selectedRow.Cells["idDataGridViewTextBoxColumn"].Value;

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "DELETE FROM Students WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                
                try
                {
                    conn.Open();
                    Int32 rowsAffected = cmd.ExecuteNonQuery();
                    StudentsTableLoadData();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong. Try again later");
                }
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (studentsDataGridView.SelectedRows.Count <= 0)
            {
                MessageBox.Show("No row selected.");
                return;
            }

            DataGridViewRow selectedRow = studentsDataGridView.SelectedRows[0];
            var id = selectedRow.Cells["idDataGridViewTextBoxColumn"].Value.ToString();

            Form updateStudentForm = new UpdateStudentForm(id);
            updateStudentForm.ShowDialog();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string value = searchComboBox.SelectedItem.ToString();
            string catagory = value.Replace(" ", "");
            string searchValue = searchTextBox.Text;

            string query = "SELECT * FROM Students WHERE " + catagory + " LIKE '" + searchValue + "%'";
            SqlConnection conn = new SqlConnection(_connString);
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                studentsDataGridView.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading data");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
