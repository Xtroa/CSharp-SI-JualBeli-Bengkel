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
    public partial class FormHapusBarang : Form
    {
        public FormHapusBarang()
        {
            InitializeComponent();
        }
        List<Barang> listHasilData = new List<Barang>();
        private void FormHapusBarang_Load(object sender, EventArgs e)
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

            textBoxNama.Enabled = false;
            textBoxKategori.Enabled = false;
            textBoxHargaJual.Enabled = false;
            textBoxStok.Enabled = false;

            textBoxHargaJual.TextAlign = HorizontalAlignment.Right;
            textBoxStok.TextAlign = HorizontalAlignment.Right;
        }

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            //pastikan dulu kepada user apakah akan menghapus data
            DialogResult konfirmasi = MessageBox.Show("Data Barang akan terhapus. Apakah anda yakin ? ", "Konfirmasi", MessageBoxButtons.YesNo);

            if (konfirmasi == System.Windows.Forms.DialogResult.Yes)//jika user yakin ingin menghapus
            {
                //ciptakan objek yang akan ditambahkan
                string kodeKategori = textBoxKategori.Text.Substring(1, 2);
                string namaKategori = textBoxKategori.Text.Substring(6, textBoxKategori.Text.Length - 6);
                Kategori kate = new Kategori(kodeKategori, namaKategori);
                Barang brg = new Barang(textBoxKodeBarang.Text, textBoxBarcode.Text, textBoxNama.Text, int.Parse(textBoxHargaJual.Text), int.Parse(textBoxStok.Text), kate);

                //panggil static method HapusData di class Kategori
                string hasilTambah = Barang.HapusData(brg);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Barang telah dihapus.", "Informasi");
                    FormHapusBarang_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal Menghapus Kategori.Pesan Kesalahan : " + hasilTambah);
                }
            }
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

                        
                        textBoxKategori.Text = listHasilData[0].Kategori.KodeKategori + " - " + listHasilData[0].Kategori.Nama;
                        buttonHapus.Focus();
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
