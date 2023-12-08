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

        // membaca Id Bank By bank name
        public List<Bank> readIdBank(string namaBank)
        {
            List<Bank> list = new List<Bank>();
            using (DbContext context = new DbContext())
            {
                _repository = new BankRepository(context);
                list = _repository.ReadAllUsername(namaBank);
            }

            return list;
        }

    }
}
