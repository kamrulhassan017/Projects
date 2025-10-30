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
    public partial class ManagerDashboard : Form
    {
        string username;
        public ManagerDashboard(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void ManagerDashboard_Load(object sender, EventArgs e)
        {
            txtUsername.Text = username;    
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PizzaManagement c = new PizzaManagement("Manager");
            this.Hide();
            c.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IngredientManagement c = new IngredientManagement("Manager");
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

        private void btnOrder_Click(object sender, EventArgs e)
        {
            OrderManagemnt o = new OrderManagemnt();
            this.Hide();
            o.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Transactions t = new Transactions();
            this.Hide();
            t.ShowDialog();
            this.Show();
        }
    }
}
