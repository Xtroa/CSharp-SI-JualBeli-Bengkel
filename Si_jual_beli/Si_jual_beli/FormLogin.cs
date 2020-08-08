using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PenjualanPembelian_LIB;
namespace Si_jual_beli
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        List<Pegawai> listHasilData = new List<Pegawai>();
        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.Height = 50 + panelLogin.Height;
            textBoxUser.Focus();
            //beri nilai awal server dan database
            textBoxServer.Text = "localhost";
            textBoxDatabase.Text = "si_jual_beli";
            
            this.ControlBox = false;
        }

        private void linkLabelPengaturan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Height = 50 + panelLogin.Height + panelPengaturan.Height;
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (textBoxServer.Text != "" && textBoxDatabase.Text != "")
            {
                this.Height = 50 + panelLogin.Height;
            }
            else
            {
                MessageBox.Show("Nama server dan nama database tidak boleh dikosongi", "kesalahan");
            }
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah Anda Ingin Keluar dari aplikasi ini ???", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxUser.Text != "")
            {
                //create objek bertipe koneksi dengan memanggil constructor berparameter milik class koneksi


                String username = "root";
                String password = "";
                Koneksi k = new Koneksi(textBoxServer.Text, textBoxDatabase.Text, username, password);

                string hasilConnect = k.Connect();//panggil method milik class koneksi

                if (hasilConnect == "1")//jika koneksi ke database berhasil
                {
                    //waktu login berhasil dapatkan kode dan nama pegawai berdasarkan username yg diinputkan waktu login
                    FormUtama frmutama = (FormUtama)this.Owner;
                    frmutama.Enabled = true;
                    

                    //waktu login berhasil dapatkan kode dan nama pegawai berdasarkan username yang diinputkan waktu login
                    string hasilCariPegawai = Pegawai.BacaData("Username", textBoxUser.Text, listHasilData);
                    if(hasilCariPegawai == "1")
                    {
                        if (listHasilData.Count > 0)//jika username ditemukan
                        {
                            MessageBox.Show("Selamat Datang di bengkel jaya sakti motor 228", "info");//tampilkan ucapan selamat datang
                            frmutama.Enabled = true; // agar form utama bisa diakses

                            ////tampilkan kode, nama, dan jabatan pegawai yang sedang login di label yang terdapat di form utama
                            frmutama.labelKodePeg.Text = listHasilData[0].KodePegawai.ToString();
                            frmutama.labelNamaPeg.Text = listHasilData[0].Nama;
                            frmutama.labelJabatan.Text = listHasilData[0].Jabatan.NamaJabatan;

                            ////panggil method untuk pengaturan hak akses menu yang akan ditampilkan di form utama
                            frmutama.PengaturanHakAksesMenu(listHasilData[0].Jabatan);

                            this.Close(); //tutup form login
                        }
                        else
                        {
                            MessageBox.Show("Username atau password salah");
                        }
                    }
                }
                else //jika gagal
                {
                    MessageBox.Show("Koneksi gagal.Pesan kesalahan : " + hasilConnect, "kesalahan");//tampilkan pesan kesalahan
                }
            }
            else
            {
                MessageBox.Show("Username tidak boleh kosong", "kesalahan");
            }
        }

        private void textBoxUser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
