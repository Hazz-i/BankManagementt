using BankManagementt.Controller;
using System;
using System.Collections.Generic;

using System.Windows.Forms;
using BankManagement.Model.Entity;

namespace BankManagementt.View
{
    public partial class Transaksi : Form
    {
        private RekeningController _rekeningController;
        public List<Rekening> rekeningList;

        public Transaksi()
        {
            _rekeningController = new RekeningController();

            InitializeComponent();
            lblUser.Text = Dashboard.name;
            lblUsername.Text = Dashboard.username; 
            lblAlamat.Text = Dashboard.alamat;
            dte.Value = DateTime.Now;
            comboBoxRekening();
        }

        // buat pilih rekening 
        public void comboBoxRekening()
        {
            drpRekening.Items.Clear();
            rekeningList = _rekeningController.ReadRekeningByIdNasabah(int.Parse(Dashboard.nasabahId));

            foreach (var rekening in rekeningList)
            {
                drpRekening.Items.Add(rekening.nomor_rekening.ToString() + "\t(" + rekening.nama_bank.ToString() + ")");
            }
        }

        private void InisialisasiOrder()
        {
            lvwTransaction.View = System.Windows.Forms.View.Details;
            lvwTransaction.FullRowSelect = true;
            lvwTransaction.GridLines = true;

            lvwTransaction.Columns.Add("No", 50, HorizontalAlignment.Left);
            lvwTransaction.Columns.Add("ID Transaction", 90, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("Number", 150, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("Amount", 70, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("Date", 150, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("Trasaction Category", 150, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("Bank", 150, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("To Bank", 150, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("Name", 150, HorizontalAlignment.Center);
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
            this.Visible = false;
            this.Close();
            dashboard.ShowDialog();
        }

        private void bunifuIconButton2_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            this.Visible = false;
            this.Close();
            profile.ShowDialog();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
           
        }
    }
}
