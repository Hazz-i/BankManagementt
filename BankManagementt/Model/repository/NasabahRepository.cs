using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BankManagement.Model.Context;
using BankManagement.Model.Entity;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace BankManagement.Model.Repository
{
    public class NasabahRepository
    {
        private MySqlConnection _conn;
        public NasabahRepository(DbContext context)
        {
            _conn = context.Conn;
        }
        //Query Read Semua Nasabah Yang Ada
        public List<Nasabah> ReadAll()
        {
            List<Nasabah> list = new List<Nasabah>();
            try
            {
                string sql = @"select * from nasabah";
                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Nasabah nasabah = new Nasabah();
                            nasabah.id_nasabah = int.Parse(reader["id_nasabah"].ToString());
                            nasabah.nama_nasabah = reader["nama_nasabah"].ToString();
                            nasabah.alamat = reader["alamat"].ToString();
                            nasabah.no_telepon = int.Parse(reader["no_telepon"].ToString());
                            nasabah.tgl_lahir = reader["tgl_lahir"].ToString();
                            list.Add(nasabah);
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
        //Query Menambahkan Nasabah
        public int Create(Nasabah nasabah)
        {
            int result = 0;
            string sql = @"insert into nasabah (nama_nasabah, alamat, no_telepon, tgl_lahir) values (@nama_nasabah, @alamat, @no_telepon, @tgl_lahir)";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@nama_nasabah", nasabah.nama_nasabah);
                cmd.Parameters.AddWithValue("@alamat", nasabah.alamat);
                cmd.Parameters.AddWithValue("@no_telepon", nasabah.no_telepon);
                cmd.Parameters.AddWithValue("@tgl_lahir", nasabah.tgl_lahir);
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
        // Query Update Nasabah
        public int Update(Nasabah nasabah, int id_nasabah)
        {
            int result = 0;
            string sql = @"update nasabah set nama_nasabah = @nama_nasabah, alamat = @alamat, no_telepon = @no_telepon, tgl_lahir = @tgl_lahir where id_nasabah = @id_nasabah";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@nama_nasabah", nasabah.nama_nasabah);
                cmd.Parameters.AddWithValue("@alamat", nasabah.alamat);
                cmd.Parameters.AddWithValue("@no_telepon", nasabah.no_telepon);
                cmd.Parameters.AddWithValue("@tgl_lahir", nasabah.tgl_lahir);
                cmd.Parameters.AddWithValue("@id_nasabah", id_nasabah);
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
        // Query Delete Nasabah
        public int Delete(Nasabah nasabah)
        {
            int result = 0;
            string sql = @"delete from nasabah where id_nasabah = @id_nasabah";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id_nasabah", nasabah.id_nasabah);
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
