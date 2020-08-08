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
    public partial class FormUbahSupplier : Form
    {
        public FormUbahSupplier()
        {
            InitializeComponent();
        }
        List<Supplier> listHasilData = new List<Supplier>();
        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKode.Text) && !string.IsNullOrEmpty(textBoxNama.Text) && !string.IsNullOrEmpty(textBoxAlamat.Text))
            {
                //ciptakan objek yg akan ditambahkan
                Supplier sup = new Supplier(int.Parse(textBoxKode.Text), textBoxNama.Text, textBoxAlamat.Text);

                //panggil static method UbahData di class Kategori
                string hasilTambah = Supplier.UbahData(sup);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Pelanggan telah diubah.", "Informasi");
                    FormUbahSupplier_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal Mengubah Pelanggan. Pesan Kesalahan : " + hasilTambah);
                }
            }
            else
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu");
            }
        }

        private void FormUbahSupplier_Load(object sender, EventArgs e)
        {
            textBoxKode.MaxLength = 2;
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxKode.Text = "";
            textBoxNama.Text = "";
            textBoxAlamat.Text = "";
            textBoxKode.Focus();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            FormDaftarSupplier frmDaftar = (FormDaftarSupplier)this.Owner;
            frmDaftar.FormDaftarSupplier_Load(sender, e);
            this.Close();
        }

        private void textBoxKode_TextChanged(object sender, EventArgs e)
        {
            //jika user telah mengetik sesuai panjang karakter kodeKategori
            if (textBoxKode.Text.Length == textBoxKode.MaxLength)
            {
                listHasilData.Clear();

                string hasilBaca = Supplier.BacaData("KodeSupplier", textBoxKode.Text, listHasilData);
                if (hasilBaca == "1")
                {
                    if (listHasilData.Count() > 0)
                    {
                        textBoxNama.Text = listHasilData[0].NamaSupplier;
                        textBoxAlamat.Text = listHasilData[0].Alamat;
                        textBoxNama.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Kode Pelanggan tidak ditemukan. Proses Ubah Data tidak bisa dilakukan.");
                        textBoxNama.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Perintah SQL gagal dijalankan.Pesan kesalahan = " + hasilBaca);
                }
            }
        }
    }
}
