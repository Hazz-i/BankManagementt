using System;
using System.Collections.Generic;

using System.Windows.Forms;
using BankManagement.Model.Context;
using BankManagement.Model.Entity;
using BankManagement.Model.Repository;

namespace BankManagementt.Controller
{
    public class BankController
    {
        private BankRepository _repository;

        // membaca nasabah
        public List<Bank> readBank()
        {
            List<Bank> list = new List<Bank>();

            using (DbContext context = new DbContext())
            {
                _repository = new BankRepository(context);
                list = _repository.ReadAll();
            }

            return list;
        }

        // membuat nasbah 
        public int createNasbah(Bank bank)
        {
            int result = 0;

            if (string.IsNullOrEmpty(bank.nama_bank))
            {
                MessageBox.Show("Nama tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
            if (string.IsNullOrEmpty(bank.no_telepon.ToString()))
            {
                MessageBox.Show("Nomor Telefon tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
            if (string.IsNullOrEmpty(bank.alamat))
            {
                MessageBox.Show("Alamat tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new BankRepository(context);
                result = _repository.Create(bank);
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil ditambahkan !!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data gagal ditambahkan !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return result;
        }

        // mengupdate nasabah
        public int Update(Bank bank, int idBank)
        {
            int result = 0;

            if (string.IsNullOrEmpty(bank.nama_bank))
            {
                MessageBox.Show("Nama tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
            if (string.IsNullOrEmpty(bank.no_telepon.ToString()))
            {
                MessageBox.Show("Nomor Telefon tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
            if (string.IsNullOrEmpty(bank.alamat))
            {
                MessageBox.Show("Alamat tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                _repository = new BankRepository(context);
                result = _repository.Update(bank, idBank);
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil diupdate !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data gagal diupdate !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return result;
        }

        // menghapus data nasabah
        public int Delete(Bank bank)
        {
            int result = 0;

            var konfirmasi = MessageBox.Show("Apalakah anda yakin untuk menghapus data ini ??", "Peringatan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                using (DbContext context = new DbContext())
                {
                    _repository = new BankRepository(context);
                    result = _repository.Delete(bank);
                }
            }

            return result;
        }
    }
}
