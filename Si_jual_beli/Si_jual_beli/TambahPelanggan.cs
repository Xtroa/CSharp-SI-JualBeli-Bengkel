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
    public partial class TambahPelanggan : Form
    {
        public TambahPelanggan()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKode.Text) && !string.IsNullOrEmpty(textBoxNama.Text) && !string.IsNullOrEmpty(textBoxAlamat.Text) && !string.IsNullOrEmpty(textBoxTelp.Text))
            {
                int kod = int.Parse(textBoxKode.Text);
                string nam = textBoxNama.Text;
                string almt = textBoxAlamat.Text;
                string telp = textBoxTelp.Text;
                //ciptakan objek yang akan ditambahkan
                Pelanggan pl = new Pelanggan(kod, nam, almt, telp);

                //panggil static method tambahdata di class kategori
                string hasilTambah = Pelanggan.TambahData(pl);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Kategori telah tersimpan. ", "informasi");
                    TambahPelanggan_Load(sender, e);
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
            FormDaftarPelanggan frmDaftar = (FormDaftarPelanggan)this.Owner;
            frmDaftar.FormDaftarPelanggan_Load(sender, e);
            this.Close();
        }

        private void textBoxAlamat_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxTelp_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxNama_TextChanged(object sender, EventArgs e)
        {

        }

        private void TambahPelanggan_Load(object sender, EventArgs e)
        {
            int kodeTerbaru;
            string hasilGenerate = Pelanggan.GenerateKode(out kodeTerbaru);

            if (hasilGenerate == "1")
            {
                textBoxKode.Text = kodeTerbaru.ToString();
                textBoxKode.Enabled = false;
                //arahkan cursor ke textboxNama
                textBoxNama.Focus();
            }
            else
            {
                MessageBox.Show("gagal melakukan generate kode. Pesan Kesalahan : " + hasilGenerate);
            }
        }
    }
}
