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
    public partial class TambahKategoriBarang : Form
    {
        public TambahKategoriBarang()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKode.Text) && !string.IsNullOrEmpty(textBoxNama.Text))
            {
                string kat = textBoxKode.Text;
                string nam = textBoxNama.Text;
                //ciptakan objek yang akan ditambahkan
                Kategori kt = new Kategori(kat, nam);

                //panggil static method tambahdata di class kategori
                string hasilTambah = Kategori.TambahData(kt);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Kategori telah tersimpan. ", "informasi");
                    TambahKategoriBarang_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal menambah kategori. pesan kesalahan : " + hasilTambah);
                }
            }
            else
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu");
            }

        }

        private void buttonKosongi_Click_1(object sender, EventArgs e)
        {
            textBoxKode.Text = "";
            textBoxNama.Text = "";
            textBoxKode.Focus();
        }

        private void buttonKeluar_Click_1(object sender, EventArgs e)
        {

            FormKategoriBarang frmDaftar = (FormKategoriBarang)this.Owner;
            frmDaftar.Form1_Load(sender, e);
            this.Close();
        }

        private void TambahKategoriBarang_Load(object sender, EventArgs e)
        {
            Kategori daftar = new Kategori();
            string hasil = daftar.GenerateKode();
            if (hasil == "sukses")
            {
                textBoxKode.Text = daftar.KodeKategori;
                textBoxKode.Enabled = false;
            }
            else
            {
                MessageBox.Show("Generate kode gagal dilakukan. Pesan kesalahan = " + hasil);
            }
        }

    }
}
