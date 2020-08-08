using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PenjualanPembelian_LIB;
using System.IO;
namespace Si_jual_beli
{
    public partial class FormUtama : Form
    {
        public FormUtama()
        {
            InitializeComponent();
        }
        public void FormUtama_Load(object sender, EventArgs e)
        {
            //ubah form ini menjadi fullscreen
            this.WindowState = FormWindowState.Maximized;

            //ubah form ini menjaid MdiParent
            this.IsMdiContainer = true;

            //agar formUtama tidak bisa diakses sebelum proses login dilakukan
            this.Enabled = false;

            //tampilkan formlogin terlebih dahulu sebelum bisa mengakses sistem
            FormLogin frmlogin = new FormLogin();
            frmlogin.Owner = this;//FormLogin bukan MdiChild dari FormUtama 
            frmlogin.Show();

        }

        private void kategoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //buka form kategori barang
            Form form = Application.OpenForms["FormKategoriBarang"];

            if(form == null) //jika form ini belum pernah di-create sebelumnya
            {
                FormKategoriBarang frmKategori = new FormKategoriBarang(); //create objek formdaftarkategori
                frmKategori.MdiParent = this; //set form utama menjadi parent dari objek form yang dibuat
                frmKategori.Show(); //tampilkan form
            }
            else
            {
                form.Show();
                form.BringToFront();//agar form tampil di paling depan
            }
        }

        private void pelangganToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //buka form kategori barang
            Form form = Application.OpenForms["FormDaftarPelanggan"];

