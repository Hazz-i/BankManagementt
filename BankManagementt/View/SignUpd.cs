using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagementt.View
{
    public partial class SignUpd : Form
    {
        public SignUpd()
        {
            InitializeComponent();
        }

        private void linkSignIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            this.Close();

            login.ShowDialog();

        }
    }
}
