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
    public partial class FormTambahBarang : Form
    {
        public FormTambahBarang()
        {
            InitializeComponent();
        }
        List<Kategori> listDataKategori = new List<Kategori>();
        private void FormTambahBarang_Load(object sender, EventArgs e)
        {
            //sesuaikan panjang textbox dengan ukuran field
            textBoxKodeBarang.MaxLength = 5;
            textBoxBarcode.MaxLength = 13;
            textBoxNama.MaxLength = 45;
            //buat agar user hanya bisa memilih item yang ada di combobox (tidak bisa mengetikkan yang lain)
            comboBoxKatBarang.DropDownStyle = ComboBoxStyle.DropDownList;
            //beri nilai default untuk barcode, nama barang, harga jual, dan stok
            textBoxKodeBarang.Text = "";
            textBoxKodeBarang.Enabled = true;
            textBoxBarcode.Text = "";
            textBoxNama.Text = "";
            textBoxHargaJual.Text = "0";
            textBoxStok.Text = "0";
            //buat agar harga jual dan stok rata kanan
            textBoxHargaJual.TextAlign = HorizontalAlignment.Right;
            textBoxStok.TextAlign = HorizontalAlignment.Right;
            //tampilkan semua kategori yang ada di tabel kategori
            //gunakan method BacaData di class kategori untuk mendapatkan data semua kategori
            string hasilBaca = Kategori.BacaData("", "", listDataKategori);
            if (hasilBaca == "1")
            {
                comboBoxKatBarang.Items.Clear();
                for (int i = 0; i < listDataKategori.Count; i++)
                {
                    //tampilkan dengan format kode kategori - nama kategori
                    //contoh : 03 -fashion
                    comboBoxKatBarang.Items.Add(listDataKategori[i].KodeKategori + " - " + listDataKategori[i].Nama);
                }
            }
            else {
                MessageBox.Show("data barang gagal ditampilkan. Pesan Kesalahan : " + hasilBaca    );
            }
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKodeBarang.Text) && !string.IsNullOrEmpty(textBoxHargaJual.Text) &&!string.IsNullOrEmpty(textBoxNama.Text)  && !string.IsNullOrEmpty(textBoxStok.Text))
            {
                //simpan index kategori yang dipilih user di combobox
                int indexDipilihUser = comboBoxKatBarang.SelectedIndex;
                //ciptakan objek kategori yang dipilih oleh user 
                //kategori barang diambil dari listKategori sesuai index yang bersesuaian dengan comboboxkategori
                Kategori kategoribrg = listDataKategori[indexDipilihUser];

                //ciptakan objek barang
                Barang brg = new Barang(textBoxKodeBarang.Text, textBoxBarcode.Text, textBoxNama.Text, int.Parse(textBoxHargaJual.Text), int.Parse(textBoxStok.Text), kategoribrg);
                //panggil static method tambahdata di class barang
                string hasilTambah = Barang.TambahData(brg);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Barang telah tersimpan", "informasi");
                    // jalankan form load
                    FormTambahBarang_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("gagal menambah barang . Pesan kesalahan : " + hasilTambah);
                }
            }
            else
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu");
            }
        }

        private void comboBoxKatBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //generate kode barang terbaru sesuai kategori yang dipilih user
            string kodeKategori = comboBoxKatBarang.Text.Substring(0, 2);
            string kodeTerbaru;
            //gunakan method GenerateKode di class Barang
            string hasilGenerate = Barang.GenerateKode(kodeKategori, out kodeTerbaru);

            if (hasilGenerate == "1")
            {
                // kode barang terisi otomatis sesuai hasil generate kode
                textBoxKodeBarang.Text = kodeTerbaru;

                //arahkan cursor ke textboxbarcode
                textBoxBarcode.Focus();
            }
            else
            {
                MessageBox.Show("gagal melakukan generate kode. pesan kesalahan : " + hasilGenerate);
            }
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {

        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            FormDaftarBarang frmDaftar = (FormDaftarBarang)this.Owner;
            frmDaftar.FormDaftarBarang_Load(sender, e);
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBoxKodeBarang_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
