using System;
using System.IO;

namespace DataStreamCW
{
    class Program
    {
        static void Main()
        {
            string inputPath = "datatask.txt";
            string outputPath = "resulttask.txt";

            string text = File.ReadAllText(inputPath);
            string[] lines = File.ReadAllLines(inputPath);

            int lineCount = lines.Length;
            int charCount = text.Length;

            string[] words = text.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int wordCount = words.Length;

            int digitCount = 0;
            int vowelCount = 0;
            int consonantCount = 0;

            string vowels = "aeiouAEIOU";

            foreach (char c in text)
            {
                if (char.IsDigit(c)) digitCount++;
                else if (vowels.Contains(c)) vowelCount++;
                else if (char.IsLetter(c)) consonantCount++;
            }

            string result =
                $"Lines: {lineCount}\n" +
                $"Characters: {charCount}\n" +
                $"Words: {wordCount}\n" +
                $"Digits: {digitCount}\n" +
                $"Vowels: {vowelCount}\n" +
                $"Consonants: {consonantCount}";

            Console.WriteLine(result);
            File.WriteAllText(outputPath, result);
            Console.WriteLine($"\nResult saved to {outputPath}");
        }
    }
}