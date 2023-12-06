using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using BankManagement.Model.Context;
using BankManagement.Model.Entity;
using BankManagement.Model.Repository;

namespace BankManagementt.Controller
{
    public class TransaksiController
    {
        // memanggil repository
        private TransaksiRepository _repository;

        //membuat transaksi
        public int createTransaksi(Transaksi transaksi)
        {
            int result = 0;

            if (string.IsNullOrEmpty(transaksi.nomor_rekening.ToString()))
            {
                MessageBox.Show("Rekening tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new TransaksiRepository(context);
                result = _repository.Create(transaksi);
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil ditambahkan !!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data gagal ditambahkan !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return result;
        }
        public int UpdateSaldo(int saldo_update, int nomor_rekening)
        {
            int result = 0;

            using (DbContext context = new DbContext())
            {
                _repository = new TransaksiRepository(context);
                result = _repository.UpdateSaldo(saldo_update, nomor_rekening);
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil ditambahkan !!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data gagal ditambahkan !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return result;
        }
    }
}
