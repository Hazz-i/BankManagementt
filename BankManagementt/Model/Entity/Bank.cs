﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement.Model.Entity
{
    public class Bank
    {
        public int id_bank { get; set; }
        public string nama_bank { get; set; }
        public int no_telepon { get; set; }
        public string alamat { get; set; }
    }
}
