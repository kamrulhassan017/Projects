using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PIzzaShop
{
    public partial class ChefManagement : Form
    {
        public ChefManagement()
        {
            InitializeComponent();
        }

        private void ChefManagement_Load(object sender, EventArgs e)
        {
            // Load all chef records from the Chef table, including deleted ones
            LoadChefData();
        }

        private void LoadChefData(string searchTerm = "")
        {
            // SQL query to retrieve all chefs and dynamically set Status based on Deleted column
            string query = "SELECT Chef_ID, Name, Username, Salary, CASE WHEN Deleted = 1 THEN 'Not in service' ELSE 'Active' END AS Status FROM Chef";

            // Add search functionality if searchTerm is not empty
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " WHERE Name LIKE @SearchTerm"; // Filter by chef name
            }

            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                        }

                        DataTable chefTable = new DataTable();
                        adapter.Fill(chefTable);
                        dataGridView1.DataSource = chefTable; // Bind the fetched data to DataGridView
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
            // Open AddChef form to add a new chef
            AddChef addChefForm = new AddChef();
            addChefForm.ShowDialog();

            // After closing AddChef form, reload the chef data
            LoadChefData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update and save changes from DataGridView
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string chefId = row.Cells["Chef_ID"].Value.ToString();
                    string name = row.Cells["Name"].Value.ToString();
                    string username = row.Cells["Username"].Value.ToString();
                    decimal salary = Convert.ToDecimal(row.Cells["Salary"].Value);

                    string query = "UPDATE Chef SET Name = @Name, Username = @Username, Salary = @Salary WHERE Chef_ID = @Chef_ID";

                    using (SqlConnection conn = DBConn.GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Chef_ID", chefId);
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.Parameters.AddWithValue("@Salary", salary);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Chef records updated successfully.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the selected chef row
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a chef to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int chefId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Chef_ID"].Value);
            DialogResult result = MessageBox.Show("Are you sure you want to delete this chef?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Mark chef as deleted by setting Deleted = 1 (logical delete) and updating status to "Not in service"
                try
                {
                    string query = "UPDATE Chef SET Deleted = 1 WHERE Chef_ID = @Chef_ID";

                    using (SqlConnection conn = DBConn.GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Chef_ID", chefId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Reload chef data after deletion
                    LoadChefData();
                    MessageBox.Show("Chef deleted successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Search chefs by name dynamically as the user types
            string searchTerm = txtSearch.Text.Trim();
            LoadChefData(searchTerm);
        }
    }
}