using System;

public class Program
{
    public static void Main()
    {
        Console.Write("Masukkan Total Angka: "); int batas = Int32.Parse(Console.ReadLine());
        string hasil = "";
        for (int i = 1; i <= batas; i++)
        {
            hasil += (i % 5 == 0 && i % 6 == 0 && i > 6) ? " IDICLPS " :
             (i % 5 == 0 && i > 6) ? " IDIC " :
             (i % 6 == 0 && i > 6) ? " LPS " :
             " " + i.ToString() + " ";
        }
        Console.WriteLine(hasil);
    }
}