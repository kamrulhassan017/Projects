using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PIzzaShop
{
    public partial class CSODashboard : Form
    {
        string username;
        decimal totalAmount = 0;
        DataTable ordersTable;

        public CSODashboard(string username)
        {
            InitializeComponent();
            this.username = username;
            ordersTable = new DataTable();  // Initialize the DataTable
            ordersTable.Columns.Add("Item");       // Column for Pizza/Ingredient Name
            ordersTable.Columns.Add("Quantity", typeof(int));  // Column for Quantity
            ordersTable.Columns.Add("Price", typeof(decimal)); // Column for Price
        }

        private void CSODashboard_Load(object sender, EventArgs e)
        {
            // Load orders into the grid view
            LoadOrders();
            dataGridView1.ClearSelection(); // Clear any previous selection
            txtTotal.Text = "0.00"; // Initialize total amount textbox

            txtUsername.Text = username;  // Display the username

            // Load ingredients, sizes, and pizzas into combo boxes
            LoadIngredients();
            LoadSizes();
            LoadPizzas();
        }

        private void LoadOrders()
        {
            // Populate the DataGridView with the DataTable (this will bind the DataGridView)
            dataGridView1.DataSource = ordersTable;
        }

        private void LoadIngredients()
        {
            string query = "SELECT * FROM Ingredients";
            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable ingredientTable = new DataTable();
                        adapter.Fill(ingredientTable);
                        comboIngredients.Items.Clear();

                        foreach (DataRow row in ingredientTable.Rows)
                        {
                            comboIngredients.Items.Add($"{row["Name"]} - {row["Price"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ingredients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSizes()
        {
            string query = "SELECT DISTINCT Size FROM Pizza";
            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable sizeTable = new DataTable();
                        adapter.Fill(sizeTable);
                        comboSize.Items.Clear();

                        foreach (DataRow row in sizeTable.Rows)
                        {
                            comboSize.Items.Add(row["Size"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sizes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPizzas()
        {
            string query = "SELECT * FROM Pizza";
            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable pizzaTable = new DataTable();
                        adapter.Fill(pizzaTable);
                        comboPIzza.Items.Clear();

                        foreach (DataRow row in pizzaTable.Rows)
                        {
                            comboPIzza.Items.Add($"{row["Name"]} - {row["Price"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading pizzas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPizza_Click(object sender, EventArgs e)
        {
            if (comboPIzza.SelectedIndex == -1 || comboSize.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a pizza and size.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedPizza = comboPIzza.SelectedItem.ToString();
            string selectedSize = comboSize.SelectedItem.ToString();
            decimal pizzaPrice = GetPriceFromSelection(selectedPizza);

            // Calculate total amount and update the textbox
            totalAmount += pizzaPrice;
            txtTotal.Text = totalAmount.ToString("C");

            // Add pizza to the OrderTable
            AddItemToOrderTable(selectedPizza, 1, pizzaPrice, "Pizza");
        }

        private decimal GetPriceFromSelection(string selectedItem)
        {
            string priceText = selectedItem.Split('-')[1].Trim();
            return Convert.ToDecimal(priceText);
        }

        private void AddItemToOrderTable(string itemName, int quantity, decimal price, string itemType)
        {
            // Prepare values for the new row
            DataRow newRow = ordersTable.NewRow();  // Create a new row
            newRow["Item"] = itemName;
            newRow["Quantity"] = quantity;
            newRow["Price"] = price;

            // Add the new row to the DataTable
            ordersTable.Rows.Add(newRow);  // Add the row to the ordersTable

            // After adding, refresh the DataGridView to display the new row
            dataGridView1.DataSource = null;  // Unbind the existing data
            dataGridView1.DataSource = ordersTable;  // Rebind the DataGridView to the updated DataTable

            // Optionally, you can auto-size columns for a better view
            dataGridView1.AutoResizeColumns();
        }

        private void btnAddIngre_Click(object sender, EventArgs e)
        {
            if (comboIngredients.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an ingredient.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedIngredient = comboIngredients.SelectedItem.ToString();
            decimal ingredientPrice = GetPriceFromSelection(selectedIngredient);

            // Add ingredient to the OrderTable
            AddItemToOrderTable(selectedIngredient, 1, ingredientPrice, "Ingredient");

            // Update total amount after adding the ingredient price
            totalAmount += ingredientPrice;  // Add ingredient price to the total
            txtTotal.Text = totalAmount.ToString("C");  // Update the textbox with the new total
        }


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // Insert into Transaction
            string query = "INSERT INTO [Transaction] (OrderAmount) VALUES (@OrderAmount)";
            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderAmount", totalAmount);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Order confirmed and added to transaction.", "Order Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Send items to Cook List
                SendToCookList();

                // Delete order items after confirming
                DeleteOrderItems(1);  // Use the actual Order_ID if available

                // Clear the DataGridView
                ordersTable.Clear();  // Clear the DataTable that holds the orders data
                dataGridView1.DataSource = null;  // Unbind the data from the DataGridView

                // Reset total amount
                totalAmount = 0;
                txtTotal.Text = totalAmount.ToString("C");

                // Optional: Reset the combo boxes
                comboPIzza.SelectedIndex = -1;
                comboSize.SelectedIndex = -1;
                comboIngredients.SelectedIndex = -1;
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error confirming the order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendToCookList()
        {
            string query = @"
        INSERT INTO CookList (Pizza_Name, Ingredient_Name, Quantity, ItemType) 
        SELECT ItemName, NULL, Quantity, 'Pizza' 
        FROM OrderTable WHERE ItemType = 'Pizza'";

            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

               MessageBox.Show("Order items have been sent to the kitchen.", "Cook List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending order to cook list: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear the DataGridView and the ordersTable
            dataGridView1.DataSource = null;  // Unbind the DataGridView
            ordersTable.Clear();  // Clear the data in ordersTable

            // Reset total amount
            totalAmount = 0;
            txtTotal.Text = totalAmount.ToString("C");

            // Optional: Reset the combo boxes and other fields
            comboPIzza.SelectedIndex = -1;
            comboSize.SelectedIndex = -1;
            comboIngredients.SelectedIndex = -1;

            // Delete the order items from the database (OrderTable)
            DeleteOrderItems(1);  // Use the actual Order_ID if available
            LoadOrders();

            MessageBox.Show("Order has been cleared and deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Method to delete the order items from the OrderTable
        private void DeleteOrderItems(int orderId)
        {
            // SQL query to delete the order items
            string deleteOrderQuery = "DELETE FROM OrderTable WHERE Order_ID = @OrderID";

            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();

                    // Delete order items
                    using (SqlCommand cmd = new SqlCommand(deleteOrderQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error clearing the order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
