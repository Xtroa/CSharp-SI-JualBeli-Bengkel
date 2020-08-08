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
    public partial class FormUbahNotaBeli : Form
    {
        public FormUbahNotaBeli()
        {
            InitializeComponent();
        }
        List<Barang> listDataBarang = new List<Barang>();
        List<Supplier> listDataSupplier = new List<Supplier>();
        List<NotaBeli> listDataNotaBeli = new List<NotaBeli>();
        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonUbah_Click(object sender, EventArgs e)
        {
            //buat objek bertipe pelanggan
            Supplier supplier = new Supplier();
            //format comboboxpelanggan : x - yyyyyy (kode pelanggan karakter 0 sebanyak 1, nama kategori mulai karakter ke-4 s/d akhir)
            supplier.KodeSupplier = int.Parse(comboBoxPelanggan.Text.Substring(0, 1));//kode pelanggan diambil dari combobox
            supplier.NamaSupplier = comboBoxPelanggan.Text.Substring(4, comboBoxPelanggan.Text.Length - 4); //nama pelanggan diambil dari combobox
            supplier.Alamat = labelAlamat.Text;
            //buat objek bertipe pegawai
            Pegawai pegawai = new Pegawai();
            pegawai.KodePegawai = int.Parse(labelKodePeg.Text);
            pegawai.Nama = labelNamaPeg.Text;

            //buat objek bertipe notajual
            NotaBeli nota = new NotaBeli(textBoxNoNota.Text, dateTimePickerTanggal.Value, supplier, pegawai);
            //data barang diperoleh dari datagridview
            for (int i = 0; i < dataGridViewBarang.Rows.Count; i++)
            {
                Barang brg = new Barang();
                brg.KodeBarang = dataGridViewBarang.Rows[i].Cells["KodeBarang"].Value.ToString();
                brg.Nama = dataGridViewBarang.Rows[i].Cells["Nama"].Value.ToString();
                int harga = int.Parse(dataGridViewBarang.Rows[i].Cells["HargaJual"].Value.ToString());
                int jumlah = int.Parse(dataGridViewBarang.Rows[i].Cells["Jumlah"].Value.ToString());
                NotaBeliDetil notaDetil = new NotaBeliDetil(brg, harga, jumlah);

                //simpan detil barang ke nota
                nota.TambahDetilBarang(brg, harga, jumlah);
            }
            string hasilTambah = NotaBeli.UbahData(nota);
            if (hasilTambah == "1")
            {
                MessageBox.Show("Data nota jual telah tersimpan", "Info");
                buttonCetak_Click(sender, e);
                FormUbahNotaBeli_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Data nota jual gagal tersimpan. Pesan kesalahan : " + hasilTambah, "Kesalahan");
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
        private void textBoxJumlah_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //jika user menekan tombol enter
            {
                //1. hitung sub total
                int subTotal = int.Parse(labelHarga.Text) * int.Parse(textBoxJumlah.Text);

                //2. tambahkan ke datagridview
                dataGridViewBarang.Rows.Add(labelKode.Text, labelNamaBrg.Text, labelHarga.Text, textBoxJumlah.Text, subTotal);

                //3. hitung grandtotal nota dan tampilkan di labelgrandtotal
                //labelgrandtotal memiliki pemisah ribuan (delimiter ribuan)
                labelGrandTotal.Text = HitungGrandTotal().ToString("0,###");

                //4. kosongi barcode, nama barang, harga jual, dan jumlah
                textBoxBarcode.Text = "";
                labelKode.Text = "";
                labelNamaBrg.Text = "";
                labelHarga.Text = "";
                textBoxJumlah.Text = "";
                textBoxBarcode.Focus();
            }
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
        private void textBoxNoNota_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNoNota.Text.Length == textBoxNoNota.MaxLength)
            {
                string hasilBaca = NotaBeli.BacaData("NoNota", textBoxNoNota.Text, listDataNotaBeli);
                if (hasilBaca == "1")
                {
                    if (listDataNotaBeli.Count() > 0)
                    {
                        dateTimePickerTanggal.Value = listDataNotaBeli[0].Tanggal;
                        comboBoxPelanggan.SelectedIndex = comboBoxPelanggan.Items.IndexOf(listDataNotaBeli[0].Supplier.KodeSupplier + " - " + listDataNotaBeli[0].Supplier.NamaSupplier);
                        labelAlamat.Text = listDataNotaBeli[0].Supplier.Alamat;
                        textBoxBarcode.Focus();
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

        private void FormUbahNotaBeli_Load(object sender, EventArgs e)
        {
            //2. tanggal nota diisi default tanggal sistem
            dateTimePickerTanggal.Value = DateTime.Now;
            dateTimePickerTanggal.Enabled = true;

            //3. comboboxPelanggan diisi dengan semua pelanggan yang ada di tabel pelanggan (gunakan method bacadata di class pelanggan)
            comboBoxPelanggan.DropDownStyle = ComboBoxStyle.DropDownList;
            string hasilBacaPelanggan = Supplier.BacaData("", "", listDataSupplier);
            if (hasilBacaPelanggan == "1")
            {
                //kosongi dulu combobox
                comboBoxPelanggan.Items.Clear();
                //tambahkan data pelanggan ke comboboxPelanggan dengan format 'kode pelanggan - nama pelanggan'
                for (int i = 0; i < listDataSupplier.Count; i++)
                {
                    comboBoxPelanggan.Items.Add(listDataSupplier[i].KodeSupplier + " - " + listDataSupplier[i].NamaSupplier);
                }
                //agar default dari pelanggan nota adalah pelanggan pertama (pelanggan umum)
                comboBoxPelanggan.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Data pelanggan gagal ditampilkan di combobox. Pesan kesalahan : " + hasilBacaPelanggan);
            }

            //4. kode dan nama pegawai pembuat nota yang sedang login diambil dari label di formUtama
            FormUtama frmUtama = (FormUtama)this.Owner.MdiParent;
            labelKodePeg.Text = frmUtama.labelKodePeg.Text;
            labelNamaPeg.Text = frmUtama.labelNamaPeg.Text;

            //5. tambahkan kolom di datagridviewbarang
            FormatDataGrid();

            //6. agar barcode hanya bisa diisi max, 13 karakter
            textBoxBarcode.MaxLength = 13;
            textBoxNoNota.MaxLength = 11;
        }

        private void comboBoxPelanggan_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxPelanggan.DropDownStyle = ComboBoxStyle.DropDownList;
            //cari nama barang sesuai kode pelanggan yang dipilih user 
            string hasil = Supplier.BacaData("KodeSupplier", comboBoxPelanggan.Text.Substring(0, 1), listDataSupplier);
            if (hasil == "1")
            {
                if (listDataSupplier.Count > 0)
                {
                    for (int i = 0; i < listDataSupplier.Count; i++)
                    {
                        labelAlamat.Text = listDataSupplier[i].Alamat;
                    }
                }
                else
                {
                    MessageBox.Show("Kode pelanggan tidak ditemukan.");
                    labelAlamat.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Perintah SQL gagal dijalankan. Pesan kesalahan : " + hasil);
            }
        }

        private void textBoxBarcode_TextChanged(object sender, EventArgs e)
        {
            //jika user telah mengetik sesuai panjang karakter kodeBarang
            if (textBoxBarcode.Text.Length == textBoxBarcode.MaxLength)
            {
                //cari nama barang sesuai barcode yang diinputkan user
                string hasil = Barang.BacaData("Barcode", textBoxBarcode.Text, listDataBarang);
                if (hasil == "1")
                {
                    if (listDataBarang.Count > 0) //jika barcode barang ditemukan di database
                    {
                        labelKode.Text = listDataBarang[0].KodeBarang;
                        labelNamaBrg.Text = listDataBarang[0].Nama;
                        labelHarga.Text = listDataBarang[0].HargaJual.ToString();
                        textBoxJumlah.Text = "1";
                        textBoxJumlah.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Barang tidak ditemukan.");
                        textBoxBarcode.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Perintah SQL gagal dijalankan. Pesan kesalahan = " + hasil);
                }
            }

        }

        private void buttonCetak_Click(object sender, EventArgs e)
        {
            string hasilCetak = NotaBeli.CetakNota("NoNota", textBoxNoNota.Text, "nota_Beli.txt");

            if (hasilCetak == "1")
            {
                MessageBox.Show("Nota jual telah tercetak");
            }
            else
            {
                MessageBox.Show("Nota jual gagal dicetak. Pesan kesalahan : " + hasilCetak);
            }
        }
    }
}
