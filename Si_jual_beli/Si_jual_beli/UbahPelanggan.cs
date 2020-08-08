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
    public partial class UbahPelanggan : Form
    {
        public UbahPelanggan()
        {
            InitializeComponent();
        }
        List<Pelanggan> listHasilData = new List<Pelanggan>();
        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKode.Text) && !string.IsNullOrEmpty(textBoxNama.Text))
            {
                //ciptakan objek yg akan ditambahkan
                Pelanggan pl = new Pelanggan(int.Parse(textBoxKode.Text), textBoxNama.Text, textBoxAlamat.Text, textBoxTelp.Text);

                //panggil static method UbahData di class Kategori
                string hasilTambah = Pelanggan.UbahData(pl);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Pelanggan telah diubah.", "Informasi");
                    UbahPelanggan_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal Mengubah Pelanggan. Pesan Kesalahan : " + hasilTambah);
                }
            }
            else
            {
                MessageBox.Show("Lengkapi data terlebih dahulu");
            }
        }

        private void UbahPelanggan_Load(object sender, EventArgs e)
        {
            textBoxKode.MaxLength = 1;
        }

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
                        textBoxNama.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Kode Pelanggan tidak ditemukan. Proses Ubah Data tidak bisa dilakukan.");
                        textBoxNama.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Perintah SQL gagal dijalankan.Pesan kesalahan = " + hasilBaca);
                }
            }
        }
    }
}
