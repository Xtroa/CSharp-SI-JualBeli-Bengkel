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
    public partial class FormNotaBeli : Form
    {
        public FormNotaBeli()
        {
            InitializeComponent();
        }
        string kriteria = "";
        List<NotaBeli> listHasilData = new List<NotaBeli>();
        public void FormNotaBeli_Load(object sender, EventArgs e)
        {
            comboBoxNotaBeli.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxNotaBeli.SelectedIndex = 0;
            FormatDataGrid();

            string hasilBaca = NotaBeli.BacaData("", "", listHasilData);

            if (hasilBaca == "1")
            {
                //kosongi isi datagridview
                dataGridView1.Rows.Clear();

                //tampilkan semua isi listBarang di datagridview
                for (int i = 0; i < listHasilData.Count(); i++)
                {
                    dataGridView1.Rows.Add(listHasilData[i].NoNotaBeli, listHasilData[i].Tanggal, listHasilData[i].Supplier.KodeSupplier, listHasilData[i].Supplier.NamaSupplier, listHasilData[i].Supplier.Alamat, listHasilData[i].Pegawai.KodePegawai, listHasilData[i].Pegawai.Nama);
                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }
        private void FormatDataGrid()
        {
            //kosongi semua kolom di datagridview
            dataGridView1.Columns.Clear();

            //menambah kolom didatagridview
            dataGridView1.Columns.Add("NoNotaBeli", "No Nota");
            dataGridView1.Columns.Add("Tanggal", "Tanggal");
            dataGridView1.Columns.Add("KodeSupplier", "Kode Supplier");
            dataGridView1.Columns.Add("NamaSupplier", "Nama Supplier");
            dataGridView1.Columns.Add("Alamat", "Alamat Supplier");
            dataGridView1.Columns.Add("KodePegawai", "Kode Pegawai");
            dataGridView1.Columns.Add("Nama", "Nama Pegawai");

            //agar lebar kolom dapat menyesuaikan panjang/isi data
            dataGridView1.Columns["NoNotaBeli"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Tanggal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["KodeSupplier"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["NamaSupplier"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Alamat"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["KodePegawai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //agar harga jual dan stok rata kanan
            dataGridView1.Columns["KodePegawai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["KodeSupplier"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }
        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahNotaBeli frm = new FormTambahNotaBeli();
            frm.Owner = this;
            frm.Show();
        }

        private void textBoxCari_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxNotaBeli.Text == "Nomor Nota")
            {
                kriteria = "N.NoNota";
            }
            else if (comboBoxNotaBeli.Text == "Tanggal")
            {
                kriteria = "N.Tanggal";
            }
            else if (comboBoxNotaBeli.Text == "Kode Supplier")
            {
                kriteria = "N.KodeSupplier";
            }
            else if (comboBoxNotaBeli.Text == "Nama Supplier")
            {
                kriteria = "S.Nama";
            }
            else if (comboBoxNotaBeli.Text == "Alamat Supplier")
            {
                kriteria = "S.Alamat";
            }
            else if (comboBoxNotaBeli.Text == "Kode Pegawai")
            {
                kriteria = "N.KodePegawai";
            }
            else if (comboBoxNotaBeli.Text == "Nama Pegawai")
            {
                kriteria = "PG.Nama";
            }

            //tampilkan data barang sesuai kriteria
            string hasilBaca = NotaBeli.BacaData(kriteria, textBoxCari.Text, listHasilData);

            if (hasilBaca == "1")
            {
                dataGridView1.Rows.Clear();

                //tampilkan semua isi listBarang di datagridview
                for (int i = 0; i < listHasilData.Count; i++)
                {
                    dataGridView1.Rows.Add(listHasilData[i].NoNotaBeli, listHasilData[i].Tanggal, listHasilData[i].Supplier.KodeSupplier, listHasilData[i].Supplier.NamaSupplier, listHasilData[i].Supplier.Alamat, listHasilData[i].Pegawai.KodePegawai, listHasilData[i].Pegawai.Nama);
                }
            }
        }

        private void buttonCetak_Click(object sender, EventArgs e)
        {
            string hasilCetak = NotaBeli.CetakNota(kriteria, textBoxCari.Text, "daftar_nota_beli.txt");
            MessageBox.Show("Data telah tercetak");
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonUbah_Click(object sender, EventArgs e)
        {
            FormUbahNotaBeli frm = new FormUbahNotaBeli();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            FormHapusNotaBeli frm = new FormHapusNotaBeli();
            frm.Owner = this;
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormLaporanPembelian frm = new FormLaporanPembelian();
            frm.Owner = this;
            frm.Show();
        }
    }
}
