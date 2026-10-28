using System;
using System.Collections.Generic;  

public class CharCounter
{
    public static void Main(string[] args)
    {
        Console.Write("Masukkan sebuah string: ");
        string words = Console.ReadLine().Replace(" ", "").ToLower();

        Dictionary<char, int> charCount = new Dictionary<char, int>();
        foreach (char c in words)
            charCount[c] = charCount.GetValueOrDefault(c) + 1;

        foreach (var datum in charCount)
            Console.WriteLine($"'{datum.Key}': {datum.Value}");
    }
}