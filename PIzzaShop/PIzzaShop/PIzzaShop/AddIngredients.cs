using PIzzaShop;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PIzzaShop
{
    public partial class AddIngredients : Form
    {
        public AddIngredients()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Get values from textboxes
            string ingredientName = txtName.Text;
            decimal ingredientPrice = 0;

            // Try parsing the price as a decimal
            if (!decimal.TryParse(txtPrice.Text, out ingredientPrice))
            {
                MessageBox.Show("Please enter a valid price.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Clear the textboxes after input
            txtName.Clear();
            txtPrice.Clear();

            // Insert the ingredient data into the database
            if (InsertIngredient(ingredientName, ingredientPrice))
            {
                MessageBox.Show("Ingredient added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error adding ingredient. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to insert ingredient data into the Ingredients table in the database
        private bool InsertIngredient(string name, decimal price)
        {
            // SQL query to insert data into Ingredients table
            string query = "INSERT INTO Ingredients (Name, Price) VALUES (@Name, @Price)";

            try
            {
                using (SqlConnection conn = DBConn.GetConnection()) // Use DBConn class to get the connection
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Adding parameters to avoid SQL Injection
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Price", price);

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
