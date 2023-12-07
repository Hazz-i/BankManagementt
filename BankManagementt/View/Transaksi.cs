﻿using BankManagementt.Controller;
using System;
using System.Collections.Generic;

using System.Windows.Forms;
using BankManagement.Model.Entity;

namespace BankManagementt.View
{
    public partial class Transaksi : Form
    {
        private RekeningController _rekeningController;
        private TransaksiController _transaksiController;
        public List<Rekening> rekeningList;
        public List<TransaksiEntity> transaksiList;

        public Transaksi()
        {
            _rekeningController = new RekeningController();
            _transaksiController = new TransaksiController();

            InitializeComponent();
            lblUser.Text = Dashboard.name;
            lblUsername.Text = Dashboard.username; 
            lblAlamat.Text = Dashboard.alamat;
            lblSaldo.Text = Dashboard.balance.ToString();
            dte.Value = DateTime.Now;
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
            transaksiList = _transaksiController.readAll();

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

        // handler
        private void OnCreateEventHandler(TransaksiEntity transaksi)
        {
            LoadDataTransaksi();
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
    }
}
