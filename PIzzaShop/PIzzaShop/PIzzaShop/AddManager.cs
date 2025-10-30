using PIzzaShop;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PIzzaShop
{
    public partial class AddManager : Form
    {
        public AddManager()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Get values from textboxes
            string managerName = txtName.Text;
            string managerUsername = txtUsername.Text;
            string managerPassword = txtPass.Text;
            decimal managerSalary = 0;

            // Try parsing the salary as a decimal
            if (!decimal.TryParse(txtSalary.Text, out managerSalary))
            {
                MessageBox.Show("Please enter a valid salary.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Clear the textboxes after input
            txtName.Clear();
            txtUsername.Clear();
            txtPass.Clear();
            txtSalary.Clear();

            // Insert the manager data into the database
            if (InsertManager(managerName, managerUsername, managerPassword, managerSalary))
            {
                MessageBox.Show("Manager added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error adding manager. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to insert manager data into the Manager table in the database
        private bool InsertManager(string name, string username, string password, decimal salary)
        {
            // SQL query to insert data into Manager table
            string query = "INSERT INTO Manager (Name, Username, Password, Salary) VALUES (@Name, @Username, @Password, @Salary)";

            try
            {
                using (SqlConnection conn = DBConn.GetConnection()) // Use DBConn class to get the connection
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Adding parameters to avoid SQL Injection
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Salary", salary);

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if the query was successful
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
