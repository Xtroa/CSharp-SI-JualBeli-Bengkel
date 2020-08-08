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
    public partial class UbahKategoriBarang : Form
    {
        public UbahKategoriBarang()
        {
            InitializeComponent();
        }
        List<Kategori> listHasilData = new List<Kategori>();
        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKode.Text) && !string.IsNullOrEmpty(textBoxNama.Text))
            {
                //ciptakan objek yg akan ditambahkan
                Kategori kt = new Kategori(textBoxKode.Text, textBoxNama.Text);

                //panggil static method UbahData di class Kategori
                string hasilTambah = Kategori.UbahData(kt);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Kategori telah diubah.", "Informasi");
                    UbahKategoriBarang_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal Mengubah Kategori. Pesan Kesalahan : " + hasilTambah);
                }
            }
            else
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu");
            }
        }

        private void UbahKategoriBarang_Load(object sender, EventArgs e)
        {
            textBoxKode.MaxLength = 2;
        }

        private void textBoxNama_TextChanged(object sender, EventArgs e)
        {
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            FormKategoriBarang frmDaftar = (FormKategoriBarang)this.Owner;
            frmDaftar.Form1_Load(sender, e);
            this.Close();
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxKode.Text = "";
            textBoxNama.Text = "";
            textBoxKode.Focus();
        }

        private void textBoxKode_TextChanged(object sender, EventArgs e)
        {
            //jika user telah mengetik sesuai panjang karakter kodeKategori
            if (textBoxKode.Text.Length == textBoxKode.MaxLength)
            {
                listHasilData.Clear();

                string hasilBaca = Kategori.BacaData("KodeKategori", textBoxKode.Text, listHasilData);
                if (hasilBaca == "1")
                {
                    if (listHasilData.Count() > 0)
                    {
                        textBoxNama.Text = listHasilData[0].Nama;
                        textBoxNama.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Kode Kategori tidak ditemukan. Proses Ubah Data tidak bisa dilakukan.");
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
