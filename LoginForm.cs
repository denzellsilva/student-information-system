using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student_information_system
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

        }

        public void loginBtn_Click(object sender, EventArgs e)
        {
            string password = pwdTextBox.Text;

            if (password == "admin123")
            {
                Form studentListForm = new StudentListForm();
                this.Hide();
                studentListForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wrong password. Ple    ase try again.");
            }
        }

        private void pwdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.loginBtn_Click(sender, e);
            }
        }
    }
}
