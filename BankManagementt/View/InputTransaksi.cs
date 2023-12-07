using System;
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
        private RekeningController _rekeningController;
        private List<Rekening> rekeningList;
        private List<TransaksiEntity> transaksiList;

        bool isNewData = true;

        //event 
        public event addTransaksi onCreate;

        public InputTransaksi()
        {
            _controller = new TransaksiController();
            _rekeningController = new RekeningController();
            InitializeComponent();


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
            transaksi.nomor_rekening = int.Parse(txtNumber.Text);
            transaksi.asal_bank = txtFromBank.Text;
            transaksi.tgl_transaksi = dte.Value.ToString();
            transaksi.tujuan_bank = txtToBank.Text;
            transaksi.jumlah = int.Parse(txtAmount.Text);
            transaksi.jenis_transaksi = txtCategory.Text;

            int result = 0;
            if (isNewData)
            {
                result = _controller.createTransaksi(transaksi, int.Parse(Dashboard.nasabahId));

                if (result > 0)
                {
                    // Memperbarui saldo dalam listRekening
                    foreach (var item in rekeningList)
                    {
                        if (item.nomor_rekening == int.Parse(txtNumber.Text))
                        {
                            // Menambahkan saldo baru ke saldo yang ada pada rekening yang sesuai
                            item.saldo -= int.Parse(txtAmount.Text);

                            // Memperbarui saldo di database dengan memanggil metode UpdateSaldo dari TransaksiController
                            TransaksiController transaksiController = new TransaksiController();
                            transaksiController.UpdateSaldo(item.saldo, item.nomor_rekening);

                            return; // Keluar dari method setelah proses selesai
                        }
                    }

                    onCreate(transaksi);
                    this.Close();
                }
            }
            else
            {
/*              result = _controller.(transaction);
                transacitonId = transaction.transactionID;
                if (result > 0)
                {
                    OnUpdateData(transaction);
                    this.Close();
                }*/
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
