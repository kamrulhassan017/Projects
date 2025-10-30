using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PIzzaShop
{
    public partial class OrderManagemnt : Form
    {
        public OrderManagemnt()
        {
            InitializeComponent();
        }

        private void LoadCookListData()
        {
            string query = "SELECT * FROM CookList";  // Load all cook list data without filtering
            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable cookListTable = new DataTable();
                        adapter.Fill(cookListTable);
                        dataGridView1.DataSource = cookListTable;  // Bind data to DataGridView
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading cook list data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChefs()
        {
            string query = "SELECT Username FROM Chef WHERE Deleted = 0";  // Fetch active chefs (Deleted = 0)
            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable chefTable = new DataTable();
                        adapter.Fill(chefTable);
                        comboChef.Items.Clear();  // Clear ComboBox before adding new items

                        foreach (DataRow row in chefTable.Rows)
                        {
                            comboChef.Items.Add(row["Username"].ToString());  // Add chef usernames to ComboBox
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading chefs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Ensure an order is selected and a chef is chosen before assigning
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to assign.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboChef.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a chef.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get selected CookList_ID from DataGridView
            int cookListId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CookList_ID"].Value);

            // Get selected item details
            string itemName = dataGridView1.SelectedRows[0].Cells["Pizza_Name"].Value.ToString();
            string itemType = dataGridView1.SelectedRows[0].Cells["ItemType"].Value.ToString(); // "Pizza" or "Ingredient"
            string chefName = comboChef.SelectedItem.ToString();

            // Query to update the assigned chef for the selected CookList_ID
            string query = "UPDATE CookList SET AssignedTo = @AssignedTo WHERE CookList_ID = @CookList_ID";

            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters for the query
                        cmd.Parameters.AddWithValue("@AssignedTo", chefName);
                        cmd.Parameters.AddWithValue("@CookList_ID", cookListId);  // Only update the selected row by CookList_ID

                        // Execute the update query
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Order successfully assigned to the chef.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the CookList data to show updated information
                LoadCookListData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error assigning order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OrderManagemnt_Load(object sender, EventArgs e)
        {
            // Load CookList data (all orders) and chef usernames
            LoadCookListData();
            LoadChefs();
        }
    }
}
