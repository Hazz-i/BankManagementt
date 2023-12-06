using BankManagement.Model.Entity;
using BankManagementt.Controller;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Windows.Forms;

namespace BankManagementt.View
{
    public partial class AddRekening : Form
    {
        // delegete
        public delegate void HandleAddRekening(Rekening rekening);

        private Rekening rekening;
        private RekeningController _rekeningController;
        private BankController _bankController;
        private List<Bank> bankList;
        bool isNewData = true;

        public int nasabahId = int.Parse(Dashboard.nasabahId);

        // event 
        public event HandleAddRekening createRekening;

        public AddRekening()
        {
            _rekeningController = new RekeningController();
            _bankController = new BankController();
            bankList = new List<Bank>();   
            InitializeComponent();
            comboBoxRekening();
        }

        // buat pilih bank 
        public void comboBoxRekening()
        {
            drpBank.Items.Clear();
            bankList = _bankController.readBank();

            foreach (var bank in bankList)
            {
                drpBank.Items.Add(bank.id_bank);
            }
        }

        public AddRekening(string title, RekeningController controller)
        : this()
        {
            this.Text = title;
            this._rekeningController = controller;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();

            this.Dispose();
        }

        private void btnCreateRekening_Click(object sender, EventArgs e)
        {
            if(isNewData) rekening = new Rekening();

            rekening.id_bank = int.Parse(drpBank.SelectedItem.ToString());
            rekening.status = drpStatus.SelectedItem.ToString();
            rekening.nomor_rekening = int.Parse(txtNomorRekening.Text);

            int result = 0;
            if (isNewData)
            {
                result = _rekeningController.createRekening(rekening, nasabahId);

                if (result > 0)
                {
                    createRekening(rekening);
                    this.Close();
                }
            }
        }
    }
}