            if (form == null) //jika form ini belum pernah di-create sebelumnya
            {
                FormDaftarPelanggan frmDftPlanggan = new FormDaftarPelanggan(); //create objek formdaftarkategori
                frmDftPlanggan.MdiParent = this; //set form utama menjadi parent dari objek form yang dibuat
                frmDftPlanggan.Show(); //tampilkan form
            }
            else
            {
                form.Show();
                form.BringToFront();//agar form tampil di paling depan
            }
        }

        private void pegawaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //buka form kategori barang
            Form form = Application.OpenForms["FormDaftarPegawai"];

            if (form == null) //jika form ini belum pernah di-create sebelumnya
            {
                FormDaftarPegawai frmDftPgawai = new FormDaftarPegawai(); //create objek formdaftarkategori
                frmDftPgawai.MdiParent = this; //set form utama menjadi parent dari objek form yang dibuat
                frmDftPgawai.Show(); //tampilkan form
            }
            else
            {
                form.Show();
                form.BringToFront();//agar form tampil di paling depan
            }
        }

        private void barangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //buka form kategori barang
            Form form = Application.OpenForms["FormDaftarBarang"];

            if (form == null) //jika form ini belum pernah di-create sebelumnya
            {
                FormDaftarBarang frmDftBarang = new FormDaftarBarang(); //create objek formdaftarkategori
                frmDftBarang.MdiParent = this; //set form utama menjadi parent dari objek form yang dibuat
                frmDftBarang.Show(); //tampilkan form
            }
            else
            {
                form.Show();
                form.BringToFront();//agar form tampil di paling depan
            }
        }

        private void penjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //buka form kategori barang
            Form form = Application.OpenForms["FormNotaJual"];

            if (form == null) //jika form ini belum pernah di-create sebelumnya
            {
                FormNotaJual frmNtJual = new FormNotaJual(); //create objek formdaftarkategori
                frmNtJual.MdiParent = this; //set form utama menjadi parent dari objek form yang dibuat
                frmNtJual.Show(); //tampilkan form
            }
            else
            {
                form.Show();
                form.BringToFront();//agar form tampil di paling depan
            }
        }

        private void pembelianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //buka form kategori barang
            Form form = Application.OpenForms["FormNotaBeli"];

            if (form == null) //jika form ini belum pernah di-create sebelumnya
            {
                FormNotaBeli frmNtBeli = new FormNotaBeli(); //create objek formdaftarkategori
                frmNtBeli.MdiParent = this; //set form utama menjadi parent dari objek form yang dibuat
                frmNtBeli.Show(); //tampilkan form
            }
            else
            {
                form.Show();
                form.BringToFront();//agar form tampil di paling depan
            }
        }

        private void jabatanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //buka form kategori barang
            Form form = Application.OpenForms["FormDaftarJabatan"];

            if (form == null) //jika form ini belum pernah di-create sebelumnya
            {
                FormDaftarJabatan frmdftarjbtan = new FormDaftarJabatan(); //create objek formdaftarkategori
                frmdftarjbtan.MdiParent = this; //set form utama menjadi parent dari objek form yang dibuat
                frmdftarjbtan.Show(); //tampilkan form
            }
            else
            {
                form.Show();
                form.BringToFront();//agar form tampil di paling depan
            }
        }
        public void PengaturanHakAksesMenu(Jabatan pJabatan) {
            if (pJabatan.IdJabatan == "J1") {//jika jabatannya pegawai pembelian
                masterToolStripMenuItem.Visible = false;
                penjualanToolStripMenuItem.Visible = true;
                pembelianToolStripMenuItem.Visible = true;
                laporanMasterToolStripMenuItem.Visible = true;
            }
            else if (pJabatan.IdJabatan == "J2")//jika jabatannya kasir
            {
                masterToolStripMenuItem.Visible = false;
                penjualanToolStripMenuItem.Visible = true;
                pembelianToolStripMenuItem.Visible = true;
                laporanMasterToolStripMenuItem.Visible = true;
            }
            else if (pJabatan.IdJabatan == "J3")//jika jabatannya manager
            {
                masterToolStripMenuItem.Visible = true;
                penjualanToolStripMenuItem.Visible = true;
                pembelianToolStripMenuItem.Visible = true;
                laporanMasterToolStripMenuItem.Visible = true;
            }
        }

        private void suppplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //buka form kategori barang
            Form form = Application.OpenForms["FormDaftarSupplier"];

            if (form == null) //jika form ini belum pernah di-create sebelumnya
            {
                FormDaftarSupplier frmdftarSupplier = new FormDaftarSupplier(); //create objek formdaftarkategori
                frmdftarSupplier.MdiParent = this; //set form utama menjadi parent dari objek form yang dibuat
                frmdftarSupplier.Show(); //tampilkan form
            }
            else
            {
                form.Show();
                form.BringToFront();//agar form tampil di paling depan
            }
        }

       
        private void createBarang()
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            String appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            String nama = appPath + "\\" + milliseconds + ".xlsx";
            Pegawai pw = new Pegawai();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel File|*.xlsx";
            saveFileDialog1.Title = "Save an Excel File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                pw.printBarang(nama);

                //String fnam = saveFileDialog1.FileName;
                File.Copy(nama, saveFileDialog1.FileName, true);
            }
            
        }
        private void createKategori()
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            String appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            String nama = appPath + "\\" + milliseconds + ".xlsx";
            Pegawai pw = new Pegawai();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel File|*.xlsx";
            saveFileDialog1.Title = "Save an Excel File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                pw.printKategori(nama);
                File.Copy(nama, saveFileDialog1.FileName, true);
            }
        }
        private void createPelanggan()
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            String appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            String nama = appPath + "\\" + milliseconds + ".xlsx";
            Pegawai pw = new Pegawai();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel File|*.xlsx";
            saveFileDialog1.Title = "Save an Excel File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                pw.printPelanggan(nama);
                File.Copy(nama, saveFileDialog1.FileName, true);
            }
        }
        private void createSuplier()
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            String appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            String nama = appPath + "\\" + milliseconds + ".xlsx";
            Pegawai pw = new Pegawai();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel File|*.xlsx";
            saveFileDialog1.Title = "Save an Excel File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                pw.printSuplier(nama);
                File.Copy(nama, saveFileDialog1.FileName, true);
            }
        }
        private void createPegawai()
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            String appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            String nama = appPath + "\\" + milliseconds + ".xlsx";
            Pegawai pw = new Pegawai();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel File|*.xlsx";
            saveFileDialog1.Title = "Save an Excel File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                pw.printPegawai(nama);
                File.Copy(nama, saveFileDialog1.FileName, true);
            }
            
        }
        private void barangToolStripMenuItem1_Click(object sender, EventArgs e)
        {


            DialogResult result = MessageBox.Show("Apakah anda ingin melakukan simpan data??", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                createBarang();
            }
            //string hasilCetak = Barang.CetakNota("", "", "daftar_barang.txt");
            
            //MessageBox.Show("Data telah tercetak");
        }


        private void laporanPenjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda ingin melakukan simpan data?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string hasilCetak = NotaJual.CetakNota("", "", "daftar_nota_jual.txt");
                MessageBox.Show("Data telah tercetak");
            }
         
        }

        private void laporanPembelianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda ingin melakukan simpan data?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string hasilCetak = NotaBeli.CetakNota("", "", "daftar_nota_beli.txt");
                MessageBox.Show("Data telah tercetak");
            }
        }

        private void kategoriToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda ingin melakukan simpan data?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                createKategori();
                //string hasilCetak = Kategori.CetakNota("", "", "daftar_Kategori.txt");
                //MessageBox.Show("Data telah tercetak");
            }
            
        }

        private void pelangganToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda ingin melakukan simpan data?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                createPelanggan();
                //string hasilCetak = Pelanggan.CetakNota("", "", "daftar_Pelanggan.txt");
                //MessageBox.Show("Data telah tercetak");
            }
      
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda ingin melakukan simpan data?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                createSuplier();
                //string hasilCetak = Supplier.CetakNota("", "", "daftar_Supplier.txt");
                //MessageBox.Show("Data telah tercetak");
            }
        
        }

        private void pegawaiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda ingin melakukan simpan data?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                createPegawai();
                //string hasilCetak = Pegawai.CetakNota("", "", "daftar_Pegawai.txt");
                //MessageBox.Show("Data telah tercetak");
            }
       
        }

        private void laporanBulanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormLaporanPembelian beli = new FormLaporanPembelian();
            //beli.ShowDialog();

            //buka form laporan pembelian
            Form form = Application.OpenForms["FormLaporanPembelian"];

            if (form == null) //jika form ini belum pernah di-create sebelumnya
            {
                FormLaporanPembelian frmLpPembelian = new FormLaporanPembelian(); //create objek formdaftarkategori
                frmLpPembelian.MdiParent = this; //set form utama menjadi parent dari objek form yang dibuat
                frmLpPembelian.Show(); //tampilkan form
            }
            else
            {
                form.Show();
                form.BringToFront();//agar form tampil di paling depan
            }
        }

        private void laporanBeliBulanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormLaporanPenjualan masuk = new FormLaporanPenjualan();
            //masuk.ShowDialog();

            //buka form laporan penjualan
            Form form = Application.OpenForms["FormLaporanPenjualan"];

            if (form == null) //jika form ini belum pernah di-create sebelumnya
            {
                FormLaporanPenjualan frmLpPenjualan = new FormLaporanPenjualan(); //create objek formdaftarkategori
                frmLpPenjualan.MdiParent = this; //set form utama menjadi parent dari objek form yang dibuat
                frmLpPenjualan.Show(); //tampilkan form
            }
            else
            {
                form.Show();
                form.BringToFront();//agar form tampil di paling depan
            }
        }

        private void keluarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah Anda Ingin Keluar dari aplikasi ini ???", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
