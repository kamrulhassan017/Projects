using PIzzaShop;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PIzzaShop
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txtBoxPassword.UseSystemPasswordChar = true;
            btnShowHide.Text = "Show";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Go back to HomePage form
            HomePage h = new HomePage();
            h.Show();
            this.Hide();
        }

        private void btnShowHide_Click(object sender, EventArgs e)
        {
            // Toggle the password visibility
            if (txtBoxPassword.UseSystemPasswordChar)
            {
                txtBoxPassword.UseSystemPasswordChar = false; // Show password
                btnShowHide.Text = "Hide";
            }
            else
            {
                txtBoxPassword.UseSystemPasswordChar = true; // Hide password
                btnShowHide.Text = "Show";
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Check if the username is provided
            string username = txtUsername.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check the selected role and open the corresponding dashboard form
            if (radioCSO.Checked)
            {
                // Validate CSO login and open CSO dashboard
                if (ValidateLogin(username, "CSO"))
                {
                    CSODashboard csoForm = new CSODashboard(username); // Pass username to CSO dashboard
                    csoForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login credentials for CSO.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (radioManager.Checked)
            {
                // Validate Manager login and open Manager dashboard
                if (ValidateLogin(username, "Manager"))
                {
                    ManagerDashboard managerForm = new ManagerDashboard(username); // Pass username to Manager dashboard
                    managerForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login credentials for Manager.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (radioOwner.Checked)
            {
                // Validate Owner login and open Owner dashboard
                if (ValidateLogin(username, "Owner"))
                {
                    OwnerDashboard ownerForm = new OwnerDashboard(username); // Pass username to Owner dashboard
                    ownerForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login credentials for Owner.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (radioChef.Checked)
            {
                // Validate Chef login and open Chef dashboard
                if (ValidateLogin(username, "Chef"))
                {
                    ChefDashboard chefForm = new ChefDashboard(username); // Pass username to Chef dashboard
                    chefForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login credentials for Chef.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a role.", "Role Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool ValidateLogin(string username, string role)
        {
            // Declare query for different roles
            string query = "";

            // Select the correct table and query based on the role
            switch (role)
            {
                case "CSO":
                    query = "SELECT * FROM CSO WHERE Username = @Username AND (Deleted = 0 OR Deleted IS NULL)";
                    break;
                case "Manager":
                    query = "SELECT * FROM Manager WHERE Username = @Username AND (Deleted = 0 OR Deleted IS NULL)";
                    break;
                case "Owner":
                    query = "SELECT * FROM Owner WHERE Username = @Username ";
                    break;
                case "Chef":
                    query = "SELECT * FROM Chef WHERE Username = @Username AND (Deleted = 0 OR Deleted IS NULL)";
                    break;
                default:
                    return false; // Invalid role
            }

            try
            {
                using (SqlConnection conn = DBConn.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true; // Credentials are valid
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false; // Credentials are invalid or user is deleted
        }


         
    }
}
