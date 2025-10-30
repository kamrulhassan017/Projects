using System;

using System.Data;

using System.Data.SqlClient;

using System.Windows.Forms;

namespace PIzzaShop

{

    public partial class CSOManagement : Form

    {

        public CSOManagement()

        {

            InitializeComponent();

        }

        private void CSOManagement_Load(object sender, EventArgs e)

        {

            // Load all CSO records from the CSO table, including deleted ones

            LoadCSOData();

        }

        private void LoadCSOData(string searchTerm = "")

        {

            // SQL query to retrieve all CSOs with dynamic status based on the Deleted column

            string query = "SELECT CSO_ID, Name, Username, Salary, CASE WHEN Deleted = 1 THEN 'Not in service' ELSE 'Active' END AS Status FROM CSO";

            // If a search term is provided, filter by CSO name

            if (!string.IsNullOrEmpty(searchTerm))

            {

                query += " WHERE Name LIKE @SearchTerm"; // Filter by CSO name

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

                        DataTable csoTable = new DataTable();

                        adapter.Fill(csoTable);

                        dataGridView1.DataSource = csoTable; // Bind the fetched data to DataGridView

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

            // Open AddCSO form to add a new CSO

            AddCSO addCSOForm = new AddCSO();

            addCSOForm.ShowDialog();

            // After closing AddCSO form, reload the CSO data

            LoadCSOData();

        }

        private void btnUpdate_Click(object sender, EventArgs e)

        {

            // Update and save changes from DataGridView

            try

            {

                foreach (DataGridViewRow row in dataGridView1.Rows)

                {

                    if (row.IsNewRow) continue;

                    string csoId = row.Cells["CSO_ID"].Value.ToString();

                    string name = row.Cells["Name"].Value.ToString();

                    string username = row.Cells["Username"].Value.ToString();

                    decimal salary = Convert.ToDecimal(row.Cells["Salary"].Value);

                    string query = "UPDATE CSO SET Name = @Name, Username = @Username, Salary = @Salary WHERE CSO_ID = @CSO_ID";

                    using (SqlConnection conn = DBConn.GetConnection())

                    {

                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand(query, conn))

                        {

                            cmd.Parameters.AddWithValue("@CSO_ID", csoId);

                            cmd.Parameters.AddWithValue("@Name", name);

                            cmd.Parameters.AddWithValue("@Username", username);

                            cmd.Parameters.AddWithValue("@Salary", salary);

                            cmd.ExecuteNonQuery();

                        }

                    }

                }

                MessageBox.Show("CSO records updated successfully.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch (Exception ex)

            {

                MessageBox.Show("Error: " + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnDelete_Click(object sender, EventArgs e)

        {

            // Get the selected CSO row

            if (dataGridView1.SelectedRows.Count == 0)

            {

                MessageBox.Show("Please select a CSO to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;

            }

            int csoId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CSO_ID"].Value);

            DialogResult result = MessageBox.Show("Are you sure you want to delete this CSO?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)

            {

                // Mark CSO as deleted by setting Deleted = 1 (logical delete) and updating status to "Not in service"

                try

                {

                    string query = "UPDATE CSO SET Deleted = 1 WHERE CSO_ID = @CSO_ID";

                    using (SqlConnection conn = DBConn.GetConnection())

                    {

                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand(query, conn))

                        {

                            cmd.Parameters.AddWithValue("@CSO_ID", csoId);

                            cmd.ExecuteNonQuery();

                        }

                    }

                    // Reload CSO data after deletion

                    LoadCSOData();

                    MessageBox.Show("CSO deleted successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                catch (Exception ex)

                {

                    MessageBox.Show("Error: " + ex.Message, "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)

        {

            // Search CSOs by name dynamically as the user types

            string searchTerm = txtSearch.Text.Trim();

            LoadCSOData(searchTerm);

        }

        private void btnBack_Click(object sender, EventArgs e)

        {

            // Close the current form and return to the previous form

            this.Close();

        }

    }

}

