using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using BankManagement.Model.Context;
using BankManagement.Model.Entity;
using BankManagement.Model.Repository

namespace BankManagementt.Controller
{
    public class NasabahController
    {
        private NasabahRepository _repository;

        // membuat nasbah 
        pucblic int createNasbah(Nasabah nasabah)
        {
            int result = 0;

            if (string.IsNullOrEmpty(nasabah.nama_nasabah))
            {
                MessageBox.Show("Nama tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
            if (string.IsNullOrEmpty(nasabah.alamat))
            {
                MessageBox.Show("Alamat tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
            if (string.IsNullOrEmpty(nasabah.no_telepon))
            {
                MessageBox.Show("Nomor Telefon tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new NasabahRepository(context);
                result = _repository.Create(nasabah);
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
        public int Update(Nasabah nasabah)
        {
            int result = 0;

            if (string.IsNullOrEmpty(nasabah.nama_nasabah))
            {
                MessageBox.Show("Nama tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
            if (string.IsNullOrEmpty(nasabah.alamat))
            {
                MessageBox.Show("Alamat tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
            if (string.IsNullOrEmpty(nasabah.no_telepon))
            {
                MessageBox.Show("Nomor Telefon tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                _repository = new NasabahRepository(context);
                result = _repository.Update(nasabah, id_nasabah);
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
        public int Delete(int nasabahId)
        {
            int result = 0;

            var konfirmasi = MessageBox.Show("Apalakah anda yakin untuk menghapus data ini ??", "Peringatan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                using (DbContext context = new DbContext())
                {
                    _repository = new Nasabah(context);
                    result = _repository.Delete(nasabahId);
                }
            }

            return result;
        }

    }
}
