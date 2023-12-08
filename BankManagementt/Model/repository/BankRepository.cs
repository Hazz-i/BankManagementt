using System;
using System.Collections.Generic;

using BankManagement.Model.Context;
using BankManagement.Model.Entity;
using MySql.Data.MySqlClient;

namespace BankManagement.Model.Repository
{
    public class BankRepository
    {
        private MySqlConnection _conn;
        public BankRepository(DbContext context)
        {
            _conn = context.Conn;
        }
        //Query Read Semua Bank Yang Ada
        public List<Bank> ReadAll()
        {
            List<Bank> list = new List<Bank>();
            try
            {
                string sql = @"select * from bank";
                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bank bank = new Bank();
                            bank.id_bank = int.Parse(reader["id_bank"].ToString());
                            bank.nama_bank = reader["nama_bank"].ToString();
                            bank.no_telepon = int.Parse(reader["no_telepon"].ToString());
                            bank.alamat = reader["alamat"].ToString();
                            list.Add(bank);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll Eror : {0}", ex.Message);
            }
            return list;
        }
        // Query membaca id bank
        public List<Bank> ReadAllUsername(string bankName)
        {
            List<Bank> list = new List<Bank>();
            try
            {
                string sql = @"select * from bank where nama_bank = @nama_bank";
                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@nama_bank", bankName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bank bank = new Bank();
                            bank.id_bank = int.Parse(reader["id_bank"].ToString());
                            list.Add(bank);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll Eror : {0}", ex.Message);
            }
            return list;
        }
    }
}
