using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormUtama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ubah form ini menjadi fullscreen
            this.WindowState = FormWindowState.Maximized;

            //ubah form ini menjaid MdiParent
            this.IsMdiContainer = true;
        }

        private void kategoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //buka form kategori barang
            Form form = Application.OpenForms["FormKategoriBarang"];

            if(form == null) //jika form ini belum pernah di-create sebelumnya
            {
                FormKategoriBarang
            }
        }
    }
}
