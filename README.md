# LPS .NET Developer Skill Test

## Deskripsi
Implementasi tes developer yang mencakup pengelolaan data buku, pencatatan histori perubahan, export CSV, model generator, dan program console untuk pengolahan karakter serta angka.

## Teknologi
- C#
- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Microsoft.Data.SqlClient

## Struktur Project
- `LPSBookTest`: aplikasi web untuk CRUD Jenis Buku dan Buku.
- `LPSModelGenerator`: console application untuk menghasilkan file model C# berdasarkan tabel database.
- `CharacterFrequency`: console application untuk menghitung frekuensi karakter.
- `IdicLpsCounter`: console application untuk menjalankan aturan IDIC/LPS.

## Persiapan Database
1. Siapkan SQL Server.
2. Buat database `LPSBookTestDb`.
3. Jalankan script `database/LPSBookTestDb_Schema.sql`.
4. Sesuaikan connection string aplikasi dengan server dan database yang digunakan.
5. Terapkan migration menggunakan Entity Framework Core jika diperlukan.

## Menjalankan Aplikasi Web
Buka solution `LPSBookTest.sln` di Visual Studio, pilih project `LPSBookTest` sebagai startup project, lalu jalankan aplikasi.

## Menjalankan Model Generator
Pilih project `LPSModelGenerator` sebagai startup project, kemudian jalankan program. File model dihasilkan berdasarkan metadata tabel pada database.

## Menjalankan Program Console
Jalankan project `CharacterFrequency` untuk soal frekuensi karakter dan `IdicLpsCounter` untuk soal IDIC/LPS.

## Fitur
- CRUD master Jenis Buku.
- CRUD data Buku dengan dropdown Jenis Buku.
- Pencarian data buku.
- Export data buku ke CSV.
- Pencatatan Create, Edit, dan Delete ke tabel history.
- Model generator berdasarkan metadata tabel database.
- Character Frequency Counter.
- Program IDIC/LPS.

## Catatan
Sesuaikan instruksi database dan konfigurasi lokal dengan environment yang digunakan. Semua status fitur dan pengujian pada README harus sesuai dengan hasil pengujian aktual.
