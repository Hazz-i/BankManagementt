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
                            nasabah.username = reader["username"].ToString();
                            nasabah.password = reader["password"].ToString();
                            nasabah.email = reader["email"].ToString();
                            nasabah.alamat = reader["alamat"].ToString();
                            nasabah.no_telepon = int.Parse(reader["no_telepon"].ToString());
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
        //BAGIAN DAFTAR AKUN
        //Validasi akun sudah terbuat atau belum by username
        public bool DaftarValidasi(string email)
        {
            bool valid = false;
            try
            {
                string sql = @"select email, password from nasabah where email = @email";
                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            valid = true;
                        }
                        else
                        {
                            valid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Get Email and Pass Error: {0}", ex.Message);
            }
            return valid;
        }
        //Query Menambahkan Nasabah / Add Akun Nasabah
        public int Create(Nasabah nasabah)
        {
            int result = 0;
            string sql = @"insert into nasabah (nama_nasabah, alamat, no_telepon, email, username, password) values (@nama_nasabah, @alamat, @no_telepon, @email, @username, @password)";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@nama_nasabah", nasabah.nama_nasabah);
                cmd.Parameters.AddWithValue("@alamat", nasabah.alamat);
                cmd.Parameters.AddWithValue("@email", nasabah.email);
                cmd.Parameters.AddWithValue("@no_telepon", nasabah.no_telepon);
                cmd.Parameters.AddWithValue("@username", nasabah.username);
                cmd.Parameters.AddWithValue("@password", nasabah.password);
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
        //BAGIAN DAFTAR AKUN END
        //BAGIAN LOGIN VALIDASI
        public bool LoginValidasi(string email, string password)
        {
            bool valid = false;
            try
            {
                string sql = @"select email, password from nasabah where email = @email && password = @password";
                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            valid = true;
                        }
                        else
                        {
                            valid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Get Email and Pass Error: {0}", ex.Message);
            }
            return valid;
        }
        //END BAGIAN LOGIN VALIDASI
        //READ BY EMAIL
        public List<Nasabah> ReadUserByEmail(string email)
        {
            List<Nasabah> list = new List<Nasabah>();
            try
            {
                string sql = @"select * from nasabah where email = '" + email + "'";
                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Nasabah nasabah = new Nasabah();
                            nasabah.id_nasabah= int.Parse(reader["id_nasabah"].ToString());
                            nasabah.nama_nasabah = reader["nama_nasabah"].ToString();
                            nasabah.email = reader["email"].ToString();
                            nasabah.username = reader["username"].ToString();
                            nasabah.password = reader["password"].ToString();
                            nasabah.no_telepon = int.Parse(reader["no_telepon"].ToString());
                            nasabah.alamat = reader["alamat"].ToString();
                            list.Add(nasabah);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll Email Eror: {0}", ex.Message);
            }
            return list;
        }
        //END READ BY EMAIL
        // Query Update Nasabah
        public int Update(Nasabah nasabah, int id_nasabah)
        {
            int result = 0;
            string sql = @"update nasabah set nama_nasabah = @nama_nasabah, alamat = @alamat, no_telepon = @no_telepon, email = @email, username = @username, password = @password where id_nasabah = @id_nasabah";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@nama_nasabah", nasabah.nama_nasabah);
                cmd.Parameters.AddWithValue("@alamat", nasabah.alamat);
                cmd.Parameters.AddWithValue("@no_telepon", nasabah.no_telepon);
                cmd.Parameters.AddWithValue("@username", nasabah.username);
                cmd.Parameters.AddWithValue("@password", nasabah.password);
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
        public int Delete(int nasabahId)
        {
            int result = 0;
            string sql = @"delete from nasabah where id_nasabah = @id_nasabah";
            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@id_nasabah", nasabahId);
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
