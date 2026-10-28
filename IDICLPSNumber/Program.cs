using System;

public class IDICLPSNumber
{
    public static void Main(string[] args)
    {
        Console.Write("Masukkan angka : ");
        int input = int.Parse(Console.ReadLine());
        string output = "";
        for(int i = 1; i <= input; i++)
        {
            output += (i % 5 == 0 && i % 6 == 0 && i > 6) ? " IDICLPS " :
                (i % 5 == 0 && i > 5) ? " IDIC " : (i % 6 == 0 && i > 6) ? " LPS " :
                 " " + i.ToString() + " ";
        }
        Console.WriteLine(output);
    }
 
}