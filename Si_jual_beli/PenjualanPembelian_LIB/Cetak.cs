using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//agar objek bertipe file stream dapat digunakan
using System.IO;

//agar objek bertipe font dapat digunakan
using System.Drawing;

//agar objek bertipe PrintPageEventArgs dapat digunakan
using System.Drawing.Printing;
namespace PenjualanPembelian_LIB
{
    class Cetak
    {
        private Font jenisFont; //menyimpan nama dan ukuran font yang digunakan untuk mencetak ke printer
        private StreamReader fileCetak; //menyimpan file stream berisi tulisan yang akan dibaca dan dicetak ke printer
        private float marginKiri, marginKanan, marginAtas, marginBawah; //menyimpan margin kertas


        #region properties
        public float MarginKiri
        {
            get
            {
                return marginKiri;
            }

            set
            {
                marginKiri = value;
            }
        }

        public float MarginKanan
        {
            get
            {
                return marginKanan;
            }

            set
            {
                marginKanan = value;
            }
        }

        public float MarginAtas
        {
            get
            {
                return marginAtas;
            }

            set
            {
                marginAtas = value;
            }
        }

        public float MarginBawah
        {
            get
            {
                return marginBawah;
            }

            set
            {
                marginBawah = value;
            }
        }

        public StreamReader FileCetak
        {
            get
            {
                return fileCetak;
            }

            set
            {
                fileCetak = value;
            }
        }

        public Font JenisFont
        {
            get
            {
                return jenisFont;
            }

            set
            {
                jenisFont = value;
            }
        }
        #endregion

        #region Constructor
        //untuk mencetak dengan format default    
        public Cetak(string namaFile) {
            FileCetak = new StreamReader(namaFile);
            JenisFont = new Font("Arial", 10);
            MarginKiri = (float)10.5;
            MarginKanan = (float)10.5;
            MarginAtas = (float)10.5;
            MarginBawah = (float)10.5;

        }

        //untuk mencetak dengan format custom
        public Cetak(string pNnamaFile, string pNamaFont, int pUkuranFont, float pMarginKiri, float pMarginKanan, float pMarginAtas, float pMarginBawah)
        {
            FileCetak = new StreamReader(pNnamaFile);
            JenisFont = new Font(pNamaFont, pUkuranFont);
            MarginKiri = pMarginKiri;
            MarginKanan = pMarginKanan;
            MarginAtas = pMarginAtas;
            MarginBawah = pMarginBawah;
        }
        #endregion

        private void CetakTulisan(object sender, PrintPageEventArgs e) {
            //hitung jumlah baris maksimal yang dapat ditampilkan pada 1 halaman kertas
            int jumBarisPerHalaman = (int)((e.MarginBounds.Height - MarginBawah) / JenisFont.GetHeight(e.Graphics));
            //untuk menyimpan posisi y terakhir tulisan yang telah tercetak
            float y = marginAtas;
            //untuk mnyimpan jumlah bariss tulisan yang telah tercetak
            int jumBaris = 0;
            
            //untuk menyimpan tulisan yang akan dicetak
            string tulisanCetak = FileCetak.ReadLine();

            //baca filestream untuk mencetak tiap baris tulisan
            while (jumBaris < jumBarisPerHalaman && tulisanCetak != null) {
                y = MarginAtas + (jumBaris * JenisFont.GetHeight(e.Graphics));

                //cetak tulisan sesuai jenis font dan margin (warna tulisan hitam)
                e.Graphics.DrawString(tulisanCetak, JenisFont, Brushes.Black, MarginKiri, y);

                //jumlah baris tercetak ditambah 1
                jumBaris++;

                //baca baris file berikutnya
                tulisanCetak = FileCetak.ReadLine();
            }
            //jika masih belum selesai mencetak, cetak di halaman berikutnya
            if (tulisanCetak != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        public string CetakKePrinter(string pTipe) {
            try
            {
                //buat objek untuk mencetak
                PrintDocument p = new PrintDocument();
                if (pTipe == "tulisan") //jika tipe yang akan dicetak adalah teks atau tulisan
                {
                    //tambahkan event handler untuk mencetak tulisan
                    p.PrintPage += new PrintPageEventHandler(CetakTulisan);
                }

                //cetak tulisan
                p.Print();

                FileCetak.Close();

                return "1";
            }
            catch (Exception ex)
            {
                return "Proses cetak gagal. Pesan kesalahan = " + ex.Message;
            }
        }
    }
}
