using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement.Model.Entity
{
    public class Rekening
    {
        public int nomor_rekening { get; set; }
        public int id_nasabah { get; set; }
        public int id_bank { get; set; }
        public string saldo { get; set; }
        public string status { get; set; }

        //Simpan sementara untuk Join
        public string nama_nasabah { get; set; }
        public string nama_bank { get;set; }
        public string alamat_bank { get; set; }
    }
}
