﻿using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.IdentityModel.Protocols.WSTrust;
using System.Transactions;
using System.Windows.Forms;
using BankManagement.Model.Entity;
using BankManagementt.Controller;

namespace BankManagementt.View
{
    public partial class InputTransaksi : Form
    {
        // delegete
        public delegate void addTransaksi(TransaksiEntity transaksi);

        private TransaksiEntity transaksi;
        private TransaksiController _controller;
        private BankController _bankController;
        private RekeningController _rekeningController;
        private List<Rekening> rekeningList;
        private List<TransaksiEntity> transaksiList;
        private List<Bank> bankList;

        bool isNewData = true;
        public static int amount;

        //event 
        public event addTransaksi onCreate;
        public event addTransaksi onUpdate;

        public InputTransaksi()
        {
            _controller = new TransaksiController();
            _rekeningController = new RekeningController();
            _bankController = new BankController();
            InitializeComponent();
            comboBank();

            txtCategory.ReadOnly = true;
            txtFromBank.ReadOnly = true;
            txtNumber.ReadOnly = true;

            txtCategory.Text = "Transfer";
            dte.Value = DateTime.Now;

            rekeningList = _rekeningController.readRekeningComboBox(Dashboard.nomorBank);
            foreach (var item in rekeningList)
            {
                txtFromBank.Text = item.nama_bank;
                txtNumber.Text = item.nomor_rekening.ToString();
            }

        }

        public void comboBank()
        {
            drpJnisBank.Items.Clear();
            bankList = _bankController.readBank();

            foreach (var bank in bankList)
            {
                drpJnisBank.Items.Add(bank.nama_bank.ToString());
            }
        }

        public InputTransaksi(string title, TransaksiController controller)
        : this()
        {
            this.Text = title;
            this._controller = controller;
        }
        public InputTransaksi(string title, TransaksiEntity obj, TransaksiController controller)
        : this()
        {
            this.Text = title;
            this._controller = controller;
            isNewData = false;
            transaksi = obj;

            lblTransaksi.Text = "Update Data";

/*          transaksiList = _controller.readAll();
            foreach (var trs in transaksiList)
            {
                movieDropDown.Items.Add(trs.MovieName);
                methodDropDown.Items.Add(trs.methode);
                txtNumber.Text = trs.Number.ToString();
                txtAmount.Text = trs.Amount.ToString();
                snackDropDown.Items.Add(trs.Sncak);
            }*/
        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (isNewData) transaksi = new TransaksiEntity();
            string strFormat = "yyyy-MM-dd H:m:s";
            string datenow = DateTime.Now.ToString(strFormat);
            transaksi.nomor_rekening = int.Parse(txtNumber.Text);
            transaksi.asal_bank = txtFromBank.Text;
            transaksi.tgl_transaksi = datenow;
            transaksi.tujuan_bank = drpJnisBank.SelectedItem.ToString();
            transaksi.jenis_transaksi = txtCategory.Text;

            if (Dashboard.balance >= int.Parse(txtAmount.Text))
            {
                transaksi.jumlah = int.Parse(txtAmount.Text);

                int result = 0;
                if (isNewData)
                {
                    result = _controller.createTransaksi(transaksi, int.Parse(Dashboard.nasabahId));

                    if (result > 0)
                    {
                        amount = int.Parse(txtAmount.Text);
                        onCreate(transaksi);
                        this.Close();
                    }
                }
                else
                {
                    result = _controller.updateTransaksi(transaksi, Dashboard.nomorBank);
                    
                    if (result > 0)
                    {
                        onUpdate(transaksi);
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Saldo Anda tidak Mencukupi", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Text = "";
                txtAmount.Focus();
            }


        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Transaksi transaksi = new Transaksi();
            this.Close();
            transaksi.ShowDialog();
        }
    }
}
