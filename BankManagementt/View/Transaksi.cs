using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagementt.View
{
    public partial class Transaksi : Form
    {
        public Transaksi()
        {
            InitializeComponent();
            lblUser.Text = Dashboard.name;
            lblUsername.Text = Dashboard.username; 
            lblAlamat.Text = Dashboard.alamat;
            dte.Value = DateTime.Now;
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuIconButton1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();
            this.Visible = false;
            this.Close();
        }
    }
}
