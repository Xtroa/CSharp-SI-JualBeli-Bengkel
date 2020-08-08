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
    public partial class FormUbahBarang : Form
    {
        public FormUbahBarang()
        {
            InitializeComponent();
        }
        List<Barang> listHasilData = new List<Barang>();
        private void FormUbahBarang_Load(object sender, EventArgs e)
        {
            textBoxKodeBarang.MaxLength = 5;
            textBoxBarcode.MaxLength = 13;
            textBoxNama.MaxLength = 45;

            textBoxKodeBarang.Text = "";
            textBoxKategori.Text = "";
            textBoxKategori.Enabled = false;
            textBoxBarcode.Text = "";
            textBoxNama.Text = "";
            textBoxHargaJual.Text = "0";
            textBoxStok.Text = "0";
            textBoxKodeBarang.Focus();

            textBoxHargaJual.TextAlign = HorizontalAlignment.Right;
            textBoxStok.TextAlign = HorizontalAlignment.Right;
        }

        private void textBoxKodeBarang_TextChanged(object sender, EventArgs e)
        {
            if (textBoxKodeBarang.Text.Length == textBoxKodeBarang.MaxLength)
            {
                string hasilBaca = Barang.BacaData("KodeBarang", textBoxKodeBarang.Text, listHasilData);
                if (hasilBaca == "1")
                {
                    if (listHasilData.Count() > 0)
                    {
                        textBoxBarcode.Text = listHasilData[0].Barcode;
                        textBoxNama.Text = listHasilData[0].Nama;
                        textBoxHargaJual.Text = listHasilData[0].HargaJual.ToString();
                        textBoxStok.Text = listHasilData[0].Stok.ToString();

                        textBoxStok.Enabled = false;
                        textBoxKategori.Text = listHasilData[0].Kategori.KodeKategori + " - " + listHasilData[0].Kategori.Nama;
                        textBoxBarcode.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Kode Barang tidak ditemukan. Proses Ubah Data tidak bisa dilakukan.");
                        textBoxNama.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Perintah SQL gagal dijalankan.Pesan kesalahan = " + hasilBaca);
                }
            }
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKodeBarang.Text) && !string.IsNullOrEmpty(textBoxHargaJual.Text) && !string.IsNullOrEmpty(textBoxNama.Text) && !string.IsNullOrEmpty(textBoxStok.Text))
            {
                //ciptakan objek yg akan ditambahkan
                string kodeKategori = textBoxKategori.Text.Substring(1, 2);
                string namaKategori = textBoxKategori.Text.Substring(6, textBoxKategori.Text.Length - 6);
                Kategori kate = new Kategori(kodeKategori, namaKategori);
                Barang brg = new Barang(textBoxKodeBarang.Text, textBoxBarcode.Text, textBoxNama.Text, int.Parse(textBoxHargaJual.Text), int.Parse(textBoxStok.Text), kate);

                //panggil static method UbahData di class Kategori
                string hasilTambah = Barang.UbahData(brg);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Barang telah diubah.", "Informasi");
                    FormUbahBarang_Load(sender, e);
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

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxKodeBarang.Text = "";
            textBoxKategori.Text = "";
            textBoxKategori.Enabled = false;
            textBoxBarcode.Text = "";
            textBoxNama.Text = "";
            textBoxHargaJual.Text = "0";
            textBoxStok.Text = "0";
            textBoxKodeBarang.Focus();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            FormDaftarBarang frmDaftar = (FormDaftarBarang)this.Owner;
            frmDaftar.FormDaftarBarang_Load(sender, e);
            this.Close();
        }
    }
}
