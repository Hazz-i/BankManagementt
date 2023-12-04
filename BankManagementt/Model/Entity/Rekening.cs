using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementt.Model.Entity
{
    public class Rekening
    {
        public int NomorRekening { get; set; }
        public int Id_nasbah { get; set; }
        public int Id_bank { get; set; }
        public string JenisRekening { get; set; }
        public int saldo { get; set; }
    }
}
