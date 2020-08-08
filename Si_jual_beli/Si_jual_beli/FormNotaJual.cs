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
    public partial class FormNotaJual : Form
    {
        public FormNotaJual()
        {
            InitializeComponent();
        }
        string kriteria = "";
        List<NotaJual> listHasilData = new List<NotaJual>();
        public void FormNotaJual_Load(object sender, EventArgs e)
        {
            comboBoxNotaJual.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxNotaJual.SelectedIndex = 0;
            FormatDataGrid();

            string hasilBaca = NotaJual.BacaData("", "", listHasilData);

            if (hasilBaca == "1")
            {
                //kosongi isi datagridview
                dataGridView1.Rows.Clear();

                //tampilkan semua isi listBarang di datagridview
                for (int i = 0; i < listHasilData.Count(); i++)
                {
                    dataGridView1.Rows.Add(listHasilData[i].NoNotaJual, listHasilData[i].Tanggal, listHasilData[i].Pelanggan.KodePelanggan, listHasilData[i].Pelanggan.Nama, listHasilData[i].Pelanggan.Alamat, listHasilData[i].Pegawai.KodePegawai, listHasilData[i].Pegawai.Nama);
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
            dataGridView1.Columns.Add("NoNotaJual", "No Nota");
            dataGridView1.Columns.Add("Tanggal", "Tanggal");
            dataGridView1.Columns.Add("KodePelanggan", "Kode Pelanggan");
            dataGridView1.Columns.Add("Nama", "Nama Pelanggan");
            dataGridView1.Columns.Add("Alamat", "Alamat Pelanggan");
            dataGridView1.Columns.Add("KodePegawai", "Kode Pegawai");
            dataGridView1.Columns.Add("Nama", "Nama Pegawai");

            //agar lebar kolom dapat menyesuaikan panjang/isi data
            dataGridView1.Columns["NoNotaJual"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Tanggal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["KodePelanggan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Alamat"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["KodePegawai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //agar harga jual dan stok rata kanan
            dataGridView1.Columns["KodePegawai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["KodePelanggan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }
        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahNotaJual frm = new FormTambahNotaJual();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonUbah_Click(object sender, EventArgs e)
        {
            FormUbahNotaJual frm = new FormUbahNotaJual();
            frm.Owner = this;
            frm.Show();
        }

        private void textBoxCari_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxNotaJual.Text == "Nomor Nota")
            {
                kriteria = "N.NoNota";
            }
            else if (comboBoxNotaJual.Text == "Tanggal")
            {
                kriteria = "N.Tanggal";
            }
            else if (comboBoxNotaJual.Text == "Kode Pelanggan")
            {
                kriteria = "N.KodePelanggan";
            }
            else if (comboBoxNotaJual.Text == "Nama Pelanggan")
            {
                kriteria = "P.Nama";
            }
            else if (comboBoxNotaJual.Text == "Alamat Pelanggan")
            {
                kriteria = "P.Alamat";
            }
            else if (comboBoxNotaJual.Text == "Kode Pegawai")
            {
                kriteria = "N.KodePegawai";
            }
            else if (comboBoxNotaJual.Text == "Nama Pegawai")
            {
                kriteria = "PG.Nama";
            }

            //tampilkan data barang sesuai kriteria
            string hasilBaca = NotaJual.BacaData(kriteria, textBoxCari.Text, listHasilData);

            if (hasilBaca == "1")
            {
                dataGridView1.Rows.Clear();

                //tampilkan semua isi listBarang di datagridview
                for (int i = 0; i < listHasilData.Count; i++)
                {
                    dataGridView1.Rows.Add(listHasilData[i].NoNotaJual, listHasilData[i].Tanggal, listHasilData[i].Pelanggan.KodePelanggan, listHasilData[i].Pelanggan.Nama, listHasilData[i].Pelanggan.Alamat, listHasilData[i].Pegawai.KodePegawai, listHasilData[i].Pegawai.Nama);
                }
            }
        }

        private void buttonCetak_Click(object sender, EventArgs e)
        {
            string hasilCetak = NotaJual.CetakNota(kriteria, textBoxCari.Text, "daftar_nota_jual.txt");
            MessageBox.Show("Data telah tercetak");
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            FormHapusNotaJual frm = new FormHapusNotaJual();
            frm.Owner = this;
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            FormLaporanPenjualan frm = new FormLaporanPenjualan();
            frm.Owner = this;
            frm.Show();
        }
    }
}
