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
    public partial class FormDaftarPelanggan : Form
    {
        public FormDaftarPelanggan()
        {
            InitializeComponent();
        }
        List<Pelanggan> listHasilData = new List<Pelanggan>();

        public void FormDaftarPelanggan_Load(object sender, EventArgs e)
        {
            string hasilBaca = Pelanggan.BacaData("", "", listHasilData);

            if (hasilBaca == "1")
            {
                dataGridView1.DataSource = listHasilData;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void textBoxCari_TextChanged(object sender, EventArgs e)
        {
            string kriteria = "";
            if (comboBoxKategori.Text == "Kode Pelanggan")
            {
                kriteria = "KodePelanggan";
            }
            else if (comboBoxKategori.Text == "Nama")
            {
                kriteria = "Nama";
            }
            else if (comboBoxKategori.Text == "Alamat")
            {
                kriteria = "Alamat";
            }
            else if (comboBoxKategori.Text == "Telepon")
            {
                kriteria = "Telepon";
            }

            listHasilData.Clear();

            string hasilBaca = Pelanggan.BacaData(kriteria, textBoxCari.Text, listHasilData);

            if (hasilBaca == "1")
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listHasilData;
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            TambahPelanggan frm = new TambahPelanggan();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonUbah_Click(object sender, EventArgs e)
        {
            UbahPelanggan frm = new UbahPelanggan();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            HapusPelanggan frm = new HapusPelanggan();
            frm.Owner = this;
            frm.Show();
        }
    }
}
