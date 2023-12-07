using BankManagement.Model.Entity;
using BankManagementt.Controller;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Windows.Forms;

namespace BankManagementt.View
{
    public partial class Dashboard : Form
    { 
        private NasabahController _controller;
        private RekeningController _rekeningController;
        public List<Nasabah> userList;
        public List<Rekening> rekeningList;

        public string email = Login.email;
        public static string nasabahId;
        public static string name;
        public static string username;
        public static string alamat;
        public static string nama;
        public static int balance;
        public static int nomorBank;
        public int iDnasbah;

        public Dashboard()
        {
            InitializeComponent();
            userList = new List<Nasabah>();
            rekeningList = new List<Rekening>();
            _controller = new NasabahController();
            _rekeningController = new RekeningController();

            userList = _controller.ReadUserByEmail(email);

            foreach(var nbh in userList)
            {
                nasabahId = nbh.id_nasabah.ToString();
                iDnasbah = nbh.id_nasabah;
                name = nbh.nama_nasabah;
                username = nbh.username;
                alamat = nbh.alamat;
            }

            lblUser.Text = name;
            bunifuLabel7.Text = username;
            namaUser1.Text = name;
            namaUser2.Text = name;
            lblAalmat.Text = alamat;
            dte.Value = DateTime.Now;

            comboBoxRekening();
            InisialisasiRekening();
            LoadDataTransaksi();
        }

        // buat pilih rekening 
        public void comboBoxRekening()
        {
            drpRekening.Items.Clear();
            rekeningList = _rekeningController.ReadRekeningByIdNasabah(iDnasbah);

            foreach (var rekening in rekeningList)
            {
                drpRekening.Items.Add(rekening.nomor_rekening.ToString());
            }
        }

        private void InisialisasiRekening()
        {
            lvwRekeing.View = System.Windows.Forms.View.Details;
            lvwRekeing.FullRowSelect = true;
            lvwRekeing.GridLines = true;

            lvwRekeing.Columns.Add("No", 50, HorizontalAlignment.Left);
            lvwRekeing.Columns.Add("Number", 150, HorizontalAlignment.Center);
            lvwRekeing.Columns.Add("Bank", 100, HorizontalAlignment.Center);
            lvwRekeing.Columns.Add("Amount", 150, HorizontalAlignment.Center);
            lvwRekeing.Columns.Add("Status", 150, HorizontalAlignment.Center);
        }

        private void LoadDataTransaksi()
        {
            lvwRekeing.Items.Clear();
            rekeningList = _rekeningController.ReadRekeningByIdNasabah(iDnasbah);

            foreach (var trs in rekeningList)
            {
                var noUrut = lvwRekeing.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(trs.nomor_rekening.ToString());
                item.SubItems.Add(trs.nama_bank);
                item.SubItems.Add(trs.saldo.ToString());
                item.SubItems.Add(trs.status);
                lvwRekeing.Items.Add(item);
            }
        }

        // handler
        private void OnCreateEventHandler(Rekening rekening)
        {
            LoadDataTransaksi();
            comboBoxRekening();
        }
        private void OnCreateEventHandlerSaldo(Rekening rekening)
        {
            LoadDataTransaksi();
            lblSaldo.Text = rekening.saldo.ToString();
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // pindah ke trnsaksi
        private void lblTransaction_Click(object sender, EventArgs e)
        {
            Transaksi transaksi = new Transaksi();
            this.Visible = false;
            this.Close();
            transaksi.ShowDialog();
        }

        // tambah saldo
        private void btnAddSaldo_Click(object sender, EventArgs e)
        {
            if (drpRekening.Items.Count == 0)
            {
                MessageBox.Show("Silahkan Buat Rekening Terlebih Dahulu !!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                AddSaldo add = new AddSaldo("Tambah Saldo", _rekeningController);
                add.insertSaldo += OnCreateEventHandlerSaldo;
                add.ShowDialog();

                rekeningList = _rekeningController.readRekeningComboBox(int.Parse(drpRekening.SelectedItem.ToString()));

                foreach (var item in rekeningList)
                {
                    lblSaldo.Text = item.saldo.ToString();
                }

                balance = int.Parse(lblSaldo.Text);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddRekening add = new AddRekening("Tambah Rekening", _rekeningController);
            add.createRekening += OnCreateEventHandler;
            add.ShowDialog();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            this.Visible = false;
            this.Close();
            profile.ShowDialog();
        }

        private void txtSaldo_TextChanged(object sender, EventArgs e)
        {

        }

        private void drpRekening_SelectedIndexChanged(object sender, EventArgs e)
        {
            rekeningList = _rekeningController.readRekeningComboBox(int.Parse(drpRekening.SelectedItem.ToString()));
            nomorBank = int.Parse(drpRekening.SelectedItem.ToString());

            foreach (var item in rekeningList)
            {
                txtNamaRekening.Text = item.nama_bank.ToString();
                lblSaldo.Text = item.saldo.ToString();
            }

            balance = int.Parse(lblSaldo.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvwRekeing.SelectedItems.Count > 0)
            {

                // ambil objek mhs yang mau dihapus dari collection
                Rekening rek = rekeningList[lvwRekeing.SelectedIndices[0]];

                int result = _rekeningController.Delete(rek);


                if (result > 0)
                {
                    LoadDataTransaksi();
                    comboBoxRekening();
                }
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data belum dipilih !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
