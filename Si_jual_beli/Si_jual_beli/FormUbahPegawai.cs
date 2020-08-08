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
    public partial class FormUbahPegawai : Form
    {
        public FormUbahPegawai()
        {
            InitializeComponent();
        }
        List<Jabatan> listDataJabatan = new List<Jabatan>();
        List<Pegawai> listHasilData = new List<Pegawai>();
        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKodePegawai.Text) && !string.IsNullOrEmpty(textBoxNama.Text) && !string.IsNullOrEmpty(dateTimePickerTanggalLahir.Text) && !string.IsNullOrEmpty(textBoxGaji.Text) && !string.IsNullOrEmpty(textBoxAlamat.Text) && !string.IsNullOrEmpty(textBoxUsername.Text) && !string.IsNullOrEmpty(textBoxPassword.Text) && !string.IsNullOrEmpty(textBoxUPassword.Text) && !string.IsNullOrEmpty(comboBoxJabatan.Text))
            {
                string IdJabatan = comboBoxJabatan.Text.Substring(0, 2);
                string namaJabatan = comboBoxJabatan.Text.Substring(5, comboBoxJabatan.Text.Length - 8);
                Jabatan jabatanPeg = new Jabatan(IdJabatan, namaJabatan);
                Pegawai peg = new Pegawai(int.Parse(textBoxKodePegawai.Text), textBoxNama.Text, dateTimePickerTanggalLahir.Value.Date, textBoxAlamat.Text, int.Parse(textBoxGaji.Text), textBoxUsername.Text, textBoxPassword.Text, jabatanPeg);

                //panggil static method UbahData di class Kategori
                string hasilTambah = Pegawai.UbahData(peg);

                FormUtama frmUtama = (FormUtama)this.Owner.MdiParent;

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Pegawai telah tersimpan", "informasi");
                    FormUbahPegawai_Load(sender, e);
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

        private void FormUbahPegawai_Load(object sender, EventArgs e)
        {
            textBoxKodePegawai.Enabled = true;
            textBoxNama.Text = "";
            textBoxUsername.Text = "";
            textBoxAlamat.Text = "";
            dateTimePickerTanggalLahir.Value = DateTime.Now;
            textBoxPassword.Text = "";
            textBoxUPassword.Text = "";

            textBoxKodePegawai.MaxLength = 1;
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
            string hasilBaca = Jabatan.BacaData("", "", listDataJabatan);
            if (hasilBaca == "1")
            {//jika sukses membaca
                comboBoxJabatan.Items.Clear();//kosongi isi combobox
                for (int i = 0; i < listDataJabatan.Count; i++)
                {
                    //tampilkan dengan format id jabatan - nama jabatan, contoh : 32 - Kasir
                    comboBoxJabatan.Items.Add(listDataJabatan[i].IdJabatan + " - " + listDataJabatan[i].NamaJabatan);
                }
            }
            else
            {
                MessageBox.Show("data jabatan gagal ditampilkan. Pesan Kesalahan : " + hasilBaca);
            }
        }

        private void textBoxKodePegawai_TextChanged(object sender, EventArgs e)
        {
            if (textBoxKodePegawai.Text.Length == textBoxKodePegawai.MaxLength)
            {
                string hasilBaca = Pegawai.BacaData("KodePegawai", textBoxKodePegawai.Text, listHasilData);
                if (hasilBaca == "1")
                {
                    if (listHasilData.Count() > 0)
                    {
                        textBoxNama.Text = listHasilData[0].Nama;
                        dateTimePickerTanggalLahir.Value = listHasilData[0].TglLahir;
                        textBoxAlamat.Text = listHasilData[0].Alamat;
                        textBoxGaji.Text = listHasilData[0].Gaji.ToString();
                        textBoxUsername.Text = listHasilData[0].Username;
                        textBoxPassword.Text = listHasilData[0].Password;
                        textBoxUPassword.Text = listHasilData[0].Password;
                        comboBoxJabatan.Items.Add(listHasilData[0].Jabatan.IdJabatan + " - " + listHasilData[0].Jabatan.NamaJabatan);
                        comboBoxJabatan.SelectedIndex = comboBoxJabatan.Items.IndexOf(listHasilData[0].Jabatan.IdJabatan + " - " + listHasilData[0].Jabatan.NamaJabatan);
                        textBoxNama.Focus();
                        textBoxUsername.Enabled = false;
                        textBoxKodePegawai.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Kode Pegawai tidak ditemukan. Proses Ubah Data tidak bisa dilakukan.");
                        textBoxNama.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Perintah SQL gagal dijalankan.Pesan kesalahan = " + hasilBaca);
                }
            }
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxKodePegawai.Enabled = true;
            textBoxUsername.Enabled = true;
            textBoxKodePegawai.Text = "";
            textBoxGaji.Text = "";
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
    }
}
