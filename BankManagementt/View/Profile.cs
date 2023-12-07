using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BankManagement.Model.Entity;
using BankManagementt.Controller;

namespace BankManagementt.View
{
    public partial class Profile : Form
    {
        private NasabahController _controller;
        List<Nasabah> nasabahList;

        public Profile()
        {
            nasabahList = new List<Nasabah>();
            _controller = new NasabahController();

            InitializeComponent();
            txtPass.UseSystemPasswordChar = true;
            nasabahList = _controller.ReadUserByEmail(Login.email);

            foreach(var nasabah in nasabahList)
            {
                lblUsername.Text = nasabah.username;
                txtAddress.Text = nasabah.alamat;
                txtName.Text = nasabah.nama_nasabah;
                txtEmail.Text = nasabah.email;
                txtPhoneNumber.Text = nasabah.no_telepon.ToString();
                txtPass.Text = nasabah.password;
                txtNasabahID.Text = nasabah.id_nasabah.ToString();
            }

            txtNasabahID.ReadOnly = true;
            txtName.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtAddress.ReadOnly = true;
            txtPhoneNumber.ReadOnly = true;
            txtPass.ReadOnly = true;
        }

        private void bunifuFormCaptionButton1_Click_1(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();

            this.Visible = false;
            this.Dispose();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            int result = 0;

            result = _controller.Delete(int.Parse(Dashboard.nasabahId));

            if (result > 0)
            {
                Login login = new Login();
                this.Visible = false;
                this.Close();
                login.ShowDialog();

            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin Logout??", "Peringatan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(konfirmasi == DialogResult.Yes)
            {
                Login login = new Login();
                this.Visible = false;
                this.Close();
                login.ShowDialog();
            }

        }
    }
}
