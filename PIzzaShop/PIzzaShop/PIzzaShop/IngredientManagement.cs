using PIzzaShop;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PIzzaShop
{
    public partial class IngredientManagement : Form
    {
        private string userRole; // Store the user role (e.g., Manager)

        public IngredientManagement(string role)
        {
            InitializeComponent();
            userRole = role;
        }

        private void IngredientManagement_Load(object sender, EventArgs e)
        {
            // Load all ingredients when the form loads
            LoadIngredientData();

            // If the user role is "Manager", hide the delete button
            if (userRole == "Manager")
            {
                btnDelete.Visible = false;
            }
        }

        private void LoadIngredientData(string searchTerm = "")
        {
            string query = "SELECT Ingredient_ID, Name, Price, CASE WHEN Deleted = 1 THEN 'Not in service' ELSE 'Active' END AS Status FROM Ingredients WHERE Deleted = 0";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " AND Name LIKE @SearchTerm"; // Filter by ingredient name
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

                        DataTable ingredientTable = new DataTable();
                        adapter.Fill(ingredientTable);
                        dataGridView1.DataSource = ingredientTable;
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
            // Open AddIngredient form to add a new ingredient
            AddIngredients addIngredientForm = new AddIngredients();
            addIngredientForm.ShowDialog();

            // After closing AddIngredient form, reload ingredient data
            LoadIngredientData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update and save changes from DataGridView
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string ingredientId = row.Cells["Ingredient_ID"].Value.ToString();
                    string name = row.Cells["Name"].Value.ToString();
                    decimal price = Convert.ToDecimal(row.Cells["Price"].Value);

                    string query = "UPDATE Ingredients SET Name = @Name, Price = @Price WHERE Ingredient_ID = @Ingredient_ID";

                    using (SqlConnection conn = DBConn.GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Ingredient_ID", ingredientId);
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Price", price);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Ingredient records updated successfully.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the selected ingredient row
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an ingredient to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ingredientId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Ingredient_ID"].Value);
            DialogResult result = MessageBox.Show("Are you sure you want to delete this ingredient?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Mark ingredient as deleted by setting Deleted = 1 (logical delete)
                try
                {
                    string query = "UPDATE Ingredients SET Deleted = 1 WHERE Ingredient_ID = @Ingredient_ID";

                    using (SqlConnection conn = DBConn.GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Ingredient_ID", ingredientId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Reload ingredient data after deletion
                    LoadIngredientData();
                    MessageBox.Show("Ingredient deleted successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // Search ingredients by name dynamically as the user types
            string searchTerm = txtSearch.Text.Trim();
            LoadIngredientData(searchTerm);
        }
    }
}
