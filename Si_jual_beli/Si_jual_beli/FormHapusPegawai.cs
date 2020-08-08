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
    public partial class FormHapusPegawai : Form
    {
        public FormHapusPegawai()
        {
            InitializeComponent();
        }
        List<Pegawai> listHasilData = new List<Pegawai>();
        private void FormHapusPegawai_Load(object sender, EventArgs e)
        {

            textBoxKodePegawai.Focus();
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

            textBoxPassword.PasswordChar = '*';
            textBoxUPassword.PasswordChar = '*';

            
            textBoxNama.Enabled = false;
            dateTimePickerTanggalLahir.Enabled = false;
            textBoxGaji.Enabled = false;
            textBoxAlamat.Enabled = false;
            textBoxUsername.Enabled = false;
            textBoxPassword.Enabled = false;
            textBoxUPassword.Enabled = false;
            comboBoxJabatan.Enabled = false;
        }

        private void textBoxKodePegawai_TextChanged(object sender, EventArgs e)
        {
            comboBoxJabatan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
                        comboBoxJabatan.Items.Add(listHasilData[0].Jabatan.IdJabatan+ " - " + listHasilData[0].Jabatan.NamaJabatan);
                        comboBoxJabatan.SelectedIndex = comboBoxJabatan.Items.IndexOf(listHasilData[0].Jabatan.IdJabatan + " - " + listHasilData[0].Jabatan.NamaJabatan);
                        buttonHapus.Focus();
                        
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

        private void buttonHapus_Click(object sender, EventArgs e)
        {
            //pastikan dulu kepada user apakah akan menghapus data
            DialogResult konfirmasi = MessageBox.Show("Data kategori akan terhapus. Apakah anda yakin ? ", "Konfirmasi", MessageBoxButtons.YesNo);

            if (konfirmasi == System.Windows.Forms.DialogResult.Yes)//jika user yakin ingin menghapus
            {
                //ciptakan objek yang akan ditambahkan
                string kodeJabatan = comboBoxJabatan.Text.Substring(0, 2);
                string namaJabatan = comboBoxJabatan.Text.Substring(5, comboBoxJabatan.Text.Length - 8);
                Jabatan jabat = new Jabatan(kodeJabatan, namaJabatan);
                Pegawai peg = new Pegawai(int.Parse(textBoxKodePegawai.Text), textBoxNama.Text, dateTimePickerTanggalLahir.Value.Date, textBoxAlamat.Text, int.Parse(textBoxGaji.Text), textBoxUsername.Text, textBoxPassword.Text, jabat);

                //panggil static method HapusData di class Kategori
                string hasilTambah = Pegawai.HapusData(peg);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Barangg telah dihapus.", "Informasi");
                    FormHapusPegawai_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal Menghapus Kategori.Pesan Kesalahan : " + hasilTambah);
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
