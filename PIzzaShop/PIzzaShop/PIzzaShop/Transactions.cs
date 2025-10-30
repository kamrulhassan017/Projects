using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PIzzaShop
{
    public partial class Transactions : Form
    {
        public Transactions()
        {
            InitializeComponent();
        }

        private void Transactions_Load(object sender, EventArgs e)
        {
            // Load total sales amount from the Transaction table
            LoadTotalSales();
            // Load transaction records into the DataGridView
            LoadTransactionData();
        }

        private void LoadTotalSales()
        {
            string query = "SELECT SUM(OrderAmount) FROM [Transaction]";

            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();  // Execute the query and get the result

                        if (result != DBNull.Value)
                        {
                            decimal totalSales = Convert.ToDecimal(result);
                            txt1.Text = totalSales.ToString("C");  // Format the result as currency
                        }
                        else
                        {
                            txt1.Text = "0.00";  // If no sales are found, display 0.00
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading total sales: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTransactionData()
        {
            // Query to fetch all records from the Transaction table
            string query = "SELECT * FROM [Transaction]";

            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable transactionTable = new DataTable();
                        adapter.Fill(transactionTable);
                        dataGridView1.DataSource = transactionTable; // Bind data to DataGridView
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading transaction data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
