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
using System.IO;
using MySql.Data.MySqlClient;
namespace PenjualanPembelian_LIB
{
    public class NotaBeliDetil
    {
        public virtual int Harga
        {
            get;
            set;
        }

        public virtual int Jumlah
        {
            get;
            set;
        }

        public virtual Barang Barang
        {
            get;
            set;
        }
        #region Constructor
        public NotaBeliDetil()
        {
            Jumlah = 1;
            Harga = 0;
        }
        public NotaBeliDetil(Barang pBarang, int pHarga, int pJumlah)
        {
            Barang = pBarang;
            Harga = pHarga;
            Jumlah = pJumlah;
        }
        #endregion

        /*public static string BacaData(string kriteria, string nilaiKriteria, List<NotaBeliDetil> listHasilData)
        {
            string sql = "";

            //jika tidak ada kriteria yang diisikan 
            if (kriteria == "")
            {
                sql = "SELECT N.NoNota, N.Tanggal, N.KodeSupplier, S.Nama AS NamaSupplier, S.Alamat AS AlamatSupplier, N.KodePegawai, PG.Nama AS NamaPegawai FROM notabelidetil N INNER JOIN supplier S ON N.KodeSupplier = S.KodeSupplier INNER JOIN pegawai PG ON N.KodePegawai = PG.KodePegawai ORDER BY N.NoNota DESC";
            }
            else
            {
                sql = "SELECT N.NoNota, N.Tanggal, N.KodeSupplier, S.Nama AS NamaSupplier, S.Alamat AS AlamatSupplier, N.KodePegawai, PG.Nama AS NamaPegawai FROM notabelidetil N INNER JOIN supplier S ON N.KodeSupplier = S.KodeSupplier INNER JOIN pegawai PG ON N.KodePegawai = PG.KodePegawai WHERE " + kriteria + " LIKE '%" + nilaiKriteria + "%'" + " ORDER BY N.NoNota DESC";
            }
            try
            {
                MySqlDataReader hasilData = Koneksi.JalankanPerintahQuery(sql);
                listHasilData.Clear();//kosongi isi list dulu
                while (hasilData.Read() == true)//selama masih ada data
                {
                    string nomorNota = hasilData.GetValue(0).ToString();
                    DateTime tglNota = DateTime.Parse(hasilData.GetValue(1).ToString());
                    int kodeSup = int.Parse(hasilData.GetValue(2).ToString());
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
                        NotaJualDetil detilNota = new NotaJualDetil(brg, hrgJual, jumJual);

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
        }*/
    }
}

