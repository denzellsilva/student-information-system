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

        private void LoadData()
        {
            string sql = "SELECT * FROM Students";
            SqlConnection conn = new SqlConnection(_connString);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
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
            SearchComboBox.SelectedIndex = 0;
            LoadData();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            Form addStudentForm = new AddStudentForm();
            addStudentForm.ShowDialog();
        }

        private void refreshTbBtn_Click(object sender, EventArgs e)
        {
            LoadData();
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
                    LoadData();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong. Try again later");
                }
            }
        }
    }
}
