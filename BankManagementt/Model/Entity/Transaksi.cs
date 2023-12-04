using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement.Model.Entity
{
    public class Transaksi
    {
        public int id_transaksi { get; set; }
        public int nomor_rekening { get; set; }
        public int jumlah {  get; set; }
        public string tgl_transaksi { get; set; }
        public string jenis_transaksi { get; set; }
        public string asal_bank { get; set; }
        public string tujuan_bank { get; set; }

        //untuk menyimpan hasil join
        public int id_nasabah { get; set; }
        public string nama_nasabah { get; set; }
        public int id_bank { get; set; }
        public string nama_bank { get; set; }
    }
}
