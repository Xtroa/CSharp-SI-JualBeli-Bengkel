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
    public partial class FormKategoriBarang : Form
    {
        public FormKategoriBarang()
        {
            InitializeComponent();
        }
        List<Kategori> listHasilData = new List<Kategori>();
        public void Form1_Load(object sender, EventArgs e)
        {
            string hasilBaca = Kategori.BacaData("","", listHasilData);

            if (hasilBaca == "1")
            {
                dataGridView1.DataSource = listHasilData;
            }
            else 
            {
                dataGridView1.DataSource = null;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string kriteria = "";
            if (comboBoxKategori.Text == "Kode Kategori")
            {
                kriteria = "KodeKategori";
            }
            else if (comboBoxKategori.Text == "Nama Kategori")
            {
                kriteria = "Nama";
            }
            listHasilData.Clear();

            string hasilBaca = Kategori.BacaData(kriteria, textBoxCari.Text, listHasilData);

            if (hasilBaca == "1")
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listHasilData;
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            TambahKategoriBarang frm = new TambahKategoriBarang();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonUbah_Click(object sender, EventArgs e)
        {
            UbahKategoriBarang frm = new UbahKategoriBarang();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            HapusKategoriBarang frm = new HapusKategoriBarang();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
