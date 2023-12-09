using BankManagementt.Controller;
using System;
using System.Collections.Generic;

using System.Windows.Forms;
using BankManagement.Model.Entity;
using System.Transactions;
using System.Runtime.CompilerServices;

namespace BankManagementt.View
{
    public partial class Transaksi : Form
    {
        private RekeningController _rekeningController;
        private TransaksiController _transaksiController;
        public List<Rekening> rekeningList;
        public List<TransaksiEntity> transaksiList;
        public static int balanceAftertDelete;
        public int saldo;
        public int outcome;
        public int income;
        public Transaksi()
        {
            _rekeningController = new RekeningController();
            _transaksiController = new TransaksiController();

            InitializeComponent();
            outcomeRead();
            incomeRead();
            lblUser.Text = Dashboard.name;
            lblUsername.Text = Dashboard.username; 
            lblAlamat.Text = Dashboard.alamat;
            dte.Value = DateTime.Now;

            rekeningList = _rekeningController.readRekeningComboBox(Dashboard.nomorBank);

            foreach (var item in rekeningList)
            {
                lblSaldo.Text = item.saldo.ToString();
                saldo = item.saldo;
            }

            InisialisasiOrder();
            LoadDataTransaksi();
        }

        private void InisialisasiOrder()
        {
            lvwTransaction.View = System.Windows.Forms.View.Details;
            lvwTransaction.FullRowSelect = true;
            lvwTransaction.GridLines = true;

            lvwTransaction.Columns.Add("No", 50, HorizontalAlignment.Left);
            lvwTransaction.Columns.Add("ID Transaction", 90, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("Number", 100, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("Amount", 150, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("Date", 150, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("Trasaction Category", 150, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("Bank", 100, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("To Bank", 100, HorizontalAlignment.Center);
            lvwTransaction.Columns.Add("Name", 150, HorizontalAlignment.Center);
        }

        private void LoadDataTransaksi()
        {
            lvwTransaction.Items.Clear();
            transaksiList = _transaksiController.readByNasabahId(Dashboard.nomorBank);
            foreach (var trs in transaksiList)
            {
                var noUrut = lvwTransaction.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(trs.id_transaksi.ToString());
                item.SubItems.Add(trs.nomor_rekening.ToString());
                item.SubItems.Add(trs.jumlah.ToString());
                item.SubItems.Add(trs.tgl_transaksi);
                item.SubItems.Add(trs.jenis_transaksi);
                item.SubItems.Add(trs.asal_bank);
                item.SubItems.Add(trs.tujuan_bank);
                item.SubItems.Add(trs.nama_nasabah);
                lvwTransaction.Items.Add(item);
            }                                                                                                                        
        }

        // membaca outocme
        void outcomeRead()
        {
            transaksiList = _transaksiController.readAllforOutcome(Dashboard.nomorBank);
            outcome = 0;

            foreach (var trs in transaksiList)
            {
                outcome += trs.jumlah;
            }

            lblOutcome.Text = outcome.ToString(); 

        }
        void incomeRead()
        {
            transaksiList = _transaksiController.readAllforIncome(Dashboard.nomorBank);
            income = 0;

            foreach (var trs in transaksiList)
            {
                income += trs.jumlah;
            }

            lblIncome.Text = income.ToString(); 

        }

        // handler
        private void OnCreateEventHandler(TransaksiEntity transaksi)
        {
            LoadDataTransaksi();
            outcomeRead();

            rekeningList = _rekeningController.readRekeningComboBox(Dashboard.nomorBank);

            foreach (var trs in rekeningList)
            {
                lblSaldo.Text = trs.saldo.ToString();
            }

        }

        private void OnUpdateEventHandler(TransaksiEntity transaksi)
        {
            LoadDataTransaksi();
            outcomeRead();

            rekeningList = _rekeningController.readRekeningComboBox(Dashboard.nomorBank);

            foreach (var trs in rekeningList)
            {
                lblSaldo.Text = trs.saldo.ToString();
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

        private void btnTransfer_Click(object sender, EventArgs e)
        {

            InputTransaksi add = new InputTransaksi("Tambah Saldo", _transaksiController);
            add.onCreate += OnCreateEventHandler;
            add.ShowDialog();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvwTransaction.SelectedItems.Count > 0)
            {
                saldo = 0;
                transaksiList = _transaksiController.readAllforOutcome(Dashboard.nomorBank);
                rekeningList = _rekeningController.readRekeningComboBox(Dashboard.nomorBank);

                foreach(var bank in rekeningList)
                {
                    saldo = bank.saldo;
                }

                foreach (var trs in transaksiList)
                {
                    balanceAftertDelete = saldo + trs.jumlah;
                    _transaksiController.UpdateSaldo(balanceAftertDelete, Dashboard.nomorBank);
                }


                // ambil objek mhs yang mau dihapus dari collection
                TransaksiEntity rek = transaksiList[lvwTransaction.SelectedIndices[0]];

                int result = _transaksiController.Delete(rek);


                if (result > 0)
                {
                    LoadDataTransaksi();
                    outcomeRead();
                    lblSaldo.Text = balanceAftertDelete.ToString();
                }
            }
            else 
            {
                MessageBox.Show("Data belum dipilih !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lblRp_Click(object sender, EventArgs e)
        {

        }

        private void lblSaldo_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvwTransaction.SelectedItems.Count > 0)
            {
                TransaksiEntity trs = transaksiList[lvwTransaction.SelectedIndices[0]];
                InputTransaksi updateData = new InputTransaksi("Update Data Transaksi", trs, _transaksiController);
                updateData.onUpdate += OnUpdateEventHandler;
                updateData.ShowDialog();
            }
            else
            {
                MessageBox.Show("Data belum dipilih", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void bunifuLabel8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel7_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel10_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel5_Click(object sender, EventArgs e)
        {

        }
    }
}
