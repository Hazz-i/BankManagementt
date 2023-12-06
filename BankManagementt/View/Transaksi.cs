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
    }
}
