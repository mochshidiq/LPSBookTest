using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        Console.Write("Input your words: ");
        string words = Console.ReadLine().Replace(" ", "").ToLower();
        Dictionary<char, int> data = new Dictionary<char, int>();
        foreach (char word in words)
            data[word] = data.GetValueOrDefault(word) + 1;

        foreach (var datum in data)
            Console.WriteLine(datum.Key + " - " + datum.Value);

    }
}