using System;
using System.Collections.Generic;

using BankManagement.Model.Context;
using BankManagement.Model.Entity;
using MySql.Data.MySqlClient;

namespace BankManagement.Model.Repository
{
    public class TransaksiRepository
    {
        private MySqlConnection _conn;
        public TransaksiRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        //Query Read Semua Transaksi Yang Ada
        public List<TransaksiEntity> readByNasabahid(int nomer_rekening)
        {
            List<TransaksiEntity> list = new List<TransaksiEntity>();
            try
            {
                string sql = @"select transaksi.id_transaksi, transaksi.nomor_rekening, transaksi.jumlah, transaksi.tgl_transaksi, transaksi.jenis_transaksi, transaksi.asal_bank, transaksi.tujuan_bank, rekening.saldo, nasabah.id_nasabah, nasabah.nama_nasabah, bank.id_bank, bank.nama_bank from transaksi join rekening on transaksi.nomor_rekening = rekening.nomor_rekening join nasabah on rekening.id_nasabah = nasabah.id_nasabah join bank on rekening.id_bank = bank.id_bank WHERE rekening.nomor_rekening = @nomer_rekening";
                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@nomer_rekening", nomer_rekening);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TransaksiEntity transaksi = new TransaksiEntity();
                            transaksi.id_transaksi = int.Parse(reader["id_transaksi"].ToString());
                            transaksi.nomor_rekening = int.Parse(reader["nomor_rekening"].ToString());
                            transaksi.jumlah = int.Parse(reader["jumlah"].ToString());
                            transaksi.tgl_transaksi = reader["tgl_transaksi"].ToString();
                            transaksi.jenis_transaksi = reader["jenis_transaksi"].ToString();
                            transaksi.asal_bank = reader["asal_bank"].ToString();
                            transaksi.tujuan_bank = reader["tujuan_bank"].ToString();
                            transaksi.id_nasabah = int.Parse(reader["id_nasabah"].ToString());
                            transaksi.nama_nasabah = reader["nama_nasabah"].ToString();
                            transaksi.id_bank = int.Parse(reader["id_bank"].ToString());
                            transaksi.nama_bank = reader["nama_bank"].ToString();
                            list.Add(transaksi);
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
        //Query Menambahkan Transaksi
        public int Create(TransaksiEntity transaksi)
        {
            int result = 0;
            string sql = "INSERT INTO transaksi (nomor_rekening, jumlah, tgl_transaksi, jenis_transaksi, asal_bank, tujuan_bank) VALUES (@nomor_rekening, @jumlah, @tgl_transaksi, @jenis_transaksi, @asal_bank, @tujuan_bank)";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@nomor_rekening", transaksi.nomor_rekening);
                cmd.Parameters.AddWithValue("@jumlah", transaksi.jumlah);
                cmd.Parameters.AddWithValue("@tgl_transaksi", transaksi.tgl_transaksi);
                cmd.Parameters.AddWithValue("@jenis_transaksi", transaksi.jenis_transaksi);
                cmd.Parameters.AddWithValue("@asal_bank", transaksi.asal_bank);
                cmd.Parameters.AddWithValue("@tujuan_bank", transaksi.tujuan_bank);
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
        // Query Update Saldo Asal
        public int UpdateSaldo(int saldo_update, int nomor_rekening)
        {
            int result = 0;
            string sql = @"update rekening set saldo = @saldo_update where nomor_rekening = @nomor_rekening";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@saldo_update", saldo_update);
                cmd.Parameters.AddWithValue("@nomor_rekening", nomor_rekening);
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
        // Query Update Transaksi
        public int Update(TransaksiEntity transaksi, int id_transaksi)
        {
            int result = 0;
            string sql = @"update transaksi set jumlah = @jumlah, tujuan_bank = @tujuan_bank where id_transaksi = @id_transaksi";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id_transaksi", id_transaksi);
                cmd.Parameters.AddWithValue("@tujuan_bank", transaksi.tujuan_bank);
                cmd.Parameters.AddWithValue("@jumlah", transaksi.jumlah);
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
        // Query Delete Transaksi
        public int Delete(TransaksiEntity transaksi)
        {
            int result = 0;
            string sql = @"delete from transaksi where id_transaksi = @id_transaksi";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id_transaksi", transaksi.id_transaksi);
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
        // Query Ambil Outcome dari Transaksi by Nomor Rekening
        public List<TransaksiEntity> readAllforOutcome(int nomer_rekening)
        {
            List<TransaksiEntity> list = new List<TransaksiEntity>();
            try
            {
                string sql = @"select transaksi.id_transaksi, transaksi.nomor_rekening, transaksi.jumlah, transaksi.tgl_transaksi, transaksi.jenis_transaksi, transaksi.asal_bank, transaksi.tujuan_bank, rekening.saldo, nasabah.id_nasabah, nasabah.nama_nasabah, bank.id_bank, bank.nama_bank from transaksi join rekening on transaksi.nomor_rekening = rekening.nomor_rekening join nasabah on rekening.id_nasabah = nasabah.id_nasabah join bank on rekening.id_bank = bank.id_bank WHERE rekening.nomor_rekening = @nomer_rekening && transaksi.jenis_transaksi = 'Transfer'";
                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@nomer_rekening", nomer_rekening);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TransaksiEntity transaksi = new TransaksiEntity();
                            transaksi.id_transaksi = int.Parse(reader["id_transaksi"].ToString());
                            transaksi.nomor_rekening = int.Parse(reader["nomor_rekening"].ToString());
                            transaksi.jumlah = int.Parse(reader["jumlah"].ToString());
                            transaksi.tgl_transaksi = reader["tgl_transaksi"].ToString();
                            transaksi.jenis_transaksi = reader["jenis_transaksi"].ToString();
                            transaksi.asal_bank = reader["asal_bank"].ToString();
                            transaksi.tujuan_bank = reader["tujuan_bank"].ToString();
                            transaksi.id_nasabah = int.Parse(reader["id_nasabah"].ToString());
                            transaksi.nama_nasabah = reader["nama_nasabah"].ToString();
                            transaksi.id_bank = int.Parse(reader["id_bank"].ToString());
                            transaksi.nama_bank = reader["nama_bank"].ToString();
                            list.Add(transaksi);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("readAllforIncome Eror : {0}", ex.Message);
            }
            return list;
        }
        // Query Ambil Income dari Transaksi by Nomor Rekening
       public List<TransaksiEntity> readAllforIncome(int nomer_rekening)
        {
            List<TransaksiEntity> list = new List<TransaksiEntity>();
            try
            {
                string sql = @"select transaksi.id_transaksi, transaksi.nomor_rekening, transaksi.jumlah, transaksi.tgl_transaksi, transaksi.jenis_transaksi, transaksi.asal_bank, transaksi.tujuan_bank, rekening.saldo, nasabah.id_nasabah, nasabah.nama_nasabah, bank.id_bank, bank.nama_bank from transaksi join rekening on transaksi.nomor_rekening = rekening.nomor_rekening join nasabah on rekening.id_nasabah = nasabah.id_nasabah join bank on rekening.id_bank = bank.id_bank WHERE rekening.nomor_rekening = @nomer_rekening && transaksi.jenis_transaksi = 'Penambahan'";
                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@nomer_rekening", nomer_rekening);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TransaksiEntity transaksi = new TransaksiEntity();
                            transaksi.id_transaksi = int.Parse(reader["id_transaksi"].ToString());
                            transaksi.nomor_rekening = int.Parse(reader["nomor_rekening"].ToString());
                            transaksi.jumlah = int.Parse(reader["jumlah"].ToString());
                            transaksi.tgl_transaksi = reader["tgl_transaksi"].ToString();
                            transaksi.jenis_transaksi = reader["jenis_transaksi"].ToString();
                            transaksi.asal_bank = reader["asal_bank"].ToString();
                            transaksi.tujuan_bank = reader["tujuan_bank"].ToString();
                            transaksi.id_nasabah = int.Parse(reader["id_nasabah"].ToString());
                            transaksi.nama_nasabah = reader["nama_nasabah"].ToString();
                            transaksi.id_bank = int.Parse(reader["id_bank"].ToString());
                            transaksi.nama_bank = reader["nama_bank"].ToString();
                            list.Add(transaksi);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("readAllforIncome Eror : {0}", ex.Message);
            }
            return list;
        }
    


    }
}
