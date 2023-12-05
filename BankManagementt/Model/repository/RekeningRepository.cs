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
    public class RekeningRepository
    {
        private MySqlConnection _conn;
        public RekeningRepository(DbContext context)
        {
            _conn = context.Conn;
        }
        //Query Read Semua Rekening Yang Ada
        public List<Rekening> ReadAll()
        {
            List<Rekening> list = new List<Rekening>();
            try
            {
                string sql = @"select rekening.nomor_rekening, rekening.id_nasabah, rekening.id_bank, rekening.saldo, rekening.status, nasabah.id_nasabah, nasabah.nama_nasabah, bank.id_bank, bank.nama_bank from rekening join nasabah on rekening.id_nasabah = nasabah.id_nasabah join bank on rekening.id_bank = bank.id_bank";
                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rekening rekening = new Rekening();
                            rekening.nomor_rekening = int.Parse(reader["nomor_rekening"].ToString());
                            rekening.id_nasabah = int.Parse(reader["id_nasabah"].ToString());
                            rekening.nama_nasabah = reader["nama_nasabah"].ToString();
                            rekening.id_bank = int.Parse(reader["id_bank"].ToString());
                            rekening.nama_bank = reader["nama_bank"].ToString();
                            rekening.saldo = reader["saldo"].ToString();
                            rekening.status = reader["status"].ToString();
                            list.Add(rekening);
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
        //Query Menambahkan Rekening
        public int Create(Rekening rekening)
        {
            int result = 0;
            string sql = @"insert into rekening (id_nasabah, id_bank, saldo, status) values (@id_nasabah, @id_bank, @saldo, @status)";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id_nasabah", rekening.id_nasabah);
                cmd.Parameters.AddWithValue("@id_bank", rekening.id_bank);
                cmd.Parameters.AddWithValue("@saldo", rekening.saldo);
                cmd.Parameters.AddWithValue("@status", rekening.status);
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
        // Query Update Rekening
        public int Update(Rekening rekening, int id_rekening)
        {
            int result = 0;
            string sql = @"update rekening set id_nasabah = @id_nasabah, id_bank = @id_bank, saldo = @saldo, status = @status where id_rekening = @id_rekening";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id_nasabah", rekening.id_nasabah);
                cmd.Parameters.AddWithValue("@id_bank", rekening.id_bank);
                cmd.Parameters.AddWithValue("@saldo", rekening.saldo);
                cmd.Parameters.AddWithValue("@status", rekening.status);
                cmd.Parameters.AddWithValue("@id_rekening", id_rekening);
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
        // Query Delete Rekening
        public int Delete(Rekening rekening)
        {
            int result = 0;
            string sql = @"delete from rekening where id_rekening = @id_rekening";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id_rekening", rekening.nomor_rekening);
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
