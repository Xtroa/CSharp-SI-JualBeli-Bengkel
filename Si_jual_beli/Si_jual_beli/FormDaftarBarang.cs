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
    public partial class FormDaftarBarang : Form
    {
        public FormDaftarBarang()
        {
            InitializeComponent();
        }
        List<Barang> listHasilData = new List<Barang>();
        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahBarang frm = new FormTambahBarang();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonUbah_Click(object sender, EventArgs e)
        {
            FormUbahBarang frm = new FormUbahBarang();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            FormHapusBarang frm = new FormHapusBarang();
            frm.Owner = this;
            frm.Show();
        }

        public void FormDaftarBarang_Load(object sender, EventArgs e)
        {
            comboBoxKategori.DropDownStyle = ComboBoxStyle.DropDownList;

            //panggil method untuk menambah kolom pada datagridview
            FormatDataGrid();

            string hasilBaca = Barang.BacaData("", "", listHasilData);

            if (hasilBaca == "1")
            {
                //kosongi isi datagridview
                dataGridView1.Rows.Clear();

                //tampilkan semua isi listBarang di datagridview
                for (int i = 0; i < listHasilData.Count(); i++)
                {
                    dataGridView1.Rows.Add(listHasilData[i].KodeBarang, listHasilData[i].Barcode,listHasilData[i].Nama, listHasilData[i].HargaJual, listHasilData[i].Stok, listHasilData[i].Kategori.KodeKategori, listHasilData[i].Kategori.Nama);
                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void FormatDataGrid() { 
            //kosongi semua kolom di datagridview
            dataGridView1.Columns.Clear();

            //menambah kolom didatagridview
            dataGridView1.Columns.Add("KodeBarang", "Kode Barang");
            dataGridView1.Columns.Add("Barcode", "Barcode");
            dataGridView1.Columns.Add("Nama", "Nama Barang");
            dataGridView1.Columns.Add("HargaJual", "Harga Jual");
            dataGridView1.Columns.Add("Stok", "Stok");
            dataGridView1.Columns.Add("KodeKategori", "Kode Kategori");
            dataGridView1.Columns.Add("Nama", "Nama Kategori");

            //agar lebar kolom dapat menyesuaikan panjang/isi data
            dataGridView1.Columns["KodeBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Barcode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["HargaJual"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Stok"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["KodeKategori"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //agar harga jual dan stok rata kanan
            dataGridView1.Columns["HargaJual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Stok"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //agar harga jual ditampilkan dengan format pemisah ribuan (1000 delimeter)
            dataGridView1.Columns["HargaJual"].DefaultCellStyle.Format = "0,###";

            dataGridView1.Columns[1].Visible = false;
        }

        private void textBoxCari_TextChanged(object sender, EventArgs e)
        {
            string kriteria = "";
            if(comboBoxKategori.Text == "Kode Barang"){
                kriteria = "B.KodeBarang";
            }
            else if (comboBoxKategori.Text == "Barcode")
            {
                kriteria = "B.Barcode";
            }
            else if (comboBoxKategori.Text == "Nama")
            {
                kriteria = "B.Nama";
            }
            else if (comboBoxKategori.Text == "Harga Jual")
            {
                kriteria = "B.HargaJual";
            }
            else if (comboBoxKategori.Text == "Stok")
            {
                kriteria = "B.Stok";
            }
            else if (comboBoxKategori.Text == "Kode Kategori")
            {
                kriteria = "B.KodeKategori";
            }
            else if (comboBoxKategori.Text == "Nama Kategori")
            {
                kriteria = "K.Nama";
            }
            //tampilkan data barang sesuai kriteria
            string hasilBaca = Barang.BacaData(kriteria, textBoxCari.Text, listHasilData);

            if (hasilBaca == "1")
            {
                dataGridView1.Rows.Clear();

                //tampilkan semua isi listBarang di datagridview
                for (int i = 0; i < listHasilData.Count; i++)
                {
                    dataGridView1.Rows.Add(listHasilData[i].KodeBarang, listHasilData[i].Barcode, listHasilData[i].Nama, listHasilData[i].HargaJual, listHasilData[i].Stok, listHasilData[i].Kategori.KodeKategori, listHasilData[i].Kategori.Nama);
                }
            }
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
