﻿using System;
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
        public List<TransaksiEntity> readAll()
        {
            List<TransaksiEntity> list = new List<TransaksiEntity>();
            using (DbContext context = new DbContext())
            {
                _repository = new TransaksiRepository(context);
                list = _repository.ReadAll();
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
    }
}
