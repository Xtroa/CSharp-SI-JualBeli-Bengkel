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
    public partial class FormDaftarJabatan : Form
    {
        public FormDaftarJabatan()
        {
            InitializeComponent();
        }
        List<Jabatan> listHasilData = new List<Jabatan>();
        private void textBoxCari_TextChanged(object sender, EventArgs e)
        {
            string kriteria = "";
            if (comboBoxJabatan.Text == "Id Jabatan")
            {
                kriteria = "IdJabatan";
            }
            else if (comboBoxJabatan.Text == "Nama Jabatan")
            {
                kriteria = "Nama";
            }
            listHasilData.Clear();

            string hasilBaca = Jabatan.BacaData(kriteria, textBoxCari.Text, listHasilData);

            if (hasilBaca == "1")
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listHasilData;
            }
        }

        public void FormDaftarJabatan_Load(object sender, EventArgs e)
        {
            string hasilBaca = Jabatan.BacaData("", "", listHasilData);

            if (hasilBaca == "1")
            {
                dataGridView1.DataSource = listHasilData;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahJabatan frm = new FormTambahJabatan();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonUbah_Click(object sender, EventArgs e)
        {
            FormUbahJabatan frm = new FormUbahJabatan();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            FormHapusJabatan frm = new FormHapusJabatan();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
