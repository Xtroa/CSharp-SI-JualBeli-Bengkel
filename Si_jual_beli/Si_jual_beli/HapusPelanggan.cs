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
    public partial class HapusPelanggan : Form
    {
        public HapusPelanggan()
        {
            InitializeComponent();
        }
        List<Pelanggan> listHasilData = new List<Pelanggan>();

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxKode.Text = "";
            textBoxNama.Text = "";
            textBoxAlamat.Text = "";
            textBoxTelp.Text = "";
            textBoxKode.Focus();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HapusPelanggan_Load(object sender, EventArgs e)
        {
            textBoxKode.MaxLength = 1;

            textBoxAlamat.Enabled = false;
            textBoxTelp.Enabled = false;
            textBoxNama.Enabled = false;
        }

        private void textBoxKode_TextChanged(object sender, EventArgs e)
        {
            //jika user telah mengetik sesuai panjang karakter kodeKategori
            if (textBoxKode.Text.Length == textBoxKode.MaxLength)
            {
                listHasilData.Clear();

                string hasilBaca = Pelanggan.BacaData("KodePelanggan", textBoxKode.Text, listHasilData);
                if (hasilBaca == "1")
                {
                    if (listHasilData.Count() > 0)
                    {
                        textBoxNama.Text = listHasilData[0].Nama;
                        textBoxAlamat.Text = listHasilData[0].Alamat;
                        textBoxTelp.Text = listHasilData[0].Telepon;
                        
                        buttonHapus.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Kode Pelanggan tidak ditemukan. Proses Hapus Data tidak bisa dilakukan.");
                        textBoxNama.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Perintah SQL gagal dijalankan.Pesan kesalahan = " + hasilBaca);
                }
            }
        }

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            //pastikan dulu kepada user apakah akan menghapus data
            DialogResult konfirmasi = MessageBox.Show("Data Pelanggan akan terhapus. Apakah anda yakin ? ", "Konfirmasi", MessageBoxButtons.YesNo);

            if (konfirmasi == System.Windows.Forms.DialogResult.Yes)//jika user yakin ingin menghapus
            {
                //ciptakan objek yang akan ditambahkan
                Pelanggan pl = new Pelanggan(int.Parse(textBoxKode.Text), textBoxNama.Text, textBoxAlamat.Text, textBoxTelp.Text);

                //panggil static method HapusData di class Kategori
                string hasilTambah = Pelanggan.HapusData(pl);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Pelanggan telah dihapus.", "Informasi");
                    HapusPelanggan_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal Menghapus Pelanggan.Pesan Kesalahan : " + hasilTambah);
                }
            }
        }
    }
}
