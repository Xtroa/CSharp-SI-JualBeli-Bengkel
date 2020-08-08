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
    public partial class FormUbahJabatan : Form
    {
        public FormUbahJabatan()
        {
            InitializeComponent();
        }
        List<Jabatan> listHasilData = new List<Jabatan>();
        private void FormUbahJabatan_Load(object sender, EventArgs e)
        {
            textBoxKode.MaxLength = 2;
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKode.Text) && !string.IsNullOrEmpty(textBoxNama.Text))
            {
                //ciptakan objek yg akan ditambahkan
                Jabatan jb = new Jabatan(textBoxKode.Text, textBoxNama.Text);

                //panggil static method UbahData di class Kategori
                string hasilTambah = Jabatan.UbahData(jb);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Jabatan telah diubah.", "Informasi");
                    FormUbahJabatan_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal Mengubah jabatan. Pesan Kesalahan : " + hasilTambah);
                }
            }
            else
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu");
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
                        textBoxNama.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Id Jabatan tidak ditemukan. Proses Ubah Data tidak bisa dilakukan.");
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
