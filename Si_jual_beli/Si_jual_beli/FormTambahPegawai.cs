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
    public partial class FormTambahPegawai : Form
    {
        public FormTambahPegawai()
        {
            InitializeComponent();
        }
        List<Jabatan> listDataJabatan = new List<Jabatan>();
        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKodePegawai.Text) && !string.IsNullOrEmpty(textBoxNama.Text) && !string.IsNullOrEmpty(dateTimePickerTanggalLahir.Text) && !string.IsNullOrEmpty(textBoxGaji.Text) && !string.IsNullOrEmpty(textBoxAlamat.Text) && !string.IsNullOrEmpty(textBoxUsername.Text) && !string.IsNullOrEmpty(textBoxPassword.Text) && !string.IsNullOrEmpty(textBoxUPassword.Text) && !string.IsNullOrEmpty(comboBoxJabatan.Text))
            {
                //simpan index kategori yang dipilih user di combobox
                int indexDipilihUser = comboBoxJabatan.SelectedIndex;
                //ciptakan objek kategori yang dipilih oleh user 
                //kategori barang diambil dari listKategori sesuai index yang bersesuaian dengan comboboxkategori
                Jabatan jabatanPeg = listDataJabatan[indexDipilihUser];

                //ciptakan objek pegawai
                Pegawai peg = new Pegawai(int.Parse(textBoxKodePegawai.Text), textBoxNama.Text, dateTimePickerTanggalLahir.Value.Date, textBoxAlamat.Text, int.Parse(textBoxGaji.Text), textBoxUsername.Text, textBoxPassword.Text, jabatanPeg);
                //panggil static method tambahdata di class pegawai
                //string hasilTambah = pegawai.tambahData(peg);

                FormUtama frmUtama = (FormUtama)this.Owner.MdiParent;

                string hasilTambah = Pegawai.TambahData(peg);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Pegawai telah tersimpan", "informasi");
                    // jalankan form load
                    FormTambahPegawai_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("gagal menambah pegawai . Pesan kesalahan : " + hasilTambah);
                }
            }
            else
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu");
            }
        }

        private void FormTambahPegawai_Load(object sender, EventArgs e)
        {
            textBoxKodePegawai.Enabled = false;
            textBoxNama.Text = "";
            textBoxUsername.Text = "";
            textBoxAlamat.Text = "";
            dateTimePickerTanggalLahir.Value = DateTime.Now;
            textBoxPassword.Text = "";
            textBoxUPassword.Text = "";

            textBoxKodePegawai.MaxLength = 45;
            textBoxUsername.MaxLength = 8;
            textBoxPassword.MaxLength = 8;
            textBoxUPassword.MaxLength = 8;
            textBoxGaji.Text = "0";
            textBoxGaji.TextAlign = HorizontalAlignment.Right;
            textBoxKodePegawai.Focus();

            //agar password ditampilkan dengan karakter (*)
            textBoxPassword.PasswordChar = '*';
            textBoxUPassword.PasswordChar = '*';

            //tampilkan semua jabatan yang ada ditabel jabatan (gunakan method bacadata diclass jabatan)
            string hasilBaca = Jabatan.BacaData("","",listDataJabatan);
            if (hasilBaca == "1")
            {//jika sukses membaca
                comboBoxJabatan.Items.Clear();//kosongi isi combobox
                for (int i = 0; i < listDataJabatan.Count; i++)
                {
                    //tampilkan dengan format id jabatan - nama jabatan, contoh : 32 - Kasir
                    comboBoxJabatan.Items.Add(listDataJabatan[i].IdJabatan + " - " + listDataJabatan[i].NamaJabatan);
                }
            }
            else {
                MessageBox.Show("data jabatan gagal ditampilkan. Pesan Kesalahan : " + hasilBaca);
            }

            //generate kode pegawai terbaru (kode pegawai bertipe int)
            int kodeTerbaru;
            string hasilGenerate = Pegawai.GenerateKode(out kodeTerbaru);

            if (hasilGenerate == "1")
            {
                textBoxKodePegawai.Text = kodeTerbaru.ToString();

                //arahkan cursor ke textboxNama
                textBoxNama.Focus();
            }
            else {
                MessageBox.Show("gagal melakukan generate kode. Pesan Kesalahan : " + hasilGenerate);
            }
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxKodePegawai.Enabled = false;
            textBoxNama.Text = "";
            textBoxUsername.Text = "";
            textBoxAlamat.Text = "";
            dateTimePickerTanggalLahir.Value = DateTime.Now;
            textBoxPassword.Text = "";
            textBoxUPassword.Text = "";
            textBoxKodePegawai.Focus();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            FormDaftarPegawai frmDaftar = (FormDaftarPegawai)this.Owner;
            frmDaftar.FormDaftarPegawai_Load(sender, e);
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBoxKodePegawai_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
