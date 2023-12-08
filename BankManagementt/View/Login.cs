using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

using BankManagement.Model.Entity;
using BankManagementt.Controller;
using Org.BouncyCastle.Bcpg.OpenPgp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace BankManagementt.View
{
    public partial class Login : Form
    {
        private NasabahController _controller;
        private Nasabah nasabah;
        public List<Nasabah> login;

        public static string email;

        public Login()
        {
            InitializeComponent();
            _controller = new NasabahController();
            login = new List<Nasabah>();
            txtPassword.UseSystemPasswordChar = true;

        }

        private void bunifuFormCaptionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUpd create = new SignUpd();
            create.ShowDialog();

            this.Visible = false;

            this.Dispose();

        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            nasabah = new Nasabah();
            nasabah.email = txtEmail.Text;
            nasabah.password = txtPassword.Text;

            login = _controller.ReadUserByEmail(nasabah.email);

            if (login.Count > 0)
            {
                Nasabah loggedInUser = login[0]; 
                if (loggedInUser.password == nasabah.password)
                {
                    email = txtEmail.Text;
                    Dashboard dashboard = new Dashboard();
                    dashboard.ShowDialog();

                    this.Visible = false;

                    this.Dispose();

                }
                else if(loggedInUser.password != nasabah.password)
                {
                    MessageBox.Show("Kata Sandi Salah", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Text = "";
                    txtPassword.Focus();
                }
                else
                {
                    MessageBox.Show("Akun tidak ditemukan!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtEmail.Text = "";
                    txtPassword.Text = "";
                }
            }
            else
            {

                MessageBox.Show("Data tidak boleh kosong", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
