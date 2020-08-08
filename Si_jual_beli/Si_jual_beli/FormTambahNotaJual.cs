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
    public partial class FormTambahNotaJual : Form
    {
        public FormTambahNotaJual()
        {
            InitializeComponent();
        }
        List<Barang> listDataBarang = new List<Barang>();
        List<Pelanggan> listDataPelanggan = new List<Pelanggan>();

        public void FormTambahNotaJual_Load(object sender, EventArgs e)
        {
            if (labelNamaBrg.Text == "service")
            {
                textBoxHarga.Enabled = true;
            }
            else 
            {
                textBoxHarga.Enabled = false;
            }
            //1. Generate nomor nota jual dan tampilkan ke textboxNoNota
            string hasilNoNota;
            string hasilGenerate = NotaJual.GenerateNoNota(out hasilNoNota);
            if (hasilGenerate == "1")
            {
                textBoxNoNota.Text = hasilNoNota;
                textBoxNoNota.Enabled = false;
            }
            else
            {
                MessageBox.Show("Generate nomor nota gagal dilakukan. pesan kesalahan = " + hasilGenerate);
            }

            //2. tanggal nota diisi default tanggal sistem
            dateTimePickerTanggal.Value = DateTime.Now;
            dateTimePickerTanggal.Enabled = false;

            //3. comboboxPelanggan diisi dengan semua pelanggan yang ada di tabel pelanggan (gunakan method bacadata di class pelanggan)
            comboBoxPelanggan.DropDownStyle = ComboBoxStyle.DropDownList;
            string hasilBacaPelanggan = Pelanggan.BacaData("","",listDataPelanggan);
            if (hasilBacaPelanggan == "1")
            {
                //kosongi dulu combobox
                comboBoxPelanggan.Items.Clear();
                //tambahkan data pelanggan ke comboboxPelanggan dengan format 'kode pelanggan - nama pelanggan'
                for (int i = 0; i < listDataPelanggan.Count; i++)
                {
                    comboBoxPelanggan.Items.Add(listDataPelanggan[i].KodePelanggan + " - " + listDataPelanggan[i].Nama);
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

        private void comboBoxPelanggan_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxPelanggan.DropDownStyle = ComboBoxStyle.DropDownList;
            //cari nama barang sesuai kode pelanggan yang dipilih user 
            string hasil = Pelanggan.BacaData("KodePelanggan", comboBoxPelanggan.Text.Substring(0,1), listDataPelanggan);
            if (hasil == "1")
            {
                if (listDataPelanggan.Count > 0)
                {
                    for (int i = 0; i < listDataPelanggan.Count; i++)
                    {
                        labelAlamat.Text = listDataPelanggan[i].Alamat;
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
            if (labelNamaBrg.Text == "service")
            {
                textBoxHarga.Enabled = true;
            }
            else
            {
                textBoxHarga.Enabled = false;
            }
            //jika user telah mengetik sesuai panjang karakter kodeBarang
                //cari nama barang sesuai barcode yang diinputkan user
                string hasil = Barang.BacaData("KodeBarang", textBoxBarcode.Text, listDataBarang);
                if (hasil == "1")
                {
                    if (listDataBarang.Count > 0) //jika barcode barang ditemukan di database
                    {
                        labelKode.Text = listDataBarang[0].KodeBarang;
                        labelNamaBrg.Text = listDataBarang[0].Nama;
                        textBoxHarga.Text = listDataBarang[0].HargaJual.ToString();
                        textBoxJumlah.Text = "1";
                        //textBoxJumlah.Focus();
                        
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
                int subTotal = int.Parse(textBoxHarga.Text) * int.Parse(textBoxJumlah.Text);

                //2. tambahkan ke datagridview
                dataGridViewBarang.Rows.Add(labelKode.Text, labelNamaBrg.Text, textBoxHarga.Text, textBoxJumlah.Text, subTotal);

                //3. hitung grandtotal nota dan tampilkan di labelgrandtotal
                //labelgrandtotal memiliki pemisah ribuan (delimiter ribuan)
                labelGrandTotal.Text = HitungGrandTotal().ToString("0,###");

                //4. kosongi barcode, nama barang, harga jual, dan jumlah
                textBoxBarcode.Text = "";
                labelKode.Text = "";
                labelNamaBrg.Text = "";
                textBoxHarga.Text = "";
                textBoxJumlah.Text = "";
                textBoxBarcode.Focus();
            }
        }

        public void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (dataGridViewBarang.Rows != null && dataGridViewBarang.Rows.Count != 0)
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
                    //data barang diperoleh dari datagridview
                    for (int i = 0; i < dataGridViewBarang.Rows.Count; i++)
                    {
                        Barang brg = new Barang();
                        brg.KodeBarang = dataGridViewBarang.Rows[i].Cells["KodeBarang"].Value.ToString();
                        brg.Nama = dataGridViewBarang.Rows[i].Cells["Nama"].Value.ToString();
                        int harga = int.Parse(dataGridViewBarang.Rows[i].Cells["HargaJual"].Value.ToString());
                        int jumlah = int.Parse(dataGridViewBarang.Rows[i].Cells["Jumlah"].Value.ToString());
                        NotaJualDetil notaDetil = new NotaJualDetil(brg, harga, jumlah);

                        //simpan detil barang ke nota
                        nota.TambahDetilBarang(brg, harga, jumlah);
                    }
                    string hasilTambah = NotaJual.TambahData(nota);
                    if (hasilTambah == "1")
                    {
                        MessageBox.Show("Data nota jual telah tersimpan dan tercetak", "Info");
                        cetak();
                        FormTambahNotaJual_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Data nota jual gagal tersimpan. Pesan kesalahan : " + hasilTambah, "Kesalahan");
                    }
            }
            else
            {
                MessageBox.Show("Isi nota jual terlebih dahulu");
            }
        }

        public void simpan()
        {
            
        }

        private void buttonCetak_Click(object sender, EventArgs e)
        {
            cetak();
        }

        public void cetak()
        {
            string hasilCetak = NotaJual.CetakNota("NoNota", textBoxNoNota.Text, "nota_jual.txt");

            if (hasilCetak == "1")
            {
                MessageBox.Show("Nota jual telah tercetak");
            }
            else
            {
                MessageBox.Show("Nota jual gagal dicetak. Pesan kesalahan : " + hasilCetak);
            }
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                simpan();
                //...
            }
            else if (result == DialogResult.No)
            {
                //...
            }
            else
            {
                //...
            }
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridViewBarang_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //
                //NotaJualDetil notaDetil = new NotaJualDetil(brg, harga, jumlah);

                //simpan detil barang ke nota
                ///nota.TambahDetilBarang(brg, harga, jumlah);
                ///
                int idx=dataGridViewBarang.CurrentRow.Index;
                String kodeBarang = dataGridViewBarang.Rows[idx].Cells[0].Value + "";
                String noNota = textBoxNoNota.Text;

                //MessageBox.Show(noNota+","+kodeBarang);
                //int idx=dataGridViewBarang.CurrentRow.Index;
                String msg=NotaJual.delete(noNota, kodeBarang) ;
                if (!msg.Equals(""))
                {
                    MessageBox.Show(msg);
                }
                dataGridViewBarang.Rows.RemoveAt(idx);

                labelGrandTotal.Text = HitungGrandTotal().ToString("0,###");
                

            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridViewBarang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxJumlah_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
    }
}
