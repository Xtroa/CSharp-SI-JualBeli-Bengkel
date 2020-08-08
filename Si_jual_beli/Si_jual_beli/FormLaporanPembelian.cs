using MySql.Data.MySqlClient;
using PenjualanPembelian_LIB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Si_jual_beli
{
    public partial class FormLaporanPembelian : Form
    {
        public void loadGridView()
        {
            try
            {
                string periode = comboBoxBulan.Items[comboBoxBulan.SelectedIndex]+"";
                string keyword = "%"+tbKeyword.Text+"%";
                MySqlConnection conn = new MySqlConnection(Koneksi.strCon);
                conn.Open();

                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                
                String q = "SELECT DATE_FORMAT(Tanggal,'%d-%M-%Y') as Tanggal,b.Nama,nd.Jumlah,nd.Harga,(nd.Harga*nd.Jumlah) as Subtotal,nb.NoNota " +
                         "FROM notabeli nb " +
                         "INNER JOIN notabelidetil nd ON (nb.NoNota=nd.NoNota) " +
                         "INNER JOIN barang b ON (b.KodeBarang=nd.KodeBarang) " +
                         "WHERE DATE_FORMAT(Tanggal,'%Y-%M')=?periode AND nb.NoNota LIKE ?keyword";
                MySqlCommand c = new MySqlCommand(q, conn);
                c.Parameters.Add(new MySqlParameter("periode", periode));
                c.Parameters.Add(new MySqlParameter("keyword", keyword));

                if (comboBoxBulan.SelectedIndex==0)
                {
                    q = "SELECT DATE_FORMAT(Tanggal,'%d-%M-%Y') as Tanggal,b.Nama,nd.Jumlah,nd.Harga,(nd.Harga*nd.Jumlah) as Subtotal,nb.NoNota " +
                        "FROM notabeli nb " +
                        "INNER JOIN notabelidetil nd ON (nb.NoNota=nd.NoNota) " +
                        "INNER JOIN barang b ON (b.KodeBarang=nd.KodeBarang) " +
                        "WHERE  nb.NoNota LIKE ?keyword";
                    c = new MySqlCommand(q, conn);
                    c.Parameters.Add(new MySqlParameter("keyword", keyword));

                }
                MyDA.SelectCommand = c;

                DataTable table = new DataTable();
                MyDA.Fill(table);


                BindingSource bSource = new BindingSource();
                bSource.DataSource = table;


                dataGridView1.DataSource = bSource;
                int jumlah = 0;
                for (int i = 0; i < dataGridView1.RowCount;i++ )
                {
                    int jml = 0;
                    try
                    {
                        String strJumlah = dataGridView1.Rows[i].Cells[4].Value + "";
                        jml=Int32.Parse(strJumlah);
                    }
                    catch (Exception ex)
                    {

                    }
                       
                    jumlah = jumlah+jml;

                }
                string s = jumlah.ToString("#,##0");
                label4.Text = s;
                    for (int i = 1; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    dataGridView1.Columns[3].DefaultCellStyle.Format = "##,#";
                    dataGridView1.Columns[4].DefaultCellStyle.Format = "##,#";
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public FormLaporanPembelian()
        {
            InitializeComponent();
        }

        public void FormLaporanPembelian_Shown(object sender, EventArgs e)
        {
            loadComboPeriode();
        }

        public void loadComboPeriode()
        {
            string sql = "SELECT DATE_FORMAT(Tanggal,'%Y-%M') as Periode FROM notabeli GROUP BY DATE_FORMAT(Tanggal,'%Y-%M')";
            comboBoxBulan.Items.Clear();
            try
            {
                //Koneksi.JalankanPerintahDML(sql);
                MySqlDataReader rd = Koneksi.JalankanPerintahQuery(sql);
                comboBoxBulan.Items.Add("Semua Tampil");
                while (rd.Read())
                {
                    comboBoxBulan.Items.Add(rd["Periode"] + "");
                }
                rd.Close();
                comboBoxBulan.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if(comboBoxBulan.Text == "Semua Tampil")
            {
                MessageBox.Show("pilih bulan, tidak bisa semua");
            }
            else
            {
                printData();
            }
        }

        public void printData()
        {
            Pegawai pw = new Pegawai();
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            String appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            String nama = appPath + "\\" + milliseconds + ".xlsx";
            pw.printLaporanPembelian(nama,comboBoxBulan.Items[comboBoxBulan.SelectedIndex]+"");


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel File|*.xlsx";
            saveFileDialog1.Title = "Save an Excel File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {

                String fnam = saveFileDialog1.FileName;
                File.Copy(nama,saveFileDialog1.FileName, true);
                //fs.Close();
            }  
        }

        

        private void comboBoxBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGridView();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbKeyword_TextChanged(object sender, EventArgs e)
        {
            loadGridView();
        }

        public void FormLaporanPembelian_Load(object sender, EventArgs e)
        {

        }

        private void labelTotal_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
