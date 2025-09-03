using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public class VowelSubsequenceFinder
{
    private static readonly HashSet<char> Vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        if (words == null || words.Count == 0)
            return "[]";

        var results = words.Select(word => new
        {
            word,
            sequence = FindLongestVowelSequence(word),
            length = FindLongestVowelSequence(word).Length
        });

        return JsonSerializer.Serialize(results);
    }

    private static string FindLongestVowelSequence(string word)
    {
        if (string.IsNullOrEmpty(word))
            return string.Empty;

        string longest = string.Empty;
        string current = string.Empty;

        foreach (char c in word.ToLower())
        {
            if (Vowels.Contains(c))
            {
                current += c;
                if (current.Length > longest.Length)
                    longest = current;
            }
            else
            {
                current = string.Empty;
            }
        }

        return longest;
    }

    static void Main(string[] args)
    {
        var words1 = new List<string> { "aeiou", "bcd", "aaa" };
        Console.WriteLine(VowelSubsequenceFinder.LongestVowelSubsequenceAsJson(words1) + "\n");

        var words2 = new List<string> { "miscellaneous", "queue", "sky", "cooperative" };
        Console.WriteLine(VowelSubsequenceFinder.LongestVowelSubsequenceAsJson(words2) + "\n");

        var words3 = new List<string> { "sequential", "beautifully", "rhythms", "encyclopaedia" };
        Console.WriteLine(VowelSubsequenceFinder.LongestVowelSubsequenceAsJson(words3) + "\n");

        var words4 = new List<string> { "algorithm", "education", "idea", "strength" };
        Console.WriteLine(VowelSubsequenceFinder.LongestVowelSubsequenceAsJson(words4) + "\n");

        var words5 = new List<string> { };
        Console.WriteLine(VowelSubsequenceFinder.LongestVowelSubsequenceAsJson(words5) + "\n");
    }
}