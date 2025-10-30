using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PIzzaShop
{
    public partial class ManagerManagement : Form
    {
        public ManagerManagement()
        {
            InitializeComponent();
        }

        private void ManagerManagement_Load(object sender, EventArgs e)
        {
            // Load all managers (including deleted ones) from the Manager table
            LoadManagerData();
        }

        private void LoadManagerData(string searchTerm = "")
        {
            // Update the query to include both active and inactive managers
            string query = "SELECT Manager_ID, Name, Username, Salary, CASE WHEN Deleted = 1 THEN 'Not in service' ELSE 'Active' END AS Status FROM Manager";

            // Add search functionality
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " WHERE Name LIKE @SearchTerm"; // Filter by manager name
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

                        DataTable managerTable = new DataTable();
                        adapter.Fill(managerTable);
                        dataGridView1.DataSource = managerTable;  // Bind data to DataGridView
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
            // Open AddManager form
            AddManager addManagerForm = new AddManager();
            addManagerForm.ShowDialog();

            // After closing AddManager form, reload manager data
            LoadManagerData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update and save changes from DataGridView
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string managerId = row.Cells["Manager_ID"].Value.ToString();
                    string name = row.Cells["Name"].Value.ToString();
                    string username = row.Cells["Username"].Value.ToString();
                    decimal salary = Convert.ToDecimal(row.Cells["Salary"].Value);

                    string query = "UPDATE Manager SET Name = @Name, Username = @Username, Salary = @Salary WHERE Manager_ID = @Manager_ID";

                    using (SqlConnection conn = DBConn.GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Manager_ID", managerId);
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.Parameters.AddWithValue("@Salary", salary);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Manager records updated successfully.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the selected manager row
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a manager to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int managerId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Manager_ID"].Value);
            DialogResult result = MessageBox.Show("Are you sure you want to delete this manager?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Mark manager as deleted by setting Deleted = 1 (logical delete) and updating status to "Not in service"
                try
                {
                    string query = "UPDATE Manager SET Deleted = 1 WHERE Manager_ID = @Manager_ID";

                    using (SqlConnection conn = DBConn.GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Manager_ID", managerId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Reload manager data after deletion
                    LoadManagerData();
                    MessageBox.Show("Manager deleted successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Search managers by name dynamically
            string searchTerm = txtSearch.Text.Trim();
            LoadManagerData(searchTerm);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}