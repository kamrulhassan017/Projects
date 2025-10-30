using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PIzzaShop
{
    public partial class AddPizza : Form
    {
        public AddPizza()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string pizzaName = txtName.Text;
            string pizzaSize = txtSize.Text;
            decimal pizzaPrice = 0;

            if (!decimal.TryParse(txtPrice.Text, out pizzaPrice))
            {
                MessageBox.Show("Please enter a valid price.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtName.Clear();
            txtPrice.Clear();
            txtSize.Clear();

            if (InsertPizza(pizzaName, pizzaSize, pizzaPrice))
            {
                MessageBox.Show("Pizza added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error adding pizza. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool InsertPizza(string name, string size, decimal price)
        {
            string query = "INSERT INTO Pizza (Name, Size, Price) VALUES (@Name, @Size, @Price)";

            try
            {
                using (SqlConnection conn = DBConn.GetConnection()) 
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Size", size);
                        cmd.Parameters.AddWithValue("@Price", price);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
