using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement.Model.Entity
{
    public class Nasabah
    {
        public int id_nasabah { get; set; }
        public string nama_nasabah { get; set; }
        public string alamat { get; set; }
        public int no_telepon { get; set; }
        public string tgl_lahir { get; set; }
    }
}
