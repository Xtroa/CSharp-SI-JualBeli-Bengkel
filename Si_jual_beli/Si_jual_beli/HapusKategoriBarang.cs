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
    public partial class HapusKategoriBarang : Form
    {
        public HapusKategoriBarang()
        {
            InitializeComponent();
        }
        List<Kategori> listHasilData = new List<Kategori>();
        private void buttonHapus_Click(object sender, EventArgs e)
        {
            //pastikan dulu kepada user apakah akan menghapus data
            DialogResult konfirmasi = MessageBox.Show("Data kategori akan terhapus. Apakah anda yakin ? ", "Konfirmasi", MessageBoxButtons.YesNo);

            if (konfirmasi == System.Windows.Forms.DialogResult.Yes)//jika user yakin ingin menghapus
            {
                //ciptakan objek yang akan ditambahkan
                Kategori kt = new Kategori(textBoxKode.Text, textBoxNama.Text);

                //panggil static method HapusData di class Kategori
                string hasilTambah = Kategori.HapusData(kt);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Kategori telah dihapus.", "Informasi");
                    HapusKategoriBarang_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal Menghapus Kategori.Pesan Kesalahan : " + hasilTambah);
                }
            }
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
                        buttonHapus.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Kode Kategori tidak ditemukan. Proses Hapus Data tidak bisa dilakukan.");
                        textBoxNama.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Perintah SQL gagal dijalankan.Pesan kesalahan = " + hasilBaca);
                }
            }
        }

        public void HapusKategoriBarang_Load(object sender, EventArgs e)
        {
            textBoxKode.MaxLength = 2;

            textBoxNama.Enabled = false;
        }

        private void buttonKosongi_Click_1(object sender, EventArgs e)
        {
            textBoxKode.Text = "";
            textBoxNama.Text = "";
            textBoxKode.Focus();
        }

        private void buttonKeluar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
