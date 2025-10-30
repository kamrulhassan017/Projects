using PIzzaShop;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PIzzaShop
{
    public partial class AddChef : Form
    {
        public AddChef()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Get values from textboxes
            string chefName = txtName.Text;
            string chefUsername = txtUsername.Text;
            string chefPassword = txtPass.Text;
            decimal chefSalary = 0;

            // Try parsing the salary as a decimal
            if (!decimal.TryParse(txtSalary.Text, out chefSalary))
            {
                MessageBox.Show("Please enter a valid salary.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtName.Clear();
            txtPass.Clear();
            txtSalary.Clear();
            txtUsername.Clear();

            if (InsertChef(chefName, chefUsername, chefPassword, chefSalary))
            {
                MessageBox.Show("Chef added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error adding chef. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool InsertChef(string name, string username, string password, decimal salary)
        {
            string query = "INSERT INTO Chef (Name, Username, Password, Salary) VALUES (@Name, @Username, @Password, @Salary)";

            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Salary", salary);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
