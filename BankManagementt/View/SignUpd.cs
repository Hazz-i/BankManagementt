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
    public partial class SignUpd : Form
    {
        private NasabahController _controller;
        private Nasabah nasabah;


        public SignUpd()
        {
            InitializeComponent();
            _controller = new NasabahController();
        }

        private void linkSignIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            this.Dispose();

            login.ShowDialog();

        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            nasabah = new Nasabah();

            int result = 0; 

            if(txtPassword.Text == txtConPass.Text)
            {
                nasabah.nama_nasabah = txtName.Text;
                nasabah.username = txtUsername.Text;
                nasabah.email = txtEmail.Text;
                nasabah.alamat = txtAddress.Text;
                if (int.TryParse(txtNumber.Text, out int phoneNumber))
                {
                    nasabah.no_telepon = phoneNumber;
                }
                else { 
                }
                nasabah.password = txtPassword.Text;

                bool valid = _controller.DaftarValidasi(txtEmail.Text);

                if(valid == false)
                {
                    result = _controller.createNasbah(nasabah);

                    if (result > 0)
                    {
                        Login login = new Login();
                        login.ShowDialog();

                        this.Visible = false;
                        this.Dispose();
                    }
                }
                else
                {

                    MessageBox.Show("Email suda ada yang menggunakan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Text = " ";
                    txtEmail.Focus();
                }

            }
            else
            {
                MessageBox.Show("Konfirmasi password tidak sama!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConPass.Text = "";
                txtConPass.Focus();
            }

        }

        private void bunifuFormCaptionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SignUpd_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void linkSignIn_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            this.Visible= false;
            this.Close();
            login.ShowDialog();
        }
    }
}
