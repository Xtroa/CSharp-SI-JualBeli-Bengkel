-- phpMyAdmin SQL Dump
-- version 4.7.7
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 08 Agu 2020 pada 03.45
-- Versi server: 10.1.30-MariaDB
-- Versi PHP: 7.2.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `si_jual_beli`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `barang`
--

CREATE TABLE `barang` (
  `KodeBarang` char(5) NOT NULL,
  `Barcode` varchar(13) DEFAULT NULL,
  `Nama` varchar(45) DEFAULT NULL,
  `HargaJual` int(11) DEFAULT NULL,
  `Stok` smallint(6) DEFAULT NULL,
  `KodeKategori` char(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data untuk tabel `barang`
--

INSERT INTO `barang` (`KodeBarang`, `Barcode`, `Nama`, `HargaJual`, `Stok`, `KodeKategori`) VALUES
('01001', '', 'Oli Shell HX7 SAE 10W40 4 liter', 200000, 45, '01'),
('01002', '', 'Oli Shell HX5 4 Liter ', 180000, -3, '01'),
('01003', '', 'fdsdf', 1220, 19, '01'),
('02001', '', 'ESTILO Exh.pipe', 510000, -1, '02'),
('02002', '', 'ACCORD Exh.pipe', 70000, 0, '02'),
('02003', '', 'baru', 333330, 22, '02'),
('02004', '', 'tes', 222220, 30, '02'),
('03001', '', 'GS ASTRA AKI Mobil Hybrid GSHY-NS60', 790000, 8, '03'),
('03002', '', 'BOSCH Aki Kering Mobil SM Mega Power', 850000, 4, '03'),
('03003', '', 'aaa', 222, 2220, '03'),
('04001', '', 'Piringan Cakram Accord 2003-2007', 1300000, 5, '04'),
('04002', '', 'Piringan Cakram Escudo 2003', 900000, 4, '04'),
('05001', '', 'service', 0, 9998, '05'),
('B5002', NULL, 'gdfgd', 33, 3, '02');

-- --------------------------------------------------------

--
-- Struktur dari tabel `jabatan`
--

CREATE TABLE `jabatan` (
  `IdJabatan` char(2) NOT NULL,
  `Nama` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data untuk tabel `jabatan`
--

INSERT INTO `jabatan` (`IdJabatan`, `Nama`) VALUES
('J1', 'Pegawai Pembelian'),
('J2', 'Kasir'),
('J3', 'Pemilik');

-- --------------------------------------------------------

--
-- Struktur dari tabel `kategori`
--

CREATE TABLE `kategori` (
  `KodeKategori` char(2) NOT NULL,
  `Nama` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data untuk tabel `kategori`
--

INSERT INTO `kategori` (`KodeKategori`, `Nama`) VALUES
('01', 'oli'),
('02', 'knalpot'),
('03', 'Aki'),
('04', 'Sparepart'),
('05', 'Service'),
('06', 'tes'),
('07', 'ss');

-- --------------------------------------------------------

--
-- Struktur dari tabel `notabeli`
--

CREATE TABLE `notabeli` (
  `NoNota` char(11) NOT NULL,
  `Tanggal` datetime DEFAULT NULL,
  `KodeSupplier` int(11) NOT NULL,
  `KodePegawai` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data untuk tabel `notabeli`
--

INSERT INTO `notabeli` (`NoNota`, `Tanggal`, `KodeSupplier`, `KodePegawai`) VALUES
('20181218001', '2018-12-18 10:03:32', 1, 10),
('20190416001', '2019-04-16 11:21:01', 1, 1),
('20190416002', '2019-04-16 11:22:13', 1, 1),
('20190424001', '2019-04-24 12:13:19', 1, 1),
('20190424002', '2019-04-24 07:57:49', 1, 1),
('20190428002', '2019-04-28 12:14:54', 1, 1),
('20190428003', '2019-04-28 12:28:51', 1, 1),
('20190428004', '2019-04-28 12:35:54', 1, 1),
('20190428006', '2019-04-28 12:40:29', 1, 1),
('20190502001', '2019-05-02 07:27:37', 1, 1),
('20190502002', '2019-05-02 07:28:13', 1, 1),
('20190502003', '2019-05-02 07:33:14', 1, 1),
('20190502004', '2019-05-02 07:33:47', 1, 1),
('20190506001', '2019-05-06 09:14:25', 1, 1),
('20190507001', '2019-05-07 07:53:42', 1, 1),
('20190509001', '2019-05-09 04:05:53', 4, 1);

-- --------------------------------------------------------

--
-- Struktur dari tabel `notabelidetil`
--

CREATE TABLE `notabelidetil` (
  `NoNota` char(11) NOT NULL,
  `KodeBarang` char(5) NOT NULL,
  `Harga` int(11) DEFAULT NULL,
  `Jumlah` smallint(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data untuk tabel `notabelidetil`
--

INSERT INTO `notabelidetil` (`NoNota`, `KodeBarang`, `Harga`, `Jumlah`) VALUES
('20181218001', '01001', 15000, 2),
('20181218001', '02002', 30000, 3),
('20181218001', '03002', 50000, 1),
('20190416001', '01001', 125000, 3),
('20190424001', '01001', 200000, 3),
('20190424001', '01002', 180000, 8),
('20190424002', '01001', 200000, 18),
('20190428002', '01001', 200000, 10),
('20190428002', '01002', 180000, 2),
('20190428004', '01001', 200000, 2),
('20190428004', '01002', 180000, 3),
('20190502001', '01001', 200000, 1),
('20190502001', '02001', 510000, 1),
('20190502003', '01001', 200000, 2),
('20190502003', '01002', 180000, 2),
('20190507001', '01002', 180000, 1),
('20190509001', '01001', 200000, 1);

-- --------------------------------------------------------

--
-- Struktur dari tabel `notajual`
--

CREATE TABLE `notajual` (
  `NoNota` char(11) NOT NULL,
  `Tanggal` datetime DEFAULT NULL,
  `KodePelanggan` int(11) NOT NULL,
  `KodePegawai` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data untuk tabel `notajual`
--

INSERT INTO `notajual` (`NoNota`, `Tanggal`, `KodePelanggan`, `KodePegawai`) VALUES
('20181218001', '2018-12-18 09:56:51', 1, 10),
('20190416001', '2019-04-16 01:58:43', 1, 1),
('20190424001', '2019-04-24 11:58:22', 1, 1),
('20190428001', '2019-04-28 11:44:16', 1, 1),
('20190430001', '2019-04-30 12:54:32', 1, 1),
('20190430002', '2019-04-30 12:56:27', 1, 1),
('20190430003', '2019-04-30 01:05:02', 1, 1),
('20190502001', '2019-05-02 07:13:01', 1, 1),
('20190502002', '2019-05-02 07:38:49', 1, 1),
('20190506001', '2019-05-06 08:31:16', 1, 1),
('20190506002', '2019-05-06 08:38:30', 1, 1),
('20190506003', '2019-05-06 08:42:01', 1, 1),
('20190506004', '2019-05-06 08:47:06', 1, 1),
('20190506005', '2019-05-06 08:50:48', 1, 1),
('20190506006', '2019-05-06 08:52:04', 1, 1),
('20190506007', '2019-05-06 08:53:24', 1, 1),
('20190506008', '2019-05-06 08:55:03', 1, 1),
('20190506009', '2019-05-06 08:59:09', 1, 1),
('20190506010', '2019-05-06 09:12:57', 1, 1),
('20190507001', '2019-05-07 03:15:59', 1, 1),
('20190509001', '2019-05-09 03:58:03', 1, 1),
('20200808001', '2020-08-08 08:05:20', 1, 9),
('20200808002', '2020-08-08 08:09:42', 1, 9);

-- --------------------------------------------------------

--
-- Struktur dari tabel `notajualdetil`
--

CREATE TABLE `notajualdetil` (
  `NoNota` char(11) NOT NULL,
  `KodeBarang` char(5) NOT NULL,
  `Harga` int(11) DEFAULT NULL,
  `Jumlah` smallint(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data untuk tabel `notajualdetil`
--

INSERT INTO `notajualdetil` (`NoNota`, `KodeBarang`, `Harga`, `Jumlah`) VALUES
('20181218001', '01001', 50000, 6),
('20190416001', '03001', 100000, 3),
('20190424001', '01001', 200000, 1),
('20190424001', '01002', 180000, 13),
('20190424001', '02001', 510000, 1),
('20190428001', '01001', 200000, 1),
('20190430002', '01002', 180000, 1),
('20190502001', '01001', 200000, 2),
('20190502001', '02002', 70000, 3),
('20190506001', '02001', 510000, 1),
('20190506001', '02003', 333330, 1),
('20190506001', '05001', 66678, 1),
('20190506002', '01003', 1220, 12),
('20190506002', '02001', 510000, 1),
('20190506003', '01002', 180000, 2),
('20190506004', '01002', 180000, 1),
('20190506006', '01002', 180000, 1),
('20190506007', '01002', 180000, 1),
('20190506007', '02001', 510000, 1),
('20190506008', '02001', 510000, 1),
('20190506009', '01002', 180000, 1),
('20190506009', '01003', 1220, 1),
('20190506010', '01002', 180000, 1),
('20190507001', '02002', 70000, 1),
('20190509001', '02001', 510000, 2),
('20200808001', '01002', 180000, 3),
('20200808002', '01002', 180000, 2);

-- --------------------------------------------------------

--
-- Struktur dari tabel `pegawai`
--

CREATE TABLE `pegawai` (
  `KodePegawai` int(11) NOT NULL,
  `Nama` varchar(45) DEFAULT NULL,
  `TglLahir` date DEFAULT NULL,
  `Alamat` varchar(100) DEFAULT NULL,
  `Gaji` bigint(20) DEFAULT NULL,
  `Username` varchar(8) DEFAULT NULL,
  `Password` varchar(8) DEFAULT NULL,
  `IdJabatan` char(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data untuk tabel `pegawai`
--

INSERT INTO `pegawai` (`KodePegawai`, `Nama`, `TglLahir`, `Alamat`, `Gaji`, `Username`, `Password`, `IdJabatan`) VALUES
(1, 'Nancy', '2017-08-16', 'Tenggilis Mejoyo AA-10', 5000000, 'nancy', 'abc', 'J3'),
(2, 'Andrew', '1977-03-09', 'Raya Darmo 129', 10000000, 'andrew', '1234', 'J3'),
(3, 'Janet', '1987-02-20', 'Darmo Permai Utara X/12', 4000000, 'janet', 'janet123', 'J1'),
(4, 'Margaret', '1984-11-20', 'Raya Kendangsari 200', 4000000, 'margaret', 'margaret', 'J2'),
(5, 'Steven', '1967-03-07', 'Raya Tenggilis 44', 3000000, 'steve', 'steve123', 'J2'),
(6, 'Michael', '1988-07-12', 'Sidosermo Indah Blok A No 12', 3000000, 'michael', '123', 'J1'),
(7, 'Jennifer', '2000-02-17', 'Citraland Taman Gapura Blok C-20 ', 25000000, 'jennifer', '1234567', 'J1'),
(8, 'Angger', '2018-12-01', 'bratang gede 3/16', 100000, 'angger1', 'angger', 'J3'),
(9, 'yudis', '2018-12-18', 'siwalankerto', 1000000, 'yudis', 'yudis', 'J3'),
(10, 'yudistira', '2018-12-18', 'surabaya', 2333333, 'tes123', '1234', 'J3'),
(11, 'cek', '2019-04-24', 'cek', 2123, 'cek', '123', 'J2');

-- --------------------------------------------------------

--
-- Struktur dari tabel `pelanggan`
--

CREATE TABLE `pelanggan` (
  `KodePelanggan` int(11) NOT NULL,
  `Nama` varchar(50) DEFAULT NULL,
  `Alamat` varchar(100) DEFAULT NULL,
  `Telepon` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data untuk tabel `pelanggan`
--

INSERT INTO `pelanggan` (`KodePelanggan`, `Nama`, `Alamat`, `Telepon`) VALUES
(1, 'Pelanggan Umum', '-', '-'),
(2, 'JUNIWO', 'sdadds', '123213131231'),
(3, 'JUNIWO', 'sdadds', ''),
(4, 'ad', 'adas', '123123');

-- --------------------------------------------------------

--
-- Struktur dari tabel `supplier`
--

CREATE TABLE `supplier` (
  `KodeSupplier` int(11) NOT NULL,
  `Nama` varchar(30) DEFAULT NULL,
  `Alamat` varchar(100) DEFAULT NULL,
  `Telepon` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data untuk tabel `supplier`
--

INSERT INTO `supplier` (`KodeSupplier`, `Nama`, `Alamat`, `Telepon`) VALUES
(1, 'ubah', 'ubah', '03187652263'),
(2, 'Cooperativa the Spain', 'MH. Thamrin 55', '08745355635'),
(3, 'UD. Subur Selalu', 'Raya Jemursari 123', '08908087'),
(4, 'Leka Trading', '22 Jump Street', '343242423534'),
(5, 'TOKO BIJAKSANA', 'AYANI', '0318765226'),
(6, 'aa', 'aaa', ''),
(7, 'aa', 'aa', ''),
(8, 'ufds', 'sdfs', ''),
(9, 'ufdssdfsfddf', 'sdfsfdsfs', ''),
(10, 'ufdssdfsfddfdfsfs', 'sdfsfdsfsfdssfd', ''),
(11, 'ufdssdfsfddfdfsfsdsfsf', 'sdfsfdsfsfdssfddfsfs', ''),
(12, 'ufdssdfsfddfdfsfsdsfsf', 'sdfsfdsfsfdssfddfsfs', '');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `barang`
--
ALTER TABLE `barang`
  ADD PRIMARY KEY (`KodeBarang`),
  ADD KEY `fk_Produk_Kategori1_idx` (`KodeKategori`);

--
-- Indeks untuk tabel `jabatan`
--
ALTER TABLE `jabatan`
  ADD PRIMARY KEY (`IdJabatan`);

--
-- Indeks untuk tabel `kategori`
--
ALTER TABLE `kategori`
  ADD PRIMARY KEY (`KodeKategori`);

--
-- Indeks untuk tabel `notabeli`
--
ALTER TABLE `notabeli`
  ADD PRIMARY KEY (`NoNota`),
  ADD KEY `fk_NotaBeli_Pemasok1_idx` (`KodeSupplier`),
  ADD KEY `fk_NotaBeli_Pegawai1_idx` (`KodePegawai`);

--
-- Indeks untuk tabel `notabelidetil`
--
ALTER TABLE `notabelidetil`
  ADD PRIMARY KEY (`NoNota`,`KodeBarang`),
  ADD KEY `fk_NotaBeli_has_Produk_Produk1_idx` (`KodeBarang`),
  ADD KEY `fk_NotaBeli_has_Produk_NotaBeli1_idx` (`NoNota`);

--
-- Indeks untuk tabel `notajual`
--
ALTER TABLE `notajual`
  ADD PRIMARY KEY (`NoNota`),
  ADD KEY `fk_NotaJual_Pelanggan1_idx` (`KodePelanggan`),
  ADD KEY `fk_NotaJual_Pegawai1_idx` (`KodePegawai`);

--
-- Indeks untuk tabel `notajualdetil`
--
ALTER TABLE `notajualdetil`
  ADD PRIMARY KEY (`NoNota`,`KodeBarang`),
  ADD KEY `fk_NotaJual_has_Produk_Produk1_idx` (`KodeBarang`),
  ADD KEY `fk_NotaJual_has_Produk_NotaJual_idx` (`NoNota`);

--
-- Indeks untuk tabel `pegawai`
--
ALTER TABLE `pegawai`
  ADD PRIMARY KEY (`KodePegawai`),
  ADD KEY `fk_Pegawai_Jabatan1_idx` (`IdJabatan`);

--
-- Indeks untuk tabel `pelanggan`
--
ALTER TABLE `pelanggan`
  ADD PRIMARY KEY (`KodePelanggan`);

--
-- Indeks untuk tabel `supplier`
--
ALTER TABLE `supplier`
  ADD PRIMARY KEY (`KodeSupplier`);

--
-- Ketidakleluasaan untuk tabel pelimpahan (Dumped Tables)
--

--
-- Ketidakleluasaan untuk tabel `notabeli`
--
ALTER TABLE `notabeli`
  ADD CONSTRAINT `fk_NotaBeli_Pemasok1` FOREIGN KEY (`KodeSupplier`) REFERENCES `supplier` (`KodeSupplier`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Ketidakleluasaan untuk tabel `notabelidetil`
--
ALTER TABLE `notabelidetil`
  ADD CONSTRAINT `fk_NotaBeli_has_Produk_NotaBeli1` FOREIGN KEY (`NoNota`) REFERENCES `notabeli` (`NoNota`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_NotaBeli_has_Produk_Produk1` FOREIGN KEY (`KodeBarang`) REFERENCES `barang` (`KodeBarang`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Ketidakleluasaan untuk tabel `notajual`
--
ALTER TABLE `notajual`
  ADD CONSTRAINT `fk_NotaJual_Pelanggan1` FOREIGN KEY (`KodePelanggan`) REFERENCES `pelanggan` (`KodePelanggan`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Ketidakleluasaan untuk tabel `notajualdetil`
--
ALTER TABLE `notajualdetil`
  ADD CONSTRAINT `fk_NotaJual_has_Produk_NotaJual` FOREIGN KEY (`NoNota`) REFERENCES `notajual` (`NoNota`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_NotaJual_has_Produk_Produk1` FOREIGN KEY (`KodeBarang`) REFERENCES `barang` (`KodeBarang`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
