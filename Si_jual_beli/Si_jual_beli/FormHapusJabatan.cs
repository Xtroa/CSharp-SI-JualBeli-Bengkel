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
    public partial class FormHapusJabatan : Form
    {
        public FormHapusJabatan()
        {
            InitializeComponent();
        }
        List<Jabatan> listHasilData = new List<Jabatan>();
        private void buttonHapus_Click(object sender, EventArgs e)
        {
            //pastikan dulu kepada user apakah akan menghapus data
            DialogResult konfirmasi = MessageBox.Show("Data kategori akan terhapus. Apakah anda yakin ? ", "Konfirmasi", MessageBoxButtons.YesNo);

            if (konfirmasi == System.Windows.Forms.DialogResult.Yes)//jika user yakin ingin menghapus
            {
                //ciptakan objek yang akan ditambahkan
                Jabatan jb = new Jabatan(textBoxKode.Text, textBoxNama.Text);

                //panggil static method HapusData di class Kategori
                string hasilTambah = Jabatan.HapusData(jb);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Jabatan telah dihapus.", "Informasi");
                    FormHapusJabatan_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal Menghapus jabatan.Pesan Kesalahan : " + hasilTambah);
                }
            }
        }

        private void textBoxKode_TextChanged(object sender, EventArgs e)
        {
            //jika user telah mengetik sesuai panjang karakter kodeKategori
            if (textBoxKode.Text.Length == textBoxKode.MaxLength)
            {
                listHasilData.Clear();

                string hasilBaca = Jabatan.BacaData("IdJabatan", textBoxKode.Text, listHasilData);
                if (hasilBaca == "1")
                {
                    if (listHasilData.Count() > 0)
                    {
                        textBoxNama.Text = listHasilData[0].NamaJabatan;
                        buttonHapus.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Id Jabatan tidak ditemukan. Proses Hapus Data tidak bisa dilakukan.");
                        textBoxNama.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Perintah SQL gagal dijalankan.Pesan kesalahan = " + hasilBaca);
                }
            }
        }

        private void FormHapusJabatan_Load(object sender, EventArgs e)
        {
            textBoxKode.MaxLength = 2;
            textBoxNama.Enabled = false;
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxKode.Text = "";
            textBoxNama.Text = "";
            textBoxKode.Focus();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            FormDaftarJabatan frmDaftar = (FormDaftarJabatan)this.Owner;
            frmDaftar.FormDaftarJabatan_Load(sender, e);
            this.Close();
        }
    }
}
