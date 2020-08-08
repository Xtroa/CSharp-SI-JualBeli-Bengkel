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
    public partial class FormTambahJabatan : Form
    {
        public FormTambahJabatan()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKode.Text) && !string.IsNullOrEmpty(textBoxNama.Text))
            {
                string id = textBoxKode.Text;
                string nam = textBoxNama.Text;
                //ciptakan objek yang akan ditambahkan
                Jabatan jb = new Jabatan(id, nam);

                //panggil static method tambahdata di class kategori
                string hasilTambah = Jabatan.TambahData(jb);

                if (hasilTambah == "1")
                {
                    MessageBox.Show("Jabatan telah tersimpan. ", "informasi");
                    FormTambahJabatan_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Gagal menambah jabatan. pesan kesalahan : " + hasilTambah);
                }
            }
            else
            {
                MessageBox.Show("Lengkapi Data Terlebih Dahulu");
                
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

        private void FormTambahJabatan_Load(object sender, EventArgs e)
        {

        }
    }
}
