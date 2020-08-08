using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// agar mysqlconnection dapat digunakan
using MySql.Data.MySqlClient;

// agar dapat mengakses app.config
using System.Configuration;

namespace PenjualanPembelian_LIB
{
    public class Koneksi
    {
        private string namaServer;
        private string namaDatabase;
        private string username;
        private string password;
        private MySqlConnection koneksiDB;
        public static String strCon;
        public Koneksi() {
            koneksiDB = new MySqlConnection();
            //set connection string sesuai dengan yang ada di app.config
            koneksiDB.ConnectionString = ConfigurationManager.ConnectionStrings["KonfigurasiKoneksi"].ConnectionString;

            //panggil method connect
            string hasilConnect = Connect();

        } 

        public Koneksi(string server, string database, string user, string pwd) {
            namaServer = server;
            namaDatabase = database;
            username = user;
            password = pwd;

            strCon = "Server=" + namaServer + "; Database=" + namaDatabase+ "; Uid=" + username + "; Pwd=" + password;

            koneksiDB = new MySqlConnection();
            // set connection string sesuai nama server, database, username, dan password yyang dimasukkan user
            koneksiDB.ConnectionString = strCon;

            // panggil method connect
            string hasilConnect = Connect();

            if (hasilConnect == "sukses") {
                //ubah app config dengan memanggil method updateappconfig
                UpdateAppConfig(strCon);
            }
        }
        #region PROPERTIES
        public string NamaServer
        {
            get { return namaServer; }
            set { namaServer = value; }
        }
        public string NamaDatabase
        {
            get { return namaDatabase; }
            set { namaDatabase = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            private get { return password; }// password hanya boleh dibaca dari dalam class saja 
            set { password = value; }
        }
        public MySqlConnection KoneksiDB
        {
            get { return koneksiDB; }
            private set { koneksiDB = value; }// koneksi hanya boleh diset dari dalam class saja
        }
        #endregion

        #region METHOD
        public string Connect() {
            try
            {
                if (koneksiDB.State == System.Data.ConnectionState.Open)
                {
                    koneksiDB.Close();
                }
                koneksiDB.Open();
                return "1"; //artinya sukses
            }
            catch (MySqlException e){
                return "koneksi gagal. Pesan kesalahan : " + e.Message;
            }
        }

        public void UpdateAppConfig(string connectionString) {// untuk mengubah konfig connection string di app
            // buka konfigurasi app.config
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // set app.config pada nama tag koneksi dengan string koneksi yang dimasukkan pengguna
            config.ConnectionStrings.ConnectionStrings["KonfigurasiKoneksi"].ConnectionString = connectionString;

            //simpan app.config yang telah diperbarui
            config.Save(ConfigurationSaveMode.Modified, true);

            // reload app.config dengan pengaturan yang baru
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        public static void JalankanPerintahDML(string pSql)
        {
            Koneksi k = new Koneksi();
            k.Connect();

            //buat mysqlcommand
            MySqlCommand c = new MySqlCommand(pSql, k.KoneksiDB);

            //gunakan executenonquery untuk menjalankan perintah insert/update/delete
            c.ExecuteNonQuery();
        }
        public static MySqlDataReader JalankanPerintahQuery(string pSql)
        {
            Koneksi k = new Koneksi();
            k.Connect();

            MySqlCommand c = new MySqlCommand(pSql, k.KoneksiDB);

            MySqlDataReader hasil = c.ExecuteReader();

            return hasil;
        }

        public static string GetNamaServer() { 
            //ambil connection string yang tersimpan di app.config
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KonfigurasiKoneksi"].ConnectionString;

            //ambil nama server
            return con.DataSource;
        }

        public static string GetNamaDatabase() { 
            //ambil connection string yang tersimpan di app.config
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KonfigurasiKoneksi"].ConnectionString;

            //ambil nama database
            return con.Database;
        }
        #endregion
    }
}
