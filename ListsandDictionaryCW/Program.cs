string text = "This is the house that Jack built. And this is the wheat that was kept in the dark closet in the house that Jack built. And this is the cheerful titmouse that often steals wheat that was kept in the dark closet in the house that Jack built.";
char[] separators = { ' ', ',', '.' };
string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
Dictionary<string, int> statistic = new Dictionary<string, int>();

foreach (string word in words)
{
    if (statistic.ContainsKey(word))
    {
        statistic[word]++;
    }
    else
    {
        statistic.Add(word, 1);
    }
}

Console.WriteLine("   Word:            Count:");

int number = 1;
foreach (KeyValuePair<string, int> pair in statistic)
{
    Console.WriteLine($"{number,2}. {pair.Key,-16} {pair.Value}");
    number++;
}

Console.WriteLine($"Total words: {words.Length} unique words: {statistic.Count}");
