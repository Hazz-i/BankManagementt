using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //Query Menambahkan Bank
        public int Create(Bank bank)
        {
            int result = 0;
            string sql = @"insert into bank (nama_bank, no_telepon, alamat) values (@nama_bank, @no_telepon, @alamat)";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@nama_bank", bank.nama_bank);
                cmd.Parameters.AddWithValue("@no_telepon", bank.no_telepon);
                cmd.Parameters.AddWithValue("@alamat", bank.alamat);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }
            return result;
        }
        // Query Update Bank
        public int Update(Bank bank, int id_bank)
        {
            int result = 0;
            string sql = @"update bank set nama_bank = @nama_bank, no_telepon = @no_telepon, alamat = @alamat where id_bank = @id_bank";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@nama_bank", bank.nama_bank);
                cmd.Parameters.AddWithValue("@no_telepon", bank.no_telepon);
                cmd.Parameters.AddWithValue("@alamat", bank.alamat);
                cmd.Parameters.AddWithValue("@id_bank", id_bank);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Update error: {0}", ex.Message);
                }
            }
            return result;
        }
        // Query Delete Bank
        public int Delete(Bank bank)
        {
            int result = 0;
            string sql = @"delete from bank where id_bank = @id_bank";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id_bank", bank.id_bank);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Delete error: {0}", ex.Message);
                }
            }
            return result;
        }
    }
}
