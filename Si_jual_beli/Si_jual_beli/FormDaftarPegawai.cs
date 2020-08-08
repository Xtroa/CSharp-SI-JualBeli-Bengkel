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
    public partial class FormDaftarPegawai : Form
    {
        public FormDaftarPegawai()
        {
            InitializeComponent();
        }
        List<Pegawai> listHasilData = new List<Pegawai>();
        private void textBoxCari_TextChanged(object sender, EventArgs e)
        {
            string kriteria = "";
            if (comboBoxPegawai.Text == "Kode Pegawai")
            {
                kriteria = "P.KodePegawai";
            }
            else if (comboBoxPegawai.Text == "Nama")
            {
                kriteria = "P.Nama";
            }
            else if (comboBoxPegawai.Text == "Tanggal Lahir")
            {
                kriteria = "P.TglLahir";
            }
            else if (comboBoxPegawai.Text == "Alamat")
            {
                kriteria = "P.Alamat";
            }
            else if (comboBoxPegawai.Text == "Gaji")
            {
                kriteria = "P.Gaji";
            }
            else if (comboBoxPegawai.Text == "Username")
            {
                kriteria = "P.Username";
            }
            else if (comboBoxPegawai.Text == "Id Jabatan")
            {
                kriteria = "P.IdJabatan";
            }
            else if (comboBoxPegawai.Text == "Nama Jabatan")
            {
                kriteria = "J.Nama";
            }

            //tampilkan data barang sesuai kriteria
            string hasilBaca = Pegawai.BacaData(kriteria, textBoxCari.Text, listHasilData);

            if (hasilBaca == "1")
            {
                dataGridView1.Rows.Clear();

                //tampilkan semua isi listBarang di datagridview
                for (int i = 0; i < listHasilData.Count; i++)
                {
                    dataGridView1.Rows.Add(listHasilData[i].KodePegawai, listHasilData[i].Nama,
                        listHasilData[i].TglLahir, listHasilData[i].Alamat, listHasilData[i].Gaji,
                        listHasilData[i].Username, listHasilData[i].Jabatan.IdJabatan, listHasilData[i].Jabatan.NamaJabatan);
                }
            }
        }
        private void FormatDataGrid()
        {
            //kosongi semua kolom di datagridview
            dataGridView1.Columns.Clear();

            //menambah kolom didatagridview
            dataGridView1.Columns.Add("KodePegawai", "Kode Pegawai");
            dataGridView1.Columns.Add("Nama", "Nama");
            dataGridView1.Columns.Add("TglLahir", "Tanggal Lahir");
            dataGridView1.Columns.Add("Alamat", "Alamat");
            dataGridView1.Columns.Add("Gaji", "Gaji");
            dataGridView1.Columns.Add("Username", "Username");
            dataGridView1.Columns.Add("IdJabatan", "Id Jabatan");
            dataGridView1.Columns.Add("NamaJabatan", "Nama Jabatan");

            //agar lebar kolom dapat menyesuaikan panjang/isi data
            dataGridView1.Columns["KodePegawai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["TglLahir"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Alamat"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Gaji"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["IdJabatan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["NamaJabatan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //agar harga jual dan stok rata kanan
            dataGridView1.Columns["Gaji"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["KodePegawai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //agar harga jual ditampilkan dengan format pemisah ribuan (1000 delimeter)
            dataGridView1.Columns["Gaji"].DefaultCellStyle.Format = "0,###";
        }

        public void FormDaftarPegawai_Load(object sender, EventArgs e)
        {
            comboBoxPegawai.DropDownStyle = ComboBoxStyle.DropDownList;

            FormatDataGrid();

            string hasilBaca = Pegawai.BacaData("", "", listHasilData);

            if (hasilBaca == "1")
            {
                //kosongi isi datagridview
                dataGridView1.Rows.Clear();

                //tampilkan semua isi listBarang di datagridview
                for (int i = 0; i < listHasilData.Count(); i++)
                {
                    dataGridView1.Rows.Add(listHasilData[i].KodePegawai, listHasilData[i].Nama, listHasilData[i].TglLahir, listHasilData[i].Alamat, listHasilData[i].Gaji, listHasilData[i].Username, listHasilData[i].Jabatan.IdJabatan, listHasilData[i].Jabatan.NamaJabatan);
                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahPegawai frm = new FormTambahPegawai();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonUbah_Click(object sender, EventArgs e)
        {
            FormUbahPegawai frm = new FormUbahPegawai();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            FormHapusPegawai frm = new FormHapusPegawai();
            frm.Owner = this;
            frm.Show();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
