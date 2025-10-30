using PIzzaShop;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PIzzaShop
{
    public partial class AddCSO : Form
    {
        public AddCSO()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Get values from textboxes
            string csoName = txtName.Text;
            string csoUsername = txtUsername.Text;
            string csoPassword = txtPass.Text;
            decimal csoSalary = 0;

            // Try parsing the salary as a decimal
            if (!decimal.TryParse(txtSalary.Text, out csoSalary))
            {
                MessageBox.Show("Please enter a valid salary.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Clear the textboxes after input
            txtName.Clear();
            txtUsername.Clear();
            txtPass.Clear();
            txtSalary.Clear();

            // Insert the CSO data into the database
            if (InsertCSO(csoName, csoUsername, csoPassword, csoSalary))
            {
                MessageBox.Show("CSO added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error adding CSO. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to insert CSO data into the CSO table in the database
        private bool InsertCSO(string name, string username, string password, decimal salary)
        {
            // SQL query to insert data into CSO table
            string query = "INSERT INTO CSO (Name, Username, Password, Salary) VALUES (@Name, @Username, @Password, @Salary)";

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
