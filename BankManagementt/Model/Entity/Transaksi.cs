using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementt.Model.Entity
{
    public class Transaksi
    {
        public int Id_transaksi { get; set; }
        public int NomorRekening { get; set; }
        public int NomorRekeningPenerima { get; set; }
        public string TglTransaksi { get; set; }
        public string Jumlah { get; set; }
    }
}
