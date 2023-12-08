﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BankManagement.Model.Entity;
using BankManagementt.Controller;

namespace BankManagementt.View
{
    public partial class AddSaldo : Form
    {
        // delegete
        public delegate void HandleAddSaldo(Rekening rekening);

        private Rekening rekening;
        public List<Rekening> listRekening;  
        
        private RekeningController _controller;
        bool isNewData = true;
        private int idBank;
        private string status;
        private int nasabahId = int.Parse(Dashboard.nasabahId);

        // event 
        public event HandleAddSaldo insertSaldo;

        public AddSaldo()
        {
            rekening = new Rekening();
            listRekening = new List<Rekening>();
            _controller = new RekeningController();
            InitializeComponent();

            listRekening = _controller.readRekeningComboBox(Dashboard.nomorBank);
            txtBank.Enabled = false;
            txtRekening.Enabled = false;

            foreach (var item in listRekening)
            {
                txtBank.Text = item.nama_bank.ToString();
                txtRekening.Text = item.nomor_rekening.ToString();
                idBank = int.Parse(item.id_bank.ToString());
                status = item.status.ToString();
            }
        }

        public AddSaldo(string title, RekeningController controller)
        : this()
        {
            this.Text = title;
            this._controller = controller;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            this.Visible = false;
            this.Close();
            dashboard.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isNewData) rekening = new Rekening();

            rekening.id_nasabah = nasabahId;
            rekening.id_bank = idBank;
            rekening.status = status;
            rekening.saldo = int.Parse(txtSaldo.Text);

            // Memperbarui saldo dalam listRekening
            foreach (var item in listRekening)
            {
                if (item.nomor_rekening == int.Parse(txtRekening.Text))
                {
                    // Menambahkan saldo baru ke saldo yang ada pada rekening yang sesuai
                    item.saldo += int.Parse(txtSaldo.Text);

                    // Memperbarui saldo di database dengan memanggil metode UpdateSaldo dari TransaksiController
                    TransaksiController transaksiController = new TransaksiController();
                    transaksiController.UpdateSaldo(item.saldo, item.nomor_rekening);

                    // Memanggil event insertSaldo untuk memperbarui tampilan saldo di Dashboard
                    insertSaldo(rekening);
                    this.Close();
                    return; // Keluar dari method setelah proses selesai
                }
            }

            int result = 0;
            if (isNewData)
            {
                result = _controller.AddSaldo(int.Parse(txtSaldo.Text), int.Parse(txtRekening.Text), Dashboard.balance, Dashboard.namaBank);

                if (result > 0)
                {
                    insertSaldo(rekening);
                    this.Close();
                }
            }
        }
    }
}
