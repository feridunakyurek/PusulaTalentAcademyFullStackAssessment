using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

class SubarrayFinder
{
    public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
            return "[]";

        var (best, curr) = numbers
            .Skip(1)
            .Aggregate(
                (best: new List<int>(), curr: new List<int> { numbers[0] }),
                (state, num) =>
                {
                    var (best, curr) = state;

                    if (num > curr.Last())
                    {
                        curr.Add(num);
                    }
                    else
                    {
                        best = SelectBetterSubarray(best, curr);
                        curr = new List<int> { num };
                    }

                    return (best, curr);
                });

        var finalBest = SelectBetterSubarray(best, curr);

        return JsonSerializer.Serialize(finalBest);
    }

    private static List<int> SelectBetterSubarray(List<int> best, List<int> candidate)
    {
        return candidate.Sum() > best.Sum() ? new List<int>(candidate) : best;
    }

    static void Main(string[] args)
    {
        var array1 = new List<int> { 1, 2, 3, 1, 2 };

        string result = SubarrayFinder.MaxIncreasingSubarrayAsJson(array1);
        Console.WriteLine(result);

        var array2 = new List<int> { 2, 5, 4, 3, 2, 1 };
        Console.WriteLine(SubarrayFinder.MaxIncreasingSubarrayAsJson(array2));

        var array3 = new List<int> { 1, 2, 2, 3 };
        Console.WriteLine(SubarrayFinder.MaxIncreasingSubarrayAsJson(array3));

        var array4 = new List<int> { 1, 3, 5, 4, 7, 8, 2 };
        Console.WriteLine(SubarrayFinder.MaxIncreasingSubarrayAsJson(array4));

        var array5 = new List<int> { };
        Console.WriteLine(SubarrayFinder.MaxIncreasingSubarrayAsJson(array5));
    }
}
