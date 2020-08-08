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
    public partial class FormDaftarSupplier : Form
    {
        public FormDaftarSupplier()
        {
            InitializeComponent();
        }

        List<Supplier> listHasilData = new List<Supplier>();
        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahSupplier frm = new FormTambahSupplier();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonUbah_Click(object sender, EventArgs e)
        {
            FormUbahSupplier frm = new FormUbahSupplier();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            FormHapusSupplier frm = new FormHapusSupplier();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void FormDaftarSupplier_Load(object sender, EventArgs e)
        {
            comboBoxSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSupplier.SelectedIndex = 0;

            string hasilBaca = Supplier.BacaData("", "", listHasilData);

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
            if (comboBoxSupplier.Text == "Kode Supplier")
            {
                kriteria = "KodeSupplier";
            }
            else if (comboBoxSupplier.Text == "Nama Supplier")
            {
                kriteria = "Nama";
            }
            else if (comboBoxSupplier.Text == "Alamat")
            {
                kriteria = "Alamat";
            }

            listHasilData.Clear();

            string hasilBaca = Supplier.BacaData(kriteria, textBoxCari.Text, listHasilData);

            if (hasilBaca == "1")
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listHasilData;
            }
        }
    }
}
