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
    public class RekeningController
    {
        private RekeningController _repository;

        //Read Rekening By Id Nasabah
        public List<Rekening> ReadRekeningByIdNasabah(int id_nasabah)
        {
            List<Rekening> list = new List<Rekening>();
            using (DbContext context = new DbContext())
            {
                _repository = new RekeningRepository(context);
                list = _repository.ReadRekeningByIdNasabah(id_nasabah);
            }
            return list;
        }
        public int createRekening(Rekening rekening, int id_nasabah)
        {
            int result = 0;

            if (string.IsNullOrEmpty(rekening.id_bank))
            {
                MessageBox.Show("Bank tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
            if (string.IsNullOrEmpty(rekening.saldo))
            {
                MessageBox.Show("Saldo tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
            if (string.IsNullOrEmpty(rekening.status))
            {
                MessageBox.Show("Status tidak boleh kosong!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new RekeningRepository(context);
                result = _repository.Create(rekening, id_nasabah);
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
