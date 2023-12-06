using BankManagement.Model.Entity;
using BankManagementt.Controller;
using System;
using System.Collections.Generic;
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
        public int iDnasbah;
        public Dashboard()
        {
            InitializeComponent();
            userList = new List<Nasabah>();
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
        }

        // buat pilih rekening 
        public void comboBoxRekening()
        {
            drpRekening.Items.Clear();
            rekeningList = _rekeningController.ReadRekeningByIdNasabah(iDnasbah);

            foreach (var rekening in rekeningList)
            {
                drpRekening.Items.Add(rekening.nomor_rekening.ToString()+"\t("+ rekening.nama_bank.ToString() + ")");
            }
        }

        // handler
        private void OnCreateEventHandler(Rekening rekening)
        {
            comboBoxRekening();
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
            this.Dispose();
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
                lblSaldo.Text = drpRekening.SelectedItem.ToString();
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
    }
}
