using PIzzaShop;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PIzzaShop
{
    public partial class ChefDashboard : Form
    {
        string username;

        public ChefDashboard(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void ChefDashboard_Load(object sender, EventArgs e)
        {
            txtUsername.Text = username;
            // Load all orders from the CookList where assigned chef is the current user (username)
            LoadCookListData();
        }

        private void LoadCookListData(string searchTerm = "")
        {
            // Update query according to the updated table structure
            string query = "SELECT * FROM CookList WHERE AssignedTo = @AssignedTo";

            // Add search filter if the search term is provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " AND (Pizza_Name LIKE @SearchTerm OR Ingredient_Name LIKE @SearchTerm)";
            }

            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@AssignedTo", username);
                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                        }

                        DataTable cookListTable = new DataTable();
                        adapter.Fill(cookListTable);
                        dataGridView1.DataSource = cookListTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading cook list data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Search cooklist as the user types
            string searchTerm = txtSearch.Text.Trim();
            LoadCookListData(searchTerm);
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to complete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected CookList ID
            int cookListId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CookList_ID"].Value);

            // Update CookList to mark this order as completed
            string query = "DELETE FROM CookList WHERE CookList_ID = @CookList_ID";

            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CookList_ID", cookListId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Order marked as completed and removed from the cook list.", "Order Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload the cook list after marking the order as completed
                LoadCookListData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error completing the order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Navigate back to Login page
            Login l = new Login();
            l.Show();
            this.Hide();
        }
    }
}
