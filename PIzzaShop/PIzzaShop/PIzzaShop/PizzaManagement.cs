using PIzzaShop;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PIzzaShop
{
    public partial class PizzaManagement : Form
    {
        private string userRole;

        public PizzaManagement(string role)
        {
            InitializeComponent();
            userRole = role;
        }

        private void PizzaManagement_Load(object sender, EventArgs e)
        {
            // Load all pizza records into DataGridView
            LoadPizzaData();

            // If the user role is "Manager", hide the Delete button
            if (userRole == "Manager")
            {
                btnDelete.Visible = false;
            }
        }

        private void LoadPizzaData()
        {
            string query = "SELECT Pizza_ID, Name, Size, Price FROM Pizza WHERE Deleted = 0";
            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable pizzaTable = new DataTable();
                        adapter.Fill(pizzaTable);
                        dataGridView1.DataSource = pizzaTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Open AddPizza form
            AddPizza addPizzaForm = new AddPizza();
            addPizzaForm.ShowDialog();

            // After closing AddPizza form, reload pizza data
            LoadPizzaData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update and save changes from DataGridView
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string pizzaId = row.Cells["Pizza_ID"].Value.ToString();
                    string name = row.Cells["Name"].Value.ToString();
                    string size = row.Cells["Size"].Value.ToString();
                    decimal price = Convert.ToDecimal(row.Cells["Price"].Value);

                    string query = "UPDATE Pizza SET Name = @Name, Size = @Size, Price = @Price WHERE Pizza_ID = @Pizza_ID";

                    using (SqlConnection conn = DBConn.GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Pizza_ID", pizzaId);
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Size", size);
                            cmd.Parameters.AddWithValue("@Price", price);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Pizza records updated successfully.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the selected pizza row
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a pizza to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int pizzaId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Pizza_ID"].Value);
            DialogResult result = MessageBox.Show("Are you sure you want to delete this pizza?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Mark pizza as deleted by setting Deleted = 1 (logical delete)
                try
                {
                    string query = "UPDATE Pizza SET Deleted = 1 WHERE Pizza_ID = @Pizza_ID";

                    using (SqlConnection conn = DBConn.GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Pizza_ID", pizzaId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Reload pizza data after deletion
                    LoadPizzaData();
                    MessageBox.Show("Pizza deleted successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Close the current form and return to the previous form
            this.Close();
        }

        private void LoadPizzaData(string searchTerm = "")
        {
            // SQL query to select pizzas where the 'Deleted' column is 0 (not deleted)
            string query = "SELECT Pizza_ID, Name, Size, Price FROM Pizza WHERE Deleted = 0";

            // If searchTerm is provided (i.e., user is searching), filter by pizza name
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " AND Name LIKE @SearchTerm"; // Add filter for pizza name
            }

            try
            {
                // Create a connection to the database
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open(); // Open the connection

                    // Create the SqlDataAdapter to run the query
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        // If a search term exists, pass it as a parameter to avoid SQL injection
                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%"); // Add the wildcard for partial matching
                        }

                        // Create a DataTable to hold the query results
                        DataTable pizzaTable = new DataTable();

                        // Fill the DataTable with the data from the query
                        adapter.Fill(pizzaTable);

                        // Set the DataGridView's data source to the filled DataTable
                        dataGridView1.DataSource = pizzaTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Display an error message if something goes wrong
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Get the search term entered by the user
            string searchTerm = txtSearch.Text.Trim();

            // Reload the pizza data with the search term dynamically
            LoadPizzaData(searchTerm);
        }

    }
}
