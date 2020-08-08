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
    public partial class FormTambahSupplier : Form
    {
        public FormTambahSupplier()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKode.Text) && !string.IsNullOrEmpty(textBoxNama.Text) && !string.IsNullOrEmpty(textBoxAlamat.Text))
            {
                //ciptakan objek yang akan ditambahkan
                Supplier sup = new Supplier(int.Parse(textBoxKode.Text), textBoxNama.Text, textBoxAlamat.Text);
                //panggil static method tambahdata di class kategori
                string hasilTambah = Supplier.TambahData(sup);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Supplier telah tersimpan. ", "informasi");
                    FormTambahSupplier_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal menambah kategori. pesan kesalahan : " + hasilTambah);
                }
            }
            else
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu");
            }
            
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
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

        private void FormTambahSupplier_Load(object sender, EventArgs e)
        {
            int kodeTerbaru;
            string hasilGenerate = Supplier.GenerateKode(out kodeTerbaru);

            if (hasilGenerate == "1")
            {
                textBoxKode.Text = kodeTerbaru.ToString();
                textBoxKode.Enabled = false;
                //arahkan cursor ke textboxNama
                textBoxNama.Focus();
            }
            else
            {
                MessageBox.Show("gagal melakukan generate kode. Pesan Kesalahan : " + hasilGenerate);
            }
        }
    }
}
