﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Drawing;
using System.IO;
using MySql.Data.MySqlClient;
namespace PenjualanPembelian_LIB
{
    public class NotaBeli
    {
        public virtual string NoNotaBeli
        {
            get;
            set;
        }

        public virtual DateTime Tanggal
        {
            get;
            set;
        }

        public virtual List<NotaBeliDetil> ListNotaBeliDetil
        {
            get;
            private set;
        }

        public virtual Pegawai Pegawai
        {
            get;
            set;
        }

        public virtual Supplier Supplier
        {
            get;
            set;
        }
        #region Constructor
        public NotaBeli()
        {
            NoNotaBeli = "";
            Tanggal = DateTime.Now;
            ListNotaBeliDetil = new List<NotaBeliDetil>();
        }

        public NotaBeli(string pNota, DateTime pTanggal, Supplier pSupplier, Pegawai pPegawai)
        {
            NoNotaBeli = pNota;
            Tanggal = pTanggal;
            Supplier = pSupplier;
            Pegawai = pPegawai;
            ListNotaBeliDetil = new List<NotaBeliDetil>();
        }
        #endregion

        public void TambahDetilBarang(Barang pBarang, int pHarga, int pJumlah)
        {
            NotaBeliDetil nbd = new NotaBeliDetil(pBarang, pHarga, pJumlah);
            ListNotaBeliDetil.Add(nbd);
        }
        public static string TambahData(NotaBeli pNotaBeli)
        {
            using (var tranScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //tuliskan perintah sql 1 : menambahkan data nota ke tabel notajual
                string sql1 = "INSERT INTO notabeli(NoNota, Tanggal, KodeSupplier, KodePegawai) VALUES  ('" + pNotaBeli.NoNotaBeli + "','" + pNotaBeli.Tanggal.ToString("yyyy-MM-dd hh:mm:ss") + "'," + pNotaBeli.Supplier.KodeSupplier + "," + pNotaBeli.Pegawai.KodePegawai + ")";

                try
                {
                    //menjalankan perintah utk menambahkan ke tabel NotaJual
                    Koneksi.JalankanPerintahDML(sql1);

                    //mendapatkan semua barang yang terjual dalam nota (nota jual detil)
                    for (int i = 0; i < pNotaBeli.ListNotaBeliDetil.Count; i++)
                    {
                        //tuliskan perintah sql 2 : menambahkan barang-barang yang terjual ke tabel notajualdetil
                        string sql2 = "INSERT INTO notabelidetil(NoNota, KodeBarang, Harga, Jumlah) VALUES ('" + pNotaBeli.NoNotaBeli + "','" + pNotaBeli.ListNotaBeliDetil[i].Barang.KodeBarang + "'," + pNotaBeli.ListNotaBeliDetil[i].Harga + "," + pNotaBeli.ListNotaBeliDetil[i].Jumlah + ")";

                        //menjalankan perintah utk menambahkan ke tabel notajualdetil
                        Koneksi.JalankanPerintahDML(sql2);

                        //panggil method untuk mengupdate/mengurangi stok barang
                        string hasilUpdateBrg = Barang.UbahStokTerbeli(pNotaBeli.ListNotaBeliDetil[i].Barang.KodeBarang, pNotaBeli.ListNotaBeliDetil[i].Jumlah);
                    }
                    //jika semua perintah DML berhasil dijalankan
                    tranScope.Complete();
                    return "1";
                }
                catch (Exception ex)
                {
                    //jika ada kegagalan perintah DML
                    tranScope.Dispose();
                    return ex.Message;
                }
            }
        }
        public static string UbahData(NotaBeli pNotaBeli)
        {
            using (var tranScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //tuliskan perintah sql 1 : menambahkan data nota ke tabel notajual
                string sql1 = "UPDATE notabeli SET Tanggal = '" + pNotaBeli.Tanggal.ToString("yyyy-MM-dd hh:mm:ss") + "', KodePelanggan = " + pNotaBeli.Supplier.KodeSupplier + ", KodePegawai = " + pNotaBeli.Pegawai.KodePegawai + " WHERE NoNota = '" + pNotaBeli.NoNotaBeli + "'";

                try
                {
                    //menjalankan perintah utk menambahkan ke tabel NotaJual
                    Koneksi.JalankanPerintahDML(sql1);

                    //mendapatkan semua barang yang terjual dalam nota (nota jual detil)
                    for (int i = 0; i < pNotaBeli.ListNotaBeliDetil.Count; i++)
                    {
                        //tuliskan perintah sql 2 : menambahkan barang-barang yang terjual ke tabel notajualdetil
                        string sql2 = "UPDATE notabelidetil SET KodeBarang = '" + pNotaBeli.ListNotaBeliDetil[i].Barang.KodeBarang + "', Harga = " + pNotaBeli.ListNotaBeliDetil[i].Harga + ", Jumlah = " + pNotaBeli.ListNotaBeliDetil[i].Jumlah + " WHERE NoNota = '" + pNotaBeli.NoNotaBeli + "'";

                        //menjalankan perintah utk menambahkan ke tabel notajualdetil
                        Koneksi.JalankanPerintahDML(sql2);
                        string sql3 = "SELECT Jumlah FROM notabelidetil WHERE NoNota '" + pNotaBeli.NoNotaBeli + "'";
                        MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql3);

                        while (hasilData.Read() == true)
                        {
                            int jumBrg = int.Parse(hasilData.GetValue(0).ToString());
                            string stokSebelumnya = Barang.UbahStokTerbeli(pNotaBeli.ListNotaBeliDetil[i].Barang.KodeBarang, jumBrg);
                            string hasilUpdateBrg = Barang.UbahStokTerjual(pNotaBeli.ListNotaBeliDetil[i].Barang.KodeBarang, pNotaBeli.ListNotaBeliDetil[i].Jumlah);
                        }
                        //panggil method untuk mengupdate/mengurangi stok barang
                    }
                    //jika semua perintah DML berhasil dijalankan
                    tranScope.Complete();
                    return "1";
                }
                catch (Exception ex)
                {
                    //jika ada kegagalan perintah DML
                    tranScope.Dispose();
                    return ex.Message;
                }
            }
        }
        public static String delete(String noNota, String kodeBarang)
        {
            String msg = "";
            try
            {
                String sql1 = "DELETE FROM notabelidetil WHERE NoNota='" + noNota + "' AND KodeBarang='" + kodeBarang + "'";
                Koneksi.JalankanPerintahDML(sql1);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        public static string HapusData(NotaBeli pNotaBeli)
        {
            using (var tranScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                

                try
                {
                    //tuliskan perintah sql 1 : menambahkan data nota ke tabel notajual
                    string sql2 = "DELETE FROM notabelidetil WHERE NoNota = '" + pNotaBeli.NoNotaBeli + "'";
                    //menjalankan perintah utk menambahkan ke tabel notajualdetil
                    Koneksi.JalankanPerintahDML(sql2);

                    string sql1 = "DELETE FROM notabeli WHERE NoNota = '" + pNotaBeli.NoNotaBeli + "'";
                    Koneksi.JalankanPerintahDML(sql1);

                    //tuliskan perintah sql 2 : menambahkan barang-barang yang terjual ke tabel notajualdetil
                    

                    tranScope.Complete();
                    return "1";
                }
                catch (Exception ex)
                {
                    //jika ada kegagalan perintah DML
                    
                    return "Nota Beli Telah Terhapus";
                }
            }
        }
        public static string BacaData(string kriteria, string nilaiKriteria, List<NotaBeli> listHasilData)
        {
            string sql = "";

            //jika tidak ada kriteria yang diisikan 
            if (kriteria == "")
            {
                sql = "SELECT N.NoNota, N.Tanggal, N.KodeSupplier, S.Nama AS NamaSupplier, S.Alamat AS AlamatSupplier, N.KodePegawai, PG.Nama AS NamaPegawai FROM notabeli N INNER JOIN supplier S ON N.KodeSupplier = S.KodeSupplier INNER JOIN pegawai PG ON N.KodePegawai = PG.KodePegawai ORDER BY N.NoNota DESC";
            }
            else
            {
                sql = "SELECT N.NoNota, N.Tanggal, N.KodeSupplier, S.Nama AS NamaSupplier, S.Alamat AS AlamatSupplier, N.KodePegawai, PG.Nama AS NamaPegawai FROM notabeli N INNER JOIN supplier S ON N.KodeSupplier = S.KodeSupplier INNER JOIN pegawai PG ON N.KodePegawai = PG.KodePegawai WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'" + " ORDER BY N.NoNota DESC";
            }
            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);
                listHasilData.Clear();//kosongi isi list dulu
                while (hasilData.Read() == true)//selama masih ada data
                {
                    string nomorNota = hasilData.GetValue(0).ToString();
                    DateTime tglNota = DateTime.Parse(hasilData.GetValue(1).ToString());
                    int kodeSup= int.Parse(hasilData.GetValue(2).ToString());
                    string namaSup = hasilData.GetValue(3).ToString();
                    string alamatSup = hasilData.GetValue(4).ToString();
                    Supplier sup = new Supplier();
                    sup.KodeSupplier = kodeSup;
                    sup.NamaSupplier = namaSup;
                    sup.Alamat = alamatSup;

                    int kodePeg = int.Parse(hasilData.GetValue(5).ToString());
                    string namaPeg = hasilData.GetValue(6).ToString();
                    Pegawai peg = new Pegawai();
                    peg.KodePegawai = kodePeg;
                    peg.Nama = namaPeg;


                    NotaBeli nota = new NotaBeli(nomorNota, tglNota, sup, peg);

                    string sql2 = "SELECT NBD.KodeBarang, B.Nama, NBD.Harga, NBD.Jumlah FROM notabeli N INNER JOIN notabelidetil NBD ON N.NoNota = NBD.NoNota INNER JOIN barang B ON NBD.KodeBarang = B.KodeBarang WHERE N.NoNota = '" + nomorNota + "'";

                    MySqlDataReader hasilData2 = Koneksi.JalankanPerintahQuery(sql2);

                    while (hasilData2.Read() == true)
                    {
                        string kodeBrg = hasilData2.GetValue(0).ToString();
                        string namaBrg = hasilData2.GetValue(1).ToString();
                        Barang brg = new Barang();
                        brg.KodeBarang = kodeBrg;
                        brg.Nama = namaBrg;

                        int hrgJual = int.Parse(hasilData2.GetValue(2).ToString());
                        int jumJual = int.Parse(hasilData2.GetValue(3).ToString());
                        NotaBeliDetil detilNota = new NotaBeliDetil(brg, hrgJual, jumJual);

                        nota.TambahDetilBarang(brg, hrgJual, jumJual);
                    }
                    listHasilData.Add(nota);
                }
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string GenerateNoNota(out string pHasilNoNota)
        {
            //perintah sql : mendapatkan nomor urut transaksi di tanggal hari ini (tanggal komputer)
            string sql = "SELECT SUBSTRING(NoNota, 9, 3) AS noUrutTransaksi " + "FROM notabeli WHERE Date(Tanggal) = Date(CURRENT_DATE) " + "ORDER BY NoNota DESC LIMIT 1";
            pHasilNoNota = "";
            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);
                string noUrutTransTerbaru = "";
                //cek apakah sudah ada transaksi pada tanggal hari ini (datareader dari sql diatas bisa terbaca atau tidak)
                if (hasilData.Read() == true)
                {
                    int noUrutTrans = int.Parse(hasilData.GetValue(0).ToString()) + 1; //Dapatkan no urut transaksi terbaru
                    noUrutTransTerbaru = noUrutTrans.ToString().PadLeft(3, '0'); //jika noUrutTrans < 3 digit diberi tambahan 0 di kirinya
                }
                else // jika belum ad transaksi hari ini
                {
                    noUrutTransTerbaru = "001";
                }
                //Generate nomor nota terbaru dengan format yyyymmddxxx (yyyy : tahun , mm:bulan, dd; tanggal, xxx: nourut transaksi tgl tsb)
                string tahun = DateTime.Now.Year.ToString(); //dapatkan tahun dari tanggal komputer
                string bulan = DateTime.Now.Month.ToString().PadLeft(2, '0'); //Dapatkan bulan dari tanggal komputer, jika < 10 tambahkan 0 di kiri
                string tanggal = DateTime.Now.Day.ToString().PadLeft(2, '0'); //Dapatkan tanggal(hari) dari tanggal komputer

                //generate nomor nota terbaru sesuai format
                pHasilNoNota = tahun + bulan + tanggal + noUrutTransTerbaru.ToString();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string CetakNota(string pKriteria, string pNilaiKriteria, string pNamaFile)
        {
            try
            {
                List<NotaBeli> listNotaBeli = new List<NotaBeli>();

                //baca data nota tertentu yang akan dicetak
                string hasilBaca = NotaBeli.BacaData(pKriteria, pNilaiKriteria, listNotaBeli);

                //simpan dulu isi nota yang akan ditampilkan ke objek file (streamwriter)
                StreamWriter file = new StreamWriter(pNamaFile);

                for (int i = 0; i < listNotaBeli.Count; i++)
                {
                    //tampilkan info perusahaan
                    file.WriteLine("");
                    file.WriteLine("BENGKEL JAYA SAKTI MOTOR KNALPOT 228");
                    file.WriteLine("Jl. Mayjen Sungkono No.10, Dukuh Pakis, Surabaya");
                    file.WriteLine("Telp. (031) 5633145");
                    file.WriteLine("=".PadRight(50, '='));

                    //tampilkan header nota
                    file.WriteLine("No.Nota : " + listNotaBeli[i].NoNotaBeli);
                    file.WriteLine("Tanggal : " + listNotaBeli[i].Tanggal);
                    file.WriteLine("");
                    file.WriteLine("Supplier : " + listNotaBeli[i].Supplier.NamaSupplier);
                    file.WriteLine("Alamat    : " + listNotaBeli[i].Supplier.Alamat);
                    file.WriteLine("");
                    file.WriteLine("pembeli   : " + listNotaBeli[i].Pegawai.Nama);
                    file.WriteLine("=".PadRight(50, '='));

                    //tampilkan barang yang terjual
                    int grandTotal = 0; // untuk menampilkan grandtotal nota
                    for (int j = 0; j < listNotaBeli[i].ListNotaBeliDetil.Count; j++)
                    {
                        string nama = listNotaBeli[i].ListNotaBeliDetil[j].Barang.Nama;
                        //jika nama barang terlalu panjang maka hanya ambil 30 karakter pertama saja
                        if (nama.Length > 30)
                        {
                            nama = nama.Substring(0, 30);
                        }
                        int jumlah = listNotaBeli[i].ListNotaBeliDetil[j].Jumlah;
                        int harga = listNotaBeli[i].ListNotaBeliDetil[j].Harga;
                        int subTotal = jumlah * harga;
                        file.Write(nama.PadRight(30, ' '));
                        file.Write(jumlah.ToString().PadRight(3, ' '));
                        file.Write(harga.ToString("0,###").PadLeft(7, ' ')); // agar harga ditampilkan dengan pemisah ribuan
                        file.Write(subTotal.ToString("0,###").PadLeft(10, ' ')); //agar subtotal ditampilkan dengan pemisah ribuan
                        file.WriteLine("");
                        //hitung grandTotal nota
                        grandTotal = grandTotal + jumlah * harga;
                    }
                    file.WriteLine("=".PadRight(50, '='));
                    file.WriteLine("TOTAL : " + grandTotal.ToString("0,###"));
                    file.WriteLine("=".PadRight(50, '='));
                    file.WriteLine("Terima Kasih Atas Kunjungan Anda");
                    file.WriteLine("");
                }
                file.Close();
                //cetak ke printer
                Cetak c = new Cetak(pNamaFile, "Courier New", 9, 10, 10, 10, 10);
                c.CetakKePrinter("tulisan");
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
