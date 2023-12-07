using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using BankManagement.Model.Context;
using BankManagement.Model.Entity;
using BankManagement.Model.Repository;
using BankManagementt.View;

namespace BankManagementt.Controller
{
    public class TransaksiController
    {
        // memanggil repository
        private TransaksiRepository _repository;

        //Read Transaksi
        public List<TransaksiEntity> readByNasabahId(int nomer_rekening)
        {
            List<TransaksiEntity> list = new List<TransaksiEntity>();
            using (DbContext context = new DbContext())
            {
                _repository = new TransaksiRepository(context);
                list = _repository.readByNasabahid(nomer_rekening);
            }

            return list;
        }

        //membuat transaksi
        public int createTransaksi(TransaksiEntity transaksi, int nasabahID)
        {
            int result = 0;

            if (string.IsNullOrEmpty(transaksi.nomor_rekening.ToString()))
            {
                MessageBox.Show("Rekening tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            MessageBox.Show(transaksi.jumlah+" "+transaksi.nomor_rekening + " " + transaksi.asal_bank + " " + transaksi.tgl_transaksi + " " + transaksi.tujuan_bank + " " + transaksi.jenis_transaksi);

            using (DbContext context = new DbContext())
            {
                _repository = new TransaksiRepository(context);
                result = _repository.Create(transaksi, nasabahID);
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil ditambahkan !!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data gagal ditambahkan !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return result;
        }

        //update transaksi
        public int updateTransaksi(TransaksiEntity transaksi, int nomor_rekening)
        {
            int result = 0;

            using (DbContext context = new DbContext())
            {
                _repository = new TransaksiRepository(context);
                result = _repository.Update(transaksi, nomor_rekening);
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil ditambahkan !!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data gagal ditambahkan !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return result;
        }

        // update saldo
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

        // menghapus data nasabah
        public int Delete(TransaksiEntity transaksi)
        {
            int result = 0;

            var konfirmasi = MessageBox.Show("Apalakah anda yakin untuk menghapus data ini ??", "Peringatan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                using (DbContext context = new DbContext())
                {
                    _repository = new TransaksiRepository(context);
                    result = _repository.Delete(transaksi);
                }
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil di hapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data gagal di hapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return result;
        }

        //Controller Ambil Outcome dari Transaksi by Nomor Rekening
        public List<TransaksiEntity> readAllforOutcome(int nomer_rekening)
        {
            List<TransaksiEntity> list = new List<TransaksiEntity>();
            using (DbContext context = new DbContext())
            {
                _repository = new TransaksiRepository(context);
                list = _repository.readAllforOutcome(nomer_rekening);
            }

            return list;
        }
    }
}
