using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIzzaShop
{
    public partial class OwnerDashboard : Form
    {
        string Username;
        public OwnerDashboard(string username)
        {
            InitializeComponent();
            Username = username;
        }

        private void OwnerDashboard_Load(object sender, EventArgs e)
        {
            txtUsername.Text = Username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CSOManagement c = new CSOManagement();
            this.Hide();
            c.ShowDialog();
            this.Show(); 

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManagerManagement c = new ManagerManagement();
            this.Hide();
            c.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChefManagement c = new ChefManagement();
            this.Hide();
            c.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PizzaManagement c = new PizzaManagement("Owner");
            this.Hide();
            c.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IngredientManagement c = new IngredientManagement("Owner");
            this.Hide();
            c.ShowDialog();
            this.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Transactions t = new Transactions();
            this.Hide();
            t.ShowDialog();
            this.Show();
        }
    }
}
