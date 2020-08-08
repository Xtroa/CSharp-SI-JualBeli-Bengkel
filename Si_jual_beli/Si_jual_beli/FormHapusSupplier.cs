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
    public partial class FormHapusSupplier : Form
    {
        public FormHapusSupplier()
        {
            InitializeComponent();
        }
        List<Supplier> listHasilData = new List<Supplier>();
        private void buttonHapus_Click(object sender, EventArgs e)
        {
            //pastikan dulu kepada user apakah akan menghapus data
            DialogResult konfirmasi = MessageBox.Show("Data Supplier akan terhapus. Apakah anda yakin ? ", "Konfirmasi", MessageBoxButtons.YesNo);

            if (konfirmasi == System.Windows.Forms.DialogResult.Yes)//jika user yakin ingin menghapus
            {
                //ciptakan objek yang akan ditambahkan
                Supplier sup = new Supplier(int.Parse(textBoxKode.Text), textBoxNama.Text, textBoxAlamat.Text);

                //panggil static method HapusData di class Kategori
                string hasilTambah = Supplier.HapusData(sup);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Supplier telah dihapus.", "Informasi");
                    FormHapusSupplier_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal Menghapus Supplier.Pesan Kesalahan : " + hasilTambah);
                }
            }
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

        private void FormHapusSupplier_Load(object sender, EventArgs e)
        {
            textBoxKode.MaxLength = 1;

            textBoxAlamat.Enabled = false;
            textBoxNama.Enabled = false;
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
                        buttonHapus.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Kode Supplier tidak ditemukan. Proses Hapus Data tidak bisa dilakukan.");
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
