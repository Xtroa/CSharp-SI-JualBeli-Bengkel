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
    public partial class FormHapusNotaJual : Form
    {
        public FormHapusNotaJual()
        {
            InitializeComponent();
        }
        List<Barang> listDataBarang = new List<Barang>();
        List<Pelanggan> listDataPelanggan = new List<Pelanggan>();
        List<NotaJual> listDataNotaJual = new List<NotaJual>();
        private void textBoxNoNota_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNoNota.Text.Length == textBoxNoNota.MaxLength)
            {
                string hasilBaca = NotaJual.BacaData("NoNota", textBoxNoNota.Text, listDataNotaJual);
                if (hasilBaca == "1")
                {
                    if (listDataNotaJual.Count() > 0)
                    {
                        dateTimePickerTanggal.Value = listDataNotaJual[0].Tanggal;
                        comboBoxPelanggan.SelectedIndex = comboBoxPelanggan.Items.IndexOf(listDataNotaJual[0].Pelanggan.KodePelanggan + " - " + listDataNotaJual[0].Pelanggan.Nama);
                        //kosongi isi datagridview
                        dataGridViewBarang.Rows.Clear();

                        //tampilkan semua isi listBarang di datagridview
                        for (int i = 0; i < listDataNotaJual[0].ListNotaJualDetil.Count(); i++)
                        {
                            int subTotal = listDataNotaJual[0].ListNotaJualDetil[i].Harga * listDataNotaJual[0].ListNotaJualDetil[i].Jumlah;
                            dataGridViewBarang.Rows.Add(listDataNotaJual[0].ListNotaJualDetil[i].Barang.KodeBarang, listDataNotaJual[0].ListNotaJualDetil[i].Barang.Nama, listDataNotaJual[0].ListNotaJualDetil[i].Harga, listDataNotaJual[0].ListNotaJualDetil[i].Jumlah, subTotal);
                        }
                        labelAlamat.Text = listDataNotaJual[0].Pelanggan.Alamat;
                        labelGrandTotal.Text = HitungGrandTotal().ToString("0,###");
                        
                        textBoxNoNota.Enabled = false;

                    }
                    else
                    {
                        MessageBox.Show("Nomor Nota tidak ditemukan. Proses Ubah Data tidak bisa dilakukan.");
                        textBoxNoNota.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Perintah SQL gagal dijalankan.Pesan kesalahan = " + hasilBaca);
                }
            }
        }
        private int HitungGrandTotal()
        {
            int grandTotal = 0;
            for (int i = 0; i < dataGridViewBarang.Rows.Count; i++)
            {
                int subTotal = int.Parse(dataGridViewBarang.Rows[i].Cells["SubTotal"].Value.ToString());
                grandTotal = grandTotal + subTotal;
            }
            return grandTotal;
        }
        private void FormHapusNotaJual_Load(object sender, EventArgs e)
        {
            //2. tanggal nota diisi default tanggal sistem
            dateTimePickerTanggal.Value = DateTime.Now;
            dateTimePickerTanggal.Enabled = false;
            comboBoxPelanggan.Enabled = false;

            //3. comboboxPelanggan diisi dengan semua pelanggan yang ada di tabel pelanggan (gunakan method bacadata di class pelanggan)
            comboBoxPelanggan.DropDownStyle = ComboBoxStyle.DropDownList;
            string hasilBacaPelanggan = Pelanggan.BacaData("", "", listDataPelanggan);
            if (hasilBacaPelanggan == "1")
            {
                //kosongi dulu combobox
                comboBoxPelanggan.Items.Clear();
                //tambahkan data pelanggan ke comboboxPelanggan dengan format 'kode pelanggan - nama pelanggan'
                for (int i = 0; i < listDataPelanggan.Count; i++)
                {
                    comboBoxPelanggan.Items.Add(listDataPelanggan[i].KodePelanggan + " - " + listDataPelanggan[i].Nama);
                }
            }
            //5. tambahkan kolom di datagridviewbarang
            FormatDataGrid();
            textBoxNoNota.MaxLength = 11;
        }
        private void FormatDataGrid()
        {
            //kosongi semua kolom di datagridview
            dataGridViewBarang.Columns.Clear();

            //menambah kolom didatagridview
            dataGridViewBarang.Columns.Add("KodeBarang", "Kode Barang");
            dataGridViewBarang.Columns.Add("Nama", "Nama Barang");
            dataGridViewBarang.Columns.Add("HargaJual", "Harga Jual");
            dataGridViewBarang.Columns.Add("Jumlah", "Jumlah");
            dataGridViewBarang.Columns.Add("SubTotal", "Sub Total");

            //agar lebar kolom dapat menyesuaikan panjang/isi data
            dataGridViewBarang.Columns["KodeBarang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Nama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["HargaJual"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["Jumlah"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewBarang.Columns["SubTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //agar harga jual dan stok rata kanan
            dataGridViewBarang.Columns["Jumlah"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewBarang.Columns["HargaJual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewBarang.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //agar harga jual ditampilkan dengan format pemisah ribuan (1000 delimeter)
            dataGridViewBarang.Columns["HargaJual"].DefaultCellStyle.Format = "0,###";
            dataGridViewBarang.Columns["SubTotal"].DefaultCellStyle.Format = "0,###";

            //agar user tidak menambahkan data langsung di datagridview (harus melalui tombol tambah)
            dataGridViewBarang.AllowUserToAddRows = false;
        }
        private void buttonHapus_Click(object sender, EventArgs e)
        {
            //pastikan dulu kepada user apakah akan menghapus data
            DialogResult konfirmasi = MessageBox.Show("Data nota jual akan terhapus. Apakah anda yakin ? ", "Konfirmasi", MessageBoxButtons.YesNo);

            if (konfirmasi == System.Windows.Forms.DialogResult.Yes)//jika user yakin ingin menghapus
            {
                //buat objek bertipe pelanggan
                Pelanggan pelanggan = new Pelanggan();
                //format comboboxpelanggan : x - yyyyyy (kode pelanggan karakter 0 sebanyak 1, nama kategori mulai karakter ke-4 s/d akhir)
                pelanggan.KodePelanggan = int.Parse(comboBoxPelanggan.Text.Substring(0, 1));//kode pelanggan diambil dari combobox
                pelanggan.Nama = comboBoxPelanggan.Text.Substring(4, comboBoxPelanggan.Text.Length - 4); //nama pelanggan diambil dari combobox
                pelanggan.Alamat = labelAlamat.Text;
                //buat objek bertipe pegawai
                Pegawai pegawai = new Pegawai();
                pegawai.KodePegawai = int.Parse(labelKodePeg.Text);
                pegawai.Nama = labelNamaPeg.Text;

                //buat objek bertipe notajual
                NotaJual nota = new NotaJual(textBoxNoNota.Text, dateTimePickerTanggal.Value, pelanggan, pegawai);
                string hasilTambah = NotaJual.HapusData(nota);
                if (hasilTambah == "1")
                {
                    MessageBox.Show("Nota Jual telah dihapus.", "Informasi");
                    FormHapusNotaJual_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Nota Jual telah dihapus.");
                    //MessageBox.Show("Gagal Menghapus Nota Jual.Pesan Kesalahan : " + hasilTambah);
                }
            }
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
